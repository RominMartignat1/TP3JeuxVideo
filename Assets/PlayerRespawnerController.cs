using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawnerController : MonoBehaviour
{

    [SerializeField] private Finder finder;
    private GameObject[] players ;//= //GameObject.FindGameObjectsWithTag("Player");

    // Start is called before the first frame update
    void Start()
    {
        //players = finder.GetPlayers();
        //W players = 
        players = GameObject.FindGameObjectsWithTag("Player");
        
    }

    //awake
    private void Awake()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        //if one of the player is inactivated, respawn him
        foreach (GameObject player in players)
        {
            if (!player.activeSelf)
            {
                //player.transform.position = GetRandomPosition();
                RespawnPlayer(player);
            }
        }
    }

    public void RespawnPlayer(GameObject player)
    {
        player.SetActive(true);
        player.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
        //player.GetComponent<PlayerController>().
       // getplayers();
    }
}
