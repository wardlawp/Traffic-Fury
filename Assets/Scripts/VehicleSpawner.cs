using UnityEngine;
using System.Collections.Generic;
using Traffic;

public class VehicleSpawnerException : System.Exception
{
    public VehicleSpawnerException() {}
    public VehicleSpawnerException(string message): base(message) {}
}


public class VehicleSpawner : MonoBehaviour
{
    public GameObject[] carProtoTypes;

    // Camera position is used to calculate position at which vehicles should be spawned
    public Camera levelCamera;

    // Left and right lane X Coordinate
    public float leftLaneX;
    public float rightLaneX;
    
    // Y offset: distance from the camera centre at which vehicles should be spawned
    public float yOffset;
    public int numLanes;

    private System.Random rand;
    private GameObject[] currVehicles;
    private float lastFetchFrame;
    private const int maxMoveAttempts = 20;

    void Start()
    {
        lastFetchFrame = 0f;
        rand = new System.Random();
    }

    public GameObject createCar(ScheduleEntry e, ref List<Tuple<GameObject, ScheduleEntry>> runningCars)
    {
        Vector3 position = calculateVehicleStartPosition(e, ref runningCars);

        GameObject car = (GameObject)Instantiate(
                carProtoTypes[e.carId],
                position,
                new Quaternion()
            );

        adjustCarToAvoidCollision(car, e.appearAtBottom(), ref runningCars);

        car.GetComponent<Vehicle>().speed = e.appearance().speed;
        return car;
    }

    private void adjustCarToAvoidCollision(GameObject vehicleObj, bool appearAtBottom, ref List<Tuple<GameObject, ScheduleEntry>> runningCars)
    {
        bool colliding = true;
        int attempts = 0;

        while (colliding && (attempts < maxMoveAttempts))
        {

            colliding = isVehicleColliding(vehicleObj, ref runningCars);

            if (colliding)
            {
                moveVehicle(vehicleObj, appearAtBottom);
            }

            attempts += 1;
        }

        if (attempts == maxMoveAttempts)
        {
            throw new VehicleSpawnerException("Max attempts to adjust vehicle exceeded");
        }
    }

    private void moveVehicle(GameObject vehicleObj, bool appearAtBottom)
    {
        float dY = 0.0f;
        if (appearAtBottom)
        {
            dY = -yOffset * 0.1f;
        }
        else
        {
            dY = +yOffset * 0.1f;
        }

        vehicleObj.transform.Translate(new Vector3(0, dY));
    }

    //TODO refactor to user runningCars
    private bool isVehicleColliding(GameObject vehicleObj, ref List<Tuple<GameObject, ScheduleEntry>> runningCars)
    {

        Renderer vehicleImage = vehicleObj.GetComponent<Renderer>();

        foreach (Tuple<GameObject, ScheduleEntry> otherCar in runningCars)
        {
            if (otherCar.left == vehicleObj) continue;

            Renderer otherImage = otherCar.left.GetComponent<Renderer>();

            if (vehicleImage.bounds.Intersects(otherImage.bounds))
            {
                return true;
            }
        }

        return false;
    }

    private GameObject[] getVehicles()
    {
        if(lastFetchFrame != Time.frameCount)
        {
            lastFetchFrame = Time.frameCount;
            currVehicles = GameObject.FindGameObjectsWithTag("Vehicle");
        }

        return currVehicles;
    }


    private Vector3 calculateVehicleStartPosition(ScheduleEntry e, ref List<Tuple<GameObject, ScheduleEntry>> runningCars)
    {
        TrafficEvent appearance = e.appearance();
        float dx = (rightLaneX - leftLaneX) / numLanes;
        float x;

        if(e.xOverride == 0)
        {
            x = leftLaneX + dx * (e.lane - 0.5f);
        }
        else
        {
            x = e.xOverride;
        }
        

        if(appearance.otherScheduleRef != null)
        {
            float othervehicleY = getOtherVehicleY(appearance.otherScheduleRef, ref runningCars);
            return new Vector3(x, othervehicleY + appearance.relativeDistance, 0);
        } 
        else
        {

            float y = levelCamera.transform.position.y;

            if (appearance.appearAtBottom)
            {
                y -= yOffset;
            }
            else
            {
                y += yOffset;
            }

            return new Vector3(x, y, 0);
        }
    }

    private float getOtherVehicleY(int? id, ref List<Tuple<GameObject, ScheduleEntry>> runningCars)
    {
        foreach (Tuple<GameObject, ScheduleEntry> runningCar in runningCars)
        {
            if(runningCar.right.id == id)
            {
                return runningCar.left.transform.position.y;
            }

        }

        throw new VehicleSpawnerException("Could not find referenced vehicle");
    }
}
