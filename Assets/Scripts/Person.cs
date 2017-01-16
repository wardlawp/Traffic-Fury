using UnityEngine;
using System.Collections;

abstract public class Person : MonoBehaviour
{
    protected void moveWithPlatform()
    {
        GameObject platformVehicle = findPlatformVehicle();
        
        if(platformVehicle != null)
        {
            MovingPlatform platform = platformVehicle.GetComponent<MovingPlatform>();
            transform.Translate(0, platform.directionalSpeedNormalized(), 0);
        }

    }

    private GameObject findPlatformVehicle()
    {
        GameObject[] vehicles = GameObject.FindGameObjectsWithTag("Vehicle");

        foreach (GameObject vehicle in vehicles)
        {
            if (onPlatVehicle(vehicle)) return vehicle;
        }

        return null;
    }

    private bool onPlatVehicle(GameObject vehicle)
    {
        var bounds = GetComponent<Renderer>().bounds;
        var vehicleBounds = vehicle.GetComponent<Renderer>().bounds;
        var isPlat = vehicle.GetComponent<MovingPlatform>() != null;

        if (!isPlat)
        {
            return false;
        }
        else if (bounds.Intersects(vehicleBounds))
        {
            return true;
        }

        return false;
    }
}
