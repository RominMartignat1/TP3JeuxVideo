using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawnerController : MonoBehaviour
{

    [SerializeField] private Finder finder;
    private GameObject[] players ;

    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        
    }

    private void Awake()
    {
   
    }

    void Update()
    {
        foreach (GameObject player in players)
        {
            if (!player.activeSelf)
            {
                RespawnPlayer(player);
            }
        }
    }

    public void RespawnPlayer(GameObject player)
    {
        player.SetActive(true);
        player.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
    }
}
