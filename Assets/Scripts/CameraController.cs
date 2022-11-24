using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool isFollowingPlayer;
    private float moveSpeed = 0.5f; 

    private PlayerController playerBeingFollowed;
    // Start is called before the first frame update
    void Start()
    {
        isFollowingPlayer = false;
        playerBeingFollowed = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isFollowingPlayer = true;
            if (collision != playerBeingFollowed)
            {
                if (collision.transform.position.y > playerBeingFollowed.transform.position.y)
                {
                    isFollowingPlayer = false;
                   // playerBeingFollowed = collision.gameObject;
                }
            }

        }
       /* if (col.gameObject.tag == "Player")
        {
            Debug.Log("Player entered camera trigger");
            isFollowingPlayer = true;
            playerBeingFollowed = col.gameObject;
            if(col != playerBeingFollowed)
            {
                if(col.transform.position.y > playerBeingFollowed.transform.position.y)
                {
                    playerBeingFollowed = col.gameObject;
                }
            }
        }*/
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            isFollowingPlayer = false;
            playerBeingFollowed = null;
        }
    }

    void makeCameraFollow(PlayerController player)
    {
        Vector3 playerPos = player.transform.position;
        Vector3 cameraPos = transform.position;
        if(playerBeingFollowed.transform.position.y < transform.position.y)
        {
            transform.Translate(Vector3.down * Time.deltaTime * moveSpeed, Space.World);
        }

    }
    
}
