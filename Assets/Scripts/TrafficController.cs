using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.TrafficController;


public class Tuple<T1, T2>
{
    public T1 key { get; private set; }
    public T2 value { get; private set; }

    internal Tuple(T1 key, T2 value)
    {
        this.key = key;
        this.value = value;
    }
}

public class TrafficController : MonoBehaviour {

    public GameObject[] carProtoTypes;
    public GameObject background;

    public float leftLaneX;
    public float rightLaneX;
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

        foreach(GameObject car in cars)
        {
            runCar(car);
        }

        Debug.Log(que.lenght);
        //Todo avoid collisions

    }

    void createCar(TrafficScheduleEntry e)
    {
        Vector3 position = calculatePosition(e.lane, e.appearAtBottom());
        Instantiate(cars[e.carType], position, new Quaternion());
    }
    void runCar(GameObject car)
    {
        
    }

    private void buildQue()
    {
        que = new TrafficQue();

        que.addEntry(
            TrafficEntryBuilder.start()
                .setLane(1)
                .setCarType(1)
                .appearAt(10f, 5f)
                .accelerateAt(15f, 10f, 2f)
                .get()
            );

        que.addEntry(
            TrafficEntryBuilder.start()
                .setLane(2)
                .setCarType(2)
                .appearAt(4f, 4f)
                .get()
            );
    }
}
