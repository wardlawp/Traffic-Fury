using UnityEngine;
using System.Collections;

abstract public class PlatformRider : MonoBehaviour
{
    private GameObject[] vehicles;


    protected void Update()
    {
        vehicles = GameObject.FindGameObjectsWithTag("Vehicle");
    }

    /// <summary>
    /// Get the speed of the vehicle the Platform Rider's on
    /// </summary>
    /// <returns>
    /// float speed
    /// </returns>
    protected float getPlatformSpeed(Bounds riderBounds)
    {
        MovingPlatform platform = findPlatformVehicle(GetComponent<Renderer>().bounds);

        if (platform != null)
        {
            return platform.directionalSpeedNormalized();
        }
      
        return 0.0f;
    }

    protected MovingPlatform findPlatformVehicle(Vector3 position)
    {
        foreach (GameObject vehicle in vehicles)
        {
            MovingPlatform plat = getPlatComponent(vehicle);
            if ((plat != null) && onPlatVehicle(vehicle, position))
                return plat;
        }

        return null;
    }

    protected MovingPlatform findPlatformVehicle(Bounds bounds)
    {
        foreach (GameObject vehicle in vehicles)
        {
            MovingPlatform plat = getPlatComponent(vehicle);
            if ((plat != null) && onPlatVehicle(vehicle, bounds))
                return plat;
        }

        return null;
    }

    private MovingPlatform getPlatComponent(GameObject obj)
    {
        MovingPlatform plat = obj.GetComponent<MovingPlatform>();

        if(plat != null)
        {
            return plat;
        }
        else
        {
            return obj.GetComponentInParent<MovingPlatform>();
        }
    }

    private bool onPlatVehicle(GameObject vehicle, Vector3 position)
    {
        var vehicleBounds = vehicle.GetComponent<Renderer>().bounds;
        return vehicleBounds.Contains(position);

    }

    private bool onPlatVehicle(GameObject vehicle, Bounds bounds)
    {
        var vehicleBounds = vehicle.GetComponent<Renderer>().bounds;
        return vehicleBounds.Intersects(bounds);
    }
}
