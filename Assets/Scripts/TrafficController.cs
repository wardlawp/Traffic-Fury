﻿using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Traffic;


public class Tuple<T1, T2>
{
    public T1 left { get; private set; }
    public T2 right { get; private set; }

    internal Tuple(T1 left, T2 right)
    {
        this.left = left;
        this.right = right;
    }
}

public class TrafficController : MonoBehaviour {

    public GameObject[] carProtoTypes;
    public Camera camera;

    public float leftLaneX;
    public float rightLaneX;
    public float yOffset;
    public int numLanes;

    private Queue queue;
    private List<Tuple<GameObject, ScheduleEntry>> runningCars;

	// Use this for initialization
	void Start () {
        runningCars = new List<Tuple<GameObject, ScheduleEntry>>();

        buildQue();
    }

    


    // Update is called once per frame
    void Update () {
        foreach(ScheduleEntry e in queue.getCurrentEntries(Time.time))
        {
            createCar(e);
        }

        List<Tuple<GameObject, ScheduleEntry>> toDel = new List<Tuple<GameObject, ScheduleEntry>>();
        foreach (Tuple<GameObject, ScheduleEntry> rCar in runningCars)
        {
            foreach (TrafficEvent e in rCar.right.events)
            {
                if (e.time < Time.time)
                {
                    if ((e.type == TrafficEvent.types.Accelerate) && ((e.time + e.duration) > Time.time))
                    {
                        MovingPlatform plat = rCar.left.GetComponent<MovingPlatform>();
                        float currSpeed = plat.speed;
                        float targetSpeed = e.speed;

                        float newSpeed = currSpeed + Time.deltaTime * e.rate;
                        plat.speed = newSpeed;
                    }
                    else if (e.type == TrafficEvent.types.Explode)
                    {
                        toDel.Add(rCar);
                        //TODO
                    }
                }
            }
        }

        foreach(Tuple < GameObject, ScheduleEntry> rCar in toDel)
        {
            Destroy(rCar.left);
            runningCars.Remove(rCar);
        }

        //Todo avoid collisions
    }

    void createCar(ScheduleEntry e)
    {
        Vector3 position = calculatePosition(e.lane, e.appearAtBottom());

        GameObject car = (GameObject) Instantiate(
                carProtoTypes[e.carType], 
                position, 
                new Quaternion()
            );

        adjustCar(car, e.appearAtBottom());

        car.GetComponent<MovingPlatform>().speed = e.appearance().speed;
        runningCars.Add(new Tuple<GameObject, ScheduleEntry>(car, e));
    }

    private void adjustCar(GameObject car, bool appearAtBottom)
    {
        GameObject[] otherCars = GameObject.FindGameObjectsWithTag("Vehicle");

        bool colliding = true;

        while (colliding)
        {
            colliding = false;

            foreach(GameObject otherCar in otherCars)
            {

                if (otherCar == car) continue;

                if (car.GetComponent<Renderer>().bounds.Intersects(otherCar.GetComponent<Renderer>().bounds))
                {
                    colliding = true;
                    break;
                }
            }

            if (colliding)
            {
                float dY = 0.0f;
                if(appearAtBottom)
                {
                    dY = -yOffset * 0.05f;
                }
                else
                {
                    dY = +yOffset * 0.05f;
                }

                car.transform.Translate(new Vector3(0, dY));
            }
        }

    }

    private Vector3 calculatePosition(int lane, bool appearAtBottom)
    {
        float dx = (rightLaneX - leftLaneX) / numLanes;
        float x = leftLaneX + dx * (lane - 0.5f);

        float y = camera.transform.position.y;

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
        ScheduleEntryBuilder.carIdxMax = 3;
        float time = 13.7f;

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
            .appearAt(time + 8f, 1.8f)
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
