using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class wallpaperManager : MonoBehaviour
{
    //get every children in a list
    //private List<GameObject> children = new List<GameObject>();

    // private GameObject[] blocks;
    private WallGenerator generator;
    private float moveSpeed = 5f;
    private GameObject PlatformGeneratorManager;


    // Start is called before the first frame update
    void Start()
    {
       // blocks = GetEveryBlock(this.gameObject);
        PlatformGeneratorManager = GameObject.FindGameObjectWithTag("PlatformSpawner");
        generator = FindObjectOfType<WallGenerator>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("J'ai exit un trigger");
        if (collision.gameObject.tag == "Despawner")
        {
            generator.RespawnWall(gameObject);


        }
    }
    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.down * Time.deltaTime * moveSpeed, Space.World);
        //if (transform.position.y < -10)
    }


}
