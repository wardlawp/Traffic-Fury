using UnityEngine;
using System.Collections;

public abstract class MovingPlatform : MonoBehaviour
{

    /// <summary>
    /// Speed [m/s]
    /// </summary>
    public float speed = 1;

    public float directionalSpeedNormalized()
    {
        return -speed * Time.deltaTime;
    }
}
