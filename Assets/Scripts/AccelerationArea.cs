using UnityEngine;
using System.Collections;

public class AccelerationArea : MonoBehaviour {

    public float accleration = 1;
    public float maxSpeed = 2;
    public float minSpeed = 0;

	void Update () {
        accelerateVehicles();
    }

    void accelerateVehicles()
    {
        GameObject[] vehicles = GameObject.FindGameObjectsWithTag("Vehicle");

        foreach(GameObject vehicle in vehicles)
        {
            if(inAccelerationZone(vehicle))
                accelerateVehicles(vehicle);
        }
    }

    bool inAccelerationZone(GameObject vehicleGameObj)
    {
        var bounds = GetComponent<Collider>().bounds;
        var vehicleBounds = vehicleGameObj.GetComponent<Renderer>().bounds;

        return bounds.Intersects(vehicleBounds);
    }
    void accelerateVehicles(GameObject vehicleGameObj)
    {
        //TODO get Vehicle component and allow it to specify do not accelerate
        Vehicle vehicle = vehicleGameObj.GetComponent<Vehicle>();

        float nextSpeed = vehicle.speed + (accleration * Time.deltaTime);
        vehicle.speed = Mathf.Max(minSpeed, Mathf.Min(nextSpeed, maxSpeed));
    }
}
