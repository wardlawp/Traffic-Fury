using UnityEngine;
using System.Collections;

public class Vehicle : MovingPlatform
{

	void Start()
    {
	
	}
	
	// Update is called once per frame
	void Update()
    {
        moveDownScreen();
    }

    void moveDownScreen()
    {
        float speedNormalized = speed * Time.deltaTime;
        transform.Translate(0, -speedNormalized, 0);
    }
}
