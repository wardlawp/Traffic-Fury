using UnityEngine;
using System.Collections;

abstract public class Person : MonoBehaviour
{
    protected float getPlatformSpeed()
    {
        GameObject platformVehicle = findPlatformVehicle(GetComponent<Renderer>().bounds);

        if (platformVehicle != null)
        {
            MovingPlatform platform = platformVehicle.GetComponent<MovingPlatform>();
            return platform.directionalSpeedNormalized();
        }

        return 0.0f;
    }

    protected GameObject findPlatformVehicle(Vector3 position)
    {
        GameObject[] vehicles = GameObject.FindGameObjectsWithTag("Vehicle");

        foreach (GameObject vehicle in vehicles)
        {
            if (onPlatVehicle(vehicle, position)) return vehicle;
        }

        return null;
    }

    protected GameObject findPlatformVehicle(Bounds bounds)
    {
        GameObject[] vehicles = GameObject.FindGameObjectsWithTag("Vehicle");

        foreach (GameObject vehicle in vehicles)
        {
            if (onPlatVehicle(vehicle, bounds)) return vehicle;
        }

        return null;
    }

    private bool onPlatVehicle(GameObject vehicle, Vector3 position)
    {
        var bounds = GetComponent<Renderer>().bounds;
        var vehicleBounds = vehicle.GetComponent<Renderer>().bounds;
        var isPlat = vehicle.GetComponent<MovingPlatform>() != null;

        if (!isPlat)
        {
            return false;
        }
        else if (vehicleBounds.Contains(position))
        {
            return true;
        }

        return false;
    }

    private bool onPlatVehicle(GameObject vehicle, Bounds bounds)
    {
        var vehicleBounds = vehicle.GetComponent<Renderer>().bounds;
        var isPlat = vehicle.GetComponent<MovingPlatform>() != null;

        if (!isPlat)
        {
            return false;
        }
        else if (vehicleBounds.Intersects(bounds))
        {
            return true;
        }

        return false;
    }
}
