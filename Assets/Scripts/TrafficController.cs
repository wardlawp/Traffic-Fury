using UnityEngine;
using System.Collections.Generic;
using Traffic;


public class TrafficController : MonoBehaviour {

    private VehicleSpawner spawner;
    private Queue queue;
    private List<Tuple<GameObject, ScheduleEntry>> runningCars;
    private float queStartTime;
    
    public void setQueue(Queue queue)
    {
        queStartTime = Time.time;
        this.queue = queue;
    }

	void Start () {
        
        spawner = GetComponent<VehicleSpawner>();
        runningCars = new List<Tuple<GameObject, ScheduleEntry>>();
    }

    public void reset()
    {
        this.queue = null;

        foreach (Tuple<GameObject, ScheduleEntry> runningCar in runningCars)
        {
            Destroy(runningCar.left);
        }

        runningCars.Clear();
    }

    void Update () {
        createSchduledVehicles();
        runSchedule();
    }

    public void placePlayerOnFirstCar(GameObject player)
    {
        GameObject car = runningCars[0].left;

        player.transform.position = car.transform.position;
    }


    void createSchduledVehicles()
    {
        foreach (ScheduleEntry e in queue.getCurrentEntries(currentRunTime()))
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
            if (e.occuring(currentRunTime()))
            {
                if (e.type == TrafficEvent.types.Accelerate)
                {
                    vehicleObj.GetComponent<Vehicle>().accerlate(e.rate);
                }

                else if(e.type == TrafficEvent.types.InitiateExplosion)
                {
                    vehicleObj.GetComponent<Animator>().SetBool("initiateExplosion", true);
                }

                else if (e.type == TrafficEvent.types.Explode)
                {
                    removeVehicle = true;
                }
            }
        }

        return removeVehicle;
    }

    private float currentRunTime()
    {
        return Time.time - queStartTime;
    }
}
