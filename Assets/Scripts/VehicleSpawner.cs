using UnityEngine;
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
    private const int maxMoveAttempts = 10;

    void Start()
    {
        lastFetchFrame = 0f;
        rand = new System.Random();
    }

    public GameObject createCar(ScheduleEntry e)
    {
        Vector3 position = calculateVehicleStartPosition(e.lane, e.appearAtBottom());

        GameObject car = (GameObject)Instantiate(
                carProtoTypes[rand.Next(0, carProtoTypes.Length)],
                position,
                new Quaternion()
            );

        adjustCarToAvoidCollision(car, e.appearAtBottom());

        car.GetComponent<Vehicle>().speed = e.appearance().speed;
        return car;
    }

    private void adjustCarToAvoidCollision(GameObject vehicleObj, bool appearAtBottom)
    {
        bool colliding = true;
        int attempts = 0;

        while (colliding && (attempts < maxMoveAttempts))
        {

            colliding = isVehicleColliding(vehicleObj);

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
        GameObject[] otherCars = getVehicles();
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

    private GameObject[] getVehicles()
    {
        if(lastFetchFrame != Time.frameCount)
        {
            lastFetchFrame = Time.frameCount;
            currVehicles = GameObject.FindGameObjectsWithTag("Vehicle");
        }

        return currVehicles;
    }


    private Vector3 calculateVehicleStartPosition(int lane, bool appearAtBottom)
    {
        float dx = (rightLaneX - leftLaneX) / numLanes;
        float x = leftLaneX + dx * (lane - 0.5f);

        float y = levelCamera.transform.position.y;

        if (appearAtBottom)
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
