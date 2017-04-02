using UnityEngine;
using System.Collections;

public class Player : PlatformRider
{
    public float playerSpeed = 1;

    public float jumpTime = 0.3f;
    public float jumpSeed = 1.6f;

    const char LEFT = 'l';
    const char RIGHT = 'r';
    const char UP = 'u';
    const char DOWN = 'd';

    public bool dead = false;
    public bool jumping = false;
    public bool onPlatfrom = false;
    public char direction = DOWN;

    private float remainingJumpTime = 0.0f;
    private float platformMomentum;
    private float platformSpeed = 0.0f;
    

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }


    new void Update () {
        base.Update();
        calculateStates();
        
        if (!dead){
            handleMovment();
        }

        setAnimationStates();
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
        return (findPlatformVehicle(transform.position) != null);
    }

    #endregion

    void handleMovment()
    {
        //if player wants to jump and they are on vehicle and not jumping then goto jumping
        if(canJump() && jumpKeyPressed())
        {
            startJump();
        }

        Vector3 input = getInputVector();
        Vector3 timeNormalizedInput = input.normalized * Time.deltaTime;

        if (jumping)
        {
            handleJumpMovement(timeNormalizedInput);
        }
        else
        {
            handlePlatformMovment(timeNormalizedInput);
        }
    }

    void handleJumpMovement(Vector3 timeNormalizedInput)
    {
       
        remainingJumpTime -= Time.deltaTime;
        Vector3 playerMotion = new Vector3(timeNormalizedInput.x * jumpSeed, (timeNormalizedInput.y * jumpSeed) + platformMomentum*Time.deltaTime, 0);
        movePlayer(playerMotion);
    }

    void handlePlatformMovment(Vector3 timeNormalizedInput)
    {
        Vector3 playerMotion = new Vector3(timeNormalizedInput.x * playerSpeed, (timeNormalizedInput.y * playerSpeed) + platformSpeed*Time.deltaTime, 0);
        movePlayer(playerMotion);
    }

    void startJump()
    {
        remainingJumpTime = jumpTime;
        jumping = true;
        platformMomentum = platformSpeed;
    }

    bool jumpKeyPressed()
    {

        return (Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.Space));
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
        setPlayerRenderDirection(movement);
        transform.Translate(movement);
    }

    private Vector3 getInputVector()
    {
        Vector3 input = new Vector3();

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            input.y += 1;

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            input.y -= 1;

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            input.x += 1;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            input.x -= 1;

        return input;
    }

    void setAnimationStates()
    {
        anim.SetBool("jumping", jumping);
        anim.SetBool("dead", dead);
    }
}
