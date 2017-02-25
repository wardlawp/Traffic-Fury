using UnityEngine;
using System.Collections;

public class Vehicle : MovingPlatform
{
    // Update is called once per frame
	void Update()
    {
        moveDownScreen();
    }

    void moveDownScreen()
    {
        transform.Translate(0, directionalSpeedNormalized(), 0);
    }
}
