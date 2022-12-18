using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraController : MonoBehaviour
{
    private bool isFollowingPlayer;

    private const float MOVE_SPEED_TO_ADD = 0.75f;
    private float currentMoveSpeed = 0.0f;
    private GameObject playerBeingFollowed;

    void Start()
    {

        isFollowingPlayer = false;
        playerBeingFollowed = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (!PauseManager.GameIsPaused || !GameSceneManager.GameIsEnded)
        {
            if (!isFollowingPlayer)
            {
                transform.Translate(Vector3.up * Time.deltaTime * currentMoveSpeed, Space.World);
            }
        }

    }

    private void LateUpdate()
    {
        if (!PauseManager.GameIsPaused || !GameSceneManager.GameIsEnded) 
        {
            if (isFollowingPlayer)
            {
                makeCameraFollow(playerBeingFollowed);
            }
        }
        
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            //check if player is dead or respawnig
            if (!col.gameObject.GetComponent<PlayerController>().isPlayerDeadOrRespawning())
            {
                if (col.gameObject.GetComponent<Rigidbody2D>().velocity.y > 0)
                {
                    isFollowingPlayer = true;
                    playerBeingFollowed = col.gameObject;
                    //if player that has entered is not the player that is being followed, check which one is the highest and follow that one
                    if (col != playerBeingFollowed)
                    {
                        if (col.transform.position.y > playerBeingFollowed.transform.position.y)
                        {
                            isFollowingPlayer = true;
                            playerBeingFollowed = col.gameObject;
                        }
                    }
                }

            }

        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            isFollowingPlayer = false;
            playerBeingFollowed = null;
        }
    }

    void makeCameraFollow(GameObject player)
    {
        Vector3 playerPos = player.transform.position;
        Vector3 cameraPos = transform.position;
        if (playerBeingFollowed.transform.position.y > transform.position.y)
        {
            transform.position = new Vector3(cameraPos.x, playerPos.y, cameraPos.z);
        }

    }

    public void ChangeSpeed()
    {
        currentMoveSpeed += MOVE_SPEED_TO_ADD;
    }
}