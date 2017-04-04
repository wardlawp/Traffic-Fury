using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {

    public Player player;
    public float levelResetPause = 1.5f;
    private float respawnDelay = 0.5f;

    private AudioSource audioS;
    private TrafficController trafficController;
    private float currentLevelProgress = 0f;
    private float playerDiedTime = 0f;
    
    private float respawnTime = 0f;

    private enum States { Playing, Died, Ressetting}
    private States gameState;

    void Start ()
    {
        gameState = States.Playing;

        audioS = GetComponent<AudioSource>();
        trafficController = GetComponent<TrafficController>();

        trafficController.setQueue(LevelQue.get());
        testJumpAhead(LevelQue.SEQUENCE_5_START);
    }

    void testJumpAhead(float time)
    {
        gameState = States.Died;
        playerDiedTime = Time.time;
        currentLevelProgress = time;
    }

	void Update () {

        currentLevelProgress += Time.deltaTime;

        if (player.dead && (gameState == States.Playing))
        {
            //Update state and schdule reset
            gameState = States.Died;
            playerDiedTime = Time.time;
        }
  
        if ((gameState == States.Died) && (Time.time > (playerDiedTime + levelResetPause)))
        {
            //Calculate time in level to reset to
            float checkpointTime = LevelQue.findCheckpointTime(currentLevelProgress); //this should be playerDiedTime
            currentLevelProgress = checkpointTime;
            

            //Reset
            trafficController.reset();
            trafficController.setQueue(LevelQue.get(checkpointTime));
            playerDiedTime = 0.0f;

            //Prepare for respawn
            respawnTime = Time.time + respawnDelay;
            gameState = States.Ressetting;
        }

        if((gameState == States.Ressetting) && (respawnTime < Time.time))
        {
            //Respawn
            trafficController.placePlayerOnFirstCar(player.gameObject);
            player.dead = false;

            audioS.time = currentLevelProgress;

            //Progress to playing state
            respawnTime = 0.0f;
            gameState = States.Playing;
        }
    }
}
