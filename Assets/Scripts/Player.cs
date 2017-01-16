using UnityEngine;
using System.Collections;

public class Player : Person
{
    GameObject currentPlatform
    public float speed = 1;
	
	void Update () {
        moveWithPlatform();
        handleControls();
    }

    void handleControls()
    {
        Vector3 input = getInputVector();
        Vector3 magnitudeNormalizedInput = input.normalized;
        Vector3 timeNormalizedInput = magnitudeNormalizedInput * Time.deltaTime;

        if (aboutToJump(timeNormalizedInput))

        transform.Translate(timeNormalizedInput);
 
    }

    private bool aboutToJump(Vector3 movement)
    {
        Vector3 playerCentre = transform.position;

    }

    private Vector3 getInputVector()
    {
        Vector3 input = new Vector3();

        if (Input.GetKey(KeyCode.UpArrow))
            input.y += 1;

        if (Input.GetKey(KeyCode.DownArrow))
            input.y -= 1;

        if (Input.GetKey(KeyCode.RightArrow))
            input.x += 1;

        if (Input.GetKey(KeyCode.LeftArrow))
            input.x -= 1;

        return input;
    }

}
