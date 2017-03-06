using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Traffic;


public class TrafficController : MonoBehaviour {

    public GameObject[] carProtoTypes;
    public Camera levelCamera;

    // Lane information for spawning cars
    public float leftLaneX;
    public float rightLaneX;
    public float yOffset;
    public int numLanes;

    // The Traffic que and currently running cars
    private Queue queue;
    private List<Tuple<GameObject, ScheduleEntry>> runningCars;
    private System.Random rand;

	void Start () {
        rand = new System.Random();
        runningCars = new List<Tuple<GameObject, ScheduleEntry>>();
        buildQue();
    }

    void Update () {
        createSchduledVehicles();
        runSchedule();
    }

    void createSchduledVehicles()
    {
        foreach (ScheduleEntry e in queue.getCurrentEntries(Time.time))
        {
            createCar(e);
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

    void createCar(ScheduleEntry e)
    {
        Vector3 position = calculateVehicleStartPosition(e.lane, e.appearAtBottom());

        GameObject car = (GameObject) Instantiate(
                carProtoTypes[rand.Next(0, carProtoTypes.Length)], 
                position, 
                new Quaternion()
            );

        adjustCarToAvoidCollision(car, e.appearAtBottom());

        car.GetComponent<Vehicle>().speed = e.appearance().speed;
        runningCars.Add(new Tuple<GameObject, ScheduleEntry>(car, e));
    }

    private void adjustCarToAvoidCollision(GameObject vehicleObj, bool appearAtBottom)
    {
        bool colliding = true;
        int attempts = 0;

        while (colliding && (attempts < 10))
        {

            colliding = isVehicleColliding(vehicleObj);

            if (colliding)
            {
                nudgeVehicle(vehicleObj, appearAtBottom);
            }

            attempts += 1;
        }

        if (attempts == 10)
        {
            //TODO: use better exception type
            throw new System.Exception("Max attempts to adjust vehicle exceeded"); 
        }
    }

    private void nudgeVehicle(GameObject vehicleObj, bool appearAtBottom)
    {
        float dY = 0.0f;
        if (appearAtBottom)
        {
            dY = -yOffset * 0.05f;
        }
        else
        {
            dY = +yOffset * 0.05f;
        }

        vehicleObj.transform.Translate(new Vector3(0, dY));
    }

    private bool isVehicleColliding(GameObject vehicleObj)
    {
        GameObject[] otherCars = GameObject.FindGameObjectsWithTag("Vehicle"); //TODO use running cars instead?
        Renderer vehicleImage = vehicleObj.GetComponent<Renderer>();

        foreach (GameObject otherCar in otherCars)
        {
            if (otherCar == vehicleObj) continue;

            Renderer otherImage = otherCar.GetComponent<Renderer>();

            if (vehicleImage.bounds.Intersects(otherImage.bounds))
            {
                return true;
            }
        }

        return false;
    }

    private Vector3 calculateVehicleStartPosition(int lane, bool appearAtBottom)
    {
        float dx = (rightLaneX - leftLaneX) / numLanes;
        float x = leftLaneX + dx * (lane - 0.5f);

        float y = levelCamera.transform.position.y;

        if(appearAtBottom)
        {
            y -= yOffset;
        }
        else
        {
            y += yOffset;
        }

        return new Vector3(x, y, 0);
    }

    private void buildQue()
    {
        queue = new Queue();
        float time = 1f;

        //0:00

        //~0:14 Stationary cars
        queue.addEntry(
            ScheduleEntryBuilder.start()
                .setLane(6)
                .appearAt(time + 0.2f, 0f)
                .explodeAt(time + 5.0f) 
                .get()
            );

        queue.addEntry(
            ScheduleEntryBuilder.start()
                .setLane(6)
                .appearAt(time + 0.4f, 0f)
                .accelerateAt(time + 1.0f, 0.5f, 4f)
                .explodeAt(time + 6.0f)
                .get()
            );

        queue.addEntry(
           ScheduleEntryBuilder.start()
               .setLane(6)
               .appearAt(time + 0.6f, 0f)
               .accelerateAt(time + 1.0f, 0.6f, 3.2f)
               .explodeAt(time + 7.0f)
               .get()
           );

        queue.addEntry(
           ScheduleEntryBuilder.start()
               .setLane(5)
               .appearAt(time + 1f, .6f)
               .accelerateAt(time + 3.0f, 0.55f, 1.5f)
               .explodeAt(time + 11.0f)
               .get()
           );

        queue.addEntry(
           ScheduleEntryBuilder.start()
               .setLane(5)
               .appearAt(time + 2f, .6f)
               .accelerateAt(time + 3f, 0.7f, 1.4f)
               .accelerateAt(time + 5.4f, -0.2f, 0.5f)
               .explodeAt(time + 12.0f)
               .get()
           );

        queue.addEntry(
           ScheduleEntryBuilder.start()
               .setLane(4)
               .appearAt(time + 0.6f, 0f)
               .accelerateAt(time + 2.1f, 0.7f, 2.2f)
               .accelerateAt(time + 4.1f, 1.5f, 1f)
               .explodeAt(time + 13.0f)
               .get()
           );

        queue.addEntry(
           ScheduleEntryBuilder.start()
               .setLane(4)
               .appearAt(time + 2f, 0.3f, false)
               .accelerateAt(time + 2.1f, 0.7f, 2.2f)
               .accelerateAt(time + 4.1f, 1.3f, 1f)
               .explodeAt(time + 14.0f)
               .get()
           );

        queue.addEntry(
          ScheduleEntryBuilder.start()
              .setLane(3)
              .appearAt(time + 0.6f, 0f)
              .accelerateAt(time + 1.9f, 1.2f, 2f)
              .accelerateAt(time + 3.9f, -0.3f, 1f)
              .accelerateAt(time + 6f, +0.3f, 1.5f)
              .accelerateAt(time + 11f, -0.3f, 2.5f)
              .explodeAt(time + 14.0f)
              .get()
          );

        queue.addEntry(
         ScheduleEntryBuilder.start()
             .setLane(2)
             .appearAt(time + 7f, 1.8f)
             .accelerateAt(time + 13f, 0.8f, 1f)
             .explodeAt(time + 16.0f)
             .get()
         );

        queue.addEntry(
        ScheduleEntryBuilder.start()
            .setLane(2)
            .appearAt(time + 12f, 1.8f)
            .accelerateAt(time + 13f, 0.8f, 1f)
            .explodeAt(time + 19.0f)
            .get()
        );


        //~0:30 Moving column
        time += 15f;

        queue.addEntry(
          ScheduleEntryBuilder.start()
              .setLane(1)
              .appearAt(time, 4.5f, false)
              .explodeAt(time + 15f)
              .get()
          );

        queue.addEntry(
          ScheduleEntryBuilder.start()
              .setLane(1)
              .appearAt(time + 0.4f, 4.5f, false)
              .explodeAt(time + 15f)
              .get()
          );

        queue.addEntry(
          ScheduleEntryBuilder.start()
              .setLane(1)
              .appearAt(time + 0.7f, 4.5f, false)
              .explodeAt(time + 15f)
              .get()
          );

        queue.addEntry(
          ScheduleEntryBuilder.start()
              .setLane(1)
              .appearAt(time + 1.4f, 4.5f, false)
              .explodeAt(time + 15f)
              .get()
          );

        //~0:44
        time += 11.5f;

        queue.addEntry(
          ScheduleEntryBuilder.start()
              .setLane(2)
              .appearAt(time, 2.5f)
              .accelerateAt(time + 5f, 1, 0.5f)
              .explodeAt(time + 19f)
              .get()
          );

        queue.addEntry(
          ScheduleEntryBuilder.start()
              .setLane(2)
              .appearAt(time + 1.0f, 2.4f)
              .explodeAt(time + 19f)
              .accelerateAt(time + 5.1f, 1, 0.6f)
              .get()
          );

        queue.addEntry(
          ScheduleEntryBuilder.start()
              .setLane(3)
              .appearAt(40.0f, 2.5f)
              .accelerateAt(46.0f, 1, 0.5f)
              .explodeAt(time + 19f)
              .get()
          );

        queue.addEntry(
          ScheduleEntryBuilder.start()
              .setLane(3)
              .appearAt(time, 2.4f)
              .explodeAt(time + 19f)
              .accelerateAt(time + 7.1f, 1, 0.6f)
              .get()
          );

        queue.addEntry(
          ScheduleEntryBuilder.start()
              .setLane(4)
              .appearAt(time, 2.5f)
              .accelerateAt(time + 5f, -1, 0.3f)
              .accelerateAt(time + 5.4f, +1, 1.3f)
              .explodeAt(time + 19f)
              .get()
          );

        queue.addEntry(
          ScheduleEntryBuilder.start()
              .setLane(4)
              .appearAt(time + 1.0f, 2.4f)
              .explodeAt(time + 19f)
              .accelerateAt(time + 5.1f, 1, 0.7f)
              .get()
          );
    }
}
