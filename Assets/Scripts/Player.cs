using UnityEngine;
using System.Collections;

public class Player : Person
{
    public float playerSpeed = 1;

    public float jumpTime = 0.3f;
    public float jumpSeed = 1.6f;

    public bool dead = false;
    public bool jumping = false;
    public bool onPlatfrom = false;

    //Character Jumping
    private float remainingJumpTime = 0.0f;
    private Vector3 jumpingVector;

    private float platformSpeed = 0.0f;
	
	void Update () {
        updateInitialStates();

        if (!dead) handleMovment();
    }

    void updateInitialStates()
    {
        dead = dead || hitGround();
        jumping = inJump();
        platformSpeed = getPlatformSpeed();
        onPlatfrom = onPlat();
    }

    void handleMovment()
    {
        if(!jumping && onPlatfrom){
            handlePlatformMovement();
        }
        else if(jumping){
            handleJump();
        }
    }

    void handlePlatformMovement()
    {
        Vector3 input = getInputVector();
        Vector3 timeNormalizedInput = input.normalized * Time.deltaTime;
        Vector3 playerMotion = new Vector3(timeNormalizedInput.x * playerSpeed, (timeNormalizedInput.y * playerSpeed) + platformSpeed, 0);
       

        if (aboutToJump(playerMotion))
        {
            beginJump(timeNormalizedInput);
        }
        else
        {
            movePlayer(playerMotion);
        }

    }

    private bool hitGround()
    {
        return (!inJump() && !onPlat());
    }

    private bool inJump()
    {
        return (remainingJumpTime > 0.0f);
    }

    private void movePlayer(Vector3 movement)
    {
        transform.Translate(movement);
    }



    private void handleJump()
    {
        remainingJumpTime -= Time.deltaTime;
        movePlayer(jumpingVector);
  
    }

    private void beginJump(Vector3 timeNormalizedInput)
    {
        remainingJumpTime = jumpTime;
        jumpingVector = new Vector3(timeNormalizedInput.x*jumpSeed, (timeNormalizedInput.y*jumpSeed) + platformSpeed);
        
    }

    private bool onPlat()
    {
        return (findPlatformVehicle(GetComponent<Renderer>().bounds) != null);
    }

    private bool aboutToJump(Vector3 movement)
    {
        Vector3 nextPosition = transform.position + movement;
        return (findPlatformVehicle(nextPosition) == null);
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
