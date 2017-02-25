﻿using UnityEngine;
using System.Collections;

public class Player : PlatformRider
{
    public float playerSpeed = 1;

    public float jumpTime = 0.3f;
    public float jumpSeed = 1.6f;

    const char LEFT = 'L';
    const char RIGHT = 'R';
    const char UP = 'U';
    const char DOWN = 'D';


    public float yDisplacement = 0.0f;
    public bool dead = false;
    public bool jumping = false;
    public bool onPlatfrom = false;
    public char direction = DOWN;

    //Character Jumping
    private float remainingJumpTime = 0.0f;
    private float platformMomentum;

    private float platformSpeed = 0.0f;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }


	void Update () {
        getVehicles();
        calculateStates();

        if (!dead){
            handleMovment();
        }
    }

    #region Calculate States

    void calculateStates()
    {
        dead = dead || hitGround();
        jumping = inJump();
        platformSpeed = getPlatformSpeed(GetComponent<Renderer>().bounds);
        onPlatfrom = onPlat();
    }

    private bool hitGround()
    {
        return (!inJump() && !onPlat());
    }

    private bool inJump()
    {
        return (remainingJumpTime > 0.0f);
    }

    private bool onPlat()
    {
        return (findPlatformVehicle(GetComponent<Renderer>().bounds) != null);
    }

    #endregion

    void handleMovment()
    {
        //if player wants to jump and they are on vehicle and not jumping then goto jumping
        if(canJump() && (Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.Space)))
        {
            remainingJumpTime = jumpTime;
            jumping = true;
            platformMomentum = platformSpeed;
        }

        Vector3 input = getInputVector();
        Vector3 timeNormalizedInput = input.normalized * Time.deltaTime;

        if (jumping)
        {
            anim.SetBool("jumping", true);
            remainingJumpTime -= Time.deltaTime;
            Vector3 playerMotion = new Vector3(timeNormalizedInput.x * jumpSeed, (timeNormalizedInput.y * jumpSeed) + platformMomentum, 0);
            movePlayer(playerMotion);
        }
        else
        {
            anim.SetBool("jumping", false);
            Vector3 playerMotion = new Vector3(timeNormalizedInput.x * playerSpeed, (timeNormalizedInput.y * playerSpeed) + platformSpeed, 0);
            movePlayer(playerMotion);
        }

       
    }

    bool canJump()
    {
        return (!jumping && onPlatfrom);
    }




    private void setPlayerRenderDirection(Vector3 playerMotion)
    {
        if(playerMotion.x > 0)
        {
            direction = RIGHT;
        }
        else if(playerMotion.x < 0)
        {
            direction = LEFT;
        }
        else if(playerMotion.y > 0)
        {
            direction = UP;
        }
        else if (playerMotion.y < 0)
        {
            direction = DOWN;
        }
    }

    private void movePlayer(Vector3 movement)
    {
        yDisplacement = movement.y;
        transform.Translate(movement);
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
