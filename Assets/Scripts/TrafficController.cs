using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.TrafficController;


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

    private TrafficQue que;
    private List<Tuple<GameObject, TrafficScheduleEntry>> runningCars;

	// Use this for initialization
	void Start () {
        runningCars = new List<Tuple<GameObject, TrafficScheduleEntry>>();

        buildQue();
        //Debug.Log(que.lenght);
        //Debug.Log(que.getCurrentsEntries(5f)[0].lane);
        //Debug.Log(que.lenght);
    }

    


    // Update is called once per frame
    void Update () {
        foreach(TrafficScheduleEntry e in que.getCurrentsEntries(Time.time))
        {
            createCar(e);
        }

        List<Tuple<GameObject, TrafficScheduleEntry>> toDel = new List<Tuple<GameObject, TrafficScheduleEntry>>();
        foreach (Tuple<GameObject, TrafficScheduleEntry> rCar in runningCars)
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

        foreach(Tuple < GameObject, TrafficScheduleEntry > rCar in toDel)
        {
            Destroy(rCar.left);
            runningCars.Remove(rCar);
        }

        //Todo avoid collisions
    }

    void createCar(TrafficScheduleEntry e)
    {
        Vector3 position = calculatePosition(e.lane, e.appearAtBottom());

        GameObject car = (GameObject) Instantiate(
                carProtoTypes[e.carType], 
                position, 
                new Quaternion()
            );


        car.GetComponent<MovingPlatform>().speed = e.appearance().speed;
        runningCars.Add(new Tuple<GameObject, TrafficScheduleEntry>(car, e));
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
        que = new TrafficQue();

        //0:00

        //~0:14 Stationary cars
        que.addEntry(
            TrafficEntryBuilder.start()
                .setLane(6)
                .appearAt(14.2f, 0f)
                .explodeAt(19f) 
                .get()
            );

        que.addEntry(
            TrafficEntryBuilder.start()
                .setLane(6)
                .appearAt(14.4f, 0f)
                .explodeAt(20f)
                .get()
            );

        que.addEntry(
           TrafficEntryBuilder.start()
               .setLane(6)
               .appearAt(14.6f, 0f)
               .explodeAt(21f)
               .get()
           );

        que.addEntry(
           TrafficEntryBuilder.start()
               .setLane(5)
               .appearAt(14.4f, 0f)
               .explodeAt(25f)
               .get()
           );

        que.addEntry(
           TrafficEntryBuilder.start()
               .setLane(5)
               .appearAt(14.6f, 0f)
               .explodeAt(26f)
               .get()
           );

        que.addEntry(
           TrafficEntryBuilder.start()
               .setLane(4)
               .appearAt(14.6f, 0f)
               .explodeAt(27f)
               .get()
           );

        que.addEntry(
          TrafficEntryBuilder.start()
              .setLane(3)
              .appearAt(14.6f, 0f)
              .explodeAt(28f)
              .get()
          );

        que.addEntry(
          TrafficEntryBuilder.start()
              .setLane(2)
              .appearAt(14.6f, 0f)
              .explodeAt(32f)
              .get()
          );


        //~0:30 Moving column

        que.addEntry(
          TrafficEntryBuilder.start()
              .setLane(1)
              .appearAt(29.0f, 3f, false)
              .explodeAt(44f)
              .get()
          );

        que.addEntry(
          TrafficEntryBuilder.start()
              .setLane(1)
              .appearAt(29.4f, 3f, false)
              .explodeAt(44f)
              .get()
          );

        que.addEntry(
          TrafficEntryBuilder.start()
              .setLane(1)
              .appearAt(29.7f, 3f, false)
              .explodeAt(44f)
              .get()
          );

        que.addEntry(
          TrafficEntryBuilder.start()
              .setLane(1)
              .appearAt(30.2f, 3f, false)
              .explodeAt(44f)
              .get()
          );

        //~0:44
        que.addEntry(
          TrafficEntryBuilder.start()
              .setLane(2)
              .appearAt(40.0f, 2.5f)
              .accelerateAt(45.0f, 1, 0.5f)
              .explodeAt(59f)
              .get()
          );

        que.addEntry(
          TrafficEntryBuilder.start()
              .setLane(2)
              .appearAt(41.0f, 2.4f)
              .explodeAt(59f)
              .accelerateAt(45.1f, 1, 0.6f)
              .get()
          );

        que.addEntry(
          TrafficEntryBuilder.start()
              .setLane(3)
              .appearAt(40.0f, 2.5f)
              .accelerateAt(46.0f, 1, 0.5f)
              .explodeAt(59f)
              .get()
          );

        que.addEntry(
          TrafficEntryBuilder.start()
              .setLane(3)
              .appearAt(43.0f, 2.4f)
              .explodeAt(59f)
              .accelerateAt(47.1f, 1, 0.6f)
              .get()
          );

        que.addEntry(
          TrafficEntryBuilder.start()
              .setLane(4)
              .appearAt(40.0f, 2.5f)
              .accelerateAt(45.0f, -1, 0.3f)
              .accelerateAt(45.4f, +1, 1.3f)
              .explodeAt(59f)
              .get()
          );

        que.addEntry(
          TrafficEntryBuilder.start()
              .setLane(4)
              .appearAt(41.0f, 2.4f)
              .explodeAt(59f)
              .accelerateAt(45.1f, 1, 0.7f)
              .get()
          );
    }
}
