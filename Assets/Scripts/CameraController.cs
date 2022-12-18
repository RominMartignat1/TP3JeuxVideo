using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraController : MonoBehaviour
{
    private bool isFollowingPlayer;

    private float moveSpeed = 1.5f;
    private GameObject playerBeingFollowed;

    void Start()
    {

        isFollowingPlayer = false;
        playerBeingFollowed = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {

        if (isFollowingPlayer)
        {
            Debug.Log("Following player");
            makeCameraFollow(playerBeingFollowed);
        }
        else
        {
            Debug.Log("Not following player");
            transform.Translate(Vector3.up * Time.deltaTime * moveSpeed, Space.World);
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
                    Debug.Log("Player entered camera trigger");
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
            //transform.Translate(Vector3.up * Time.deltaTime * moveSpeed, Space.World);
        }

    }
}