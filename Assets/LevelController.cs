using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {

    public Player player;
    public float levelResetPause = 3f;

    private TrafficController trafficController;
    private float gameOverTime = 0f;
    private float levelStartTime = 0f;
    private float respawnTime = 0f;

    private enum States { Playing, Died, Ressetting}
    private States gameState;

    void Start () {
        gameState = States.Playing;
        levelStartTime = Time.time;
        trafficController = GetComponent<TrafficController>();
        trafficController.setQueue(LevelQue.get(Time.time));
	}

	void Update () {

        if (player.dead && (gameState == States.Playing))
        {
            gameState = States.Died;
            gameOverTime = Time.time;
        }
  
        if ((gameState == States.Died) && (Time.time > (gameOverTime + levelResetPause)))
        {
            //Calculate time to reset to
            float checkpointTime = LevelQue.findLatestSequence(Time.time - levelStartTime);
            levelStartTime = checkpointTime;
            gameOverTime = 0.0f;

            //Reset
            trafficController.reset();
            trafficController.setQueue(LevelQue.get(0, checkpointTime));


            //Prepare for respawn
            respawnTime = Time.time + 1;
            gameState = States.Ressetting;
        }

        if((gameState == States.Ressetting) && (respawnTime < Time.time))
        {
            //Respawn
            trafficController.placePlayerOnFirstCar(player.gameObject);
            player.dead = false;

            //Progress to playing state
            respawnTime = 0.0f;
            gameState = States.Playing;
        }
    }
}
