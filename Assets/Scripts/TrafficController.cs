using UnityEngine;
using System.Collections.Generic;
using Traffic;


public class TrafficController : MonoBehaviour {

    private VehicleSpawner spawner;
    private Queue queue;
    private List<Tuple<GameObject, ScheduleEntry>> runningCars;
    

	void Start () {
        spawner = GetComponent<VehicleSpawner>();
        runningCars = new List<Tuple<GameObject, ScheduleEntry>>();
        queue = LevelQue.get();
    }

    void Update () {
        createSchduledVehicles();
        runSchedule();
    }

    void createSchduledVehicles()
    {
        foreach (ScheduleEntry e in queue.getCurrentEntries(Time.time))
        {
            GameObject car = spawner.createCar(e);
            runningCars.Add(new Tuple<GameObject, ScheduleEntry>(car, e));
        }
    }


    void runSchedule()
    {
        List<Tuple<GameObject, ScheduleEntry>> toRemove = new List<Tuple<GameObject, ScheduleEntry>>();

        foreach (Tuple<GameObject, ScheduleEntry> runningCar in runningCars)
        {
           bool removeVehicle =  runScheduledEvents(runningCar.left, runningCar.right);

            if(removeVehicle)
            {
                toRemove.Add(runningCar);
            }
        }

        removeVehicles(toRemove);
    }

    void removeVehicles(List<Tuple<GameObject, ScheduleEntry>> toRemove)
    {
        foreach (Tuple<GameObject, ScheduleEntry> rCar in toRemove)
        {
            Destroy(rCar.left);
            runningCars.Remove(rCar);
        }
    }

    bool runScheduledEvents(GameObject vehicleObj, ScheduleEntry entry)
    {
        bool removeVehicle = false;
        foreach (TrafficEvent e in entry.events)
        {
            if (e.occuring(Time.time))
            {
                if (e.type == TrafficEvent.types.Accelerate)
                {
                    vehicleObj.GetComponent<Vehicle>().accerlate(e.rate);
                }

                else if (e.type == TrafficEvent.types.Explode)
                {
                    removeVehicle = true;
                }
            }
        }

        return removeVehicle;
    }
}
