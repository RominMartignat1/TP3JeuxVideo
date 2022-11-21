using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallpaperManager : MonoBehaviour
{
    //get every children in a list
    //private List<GameObject> children = new List<GameObject>();

   // private GameObject[] blocks;
    private float moveSpeed = 5f;
    private GameObject PlatformGeneratorManager;


    // Start is called before the first frame update
    void Start()
    {
       // blocks = GetEveryBlock(this.gameObject);
        PlatformGeneratorManager = GameObject.FindGameObjectWithTag("PlatformSpawner");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "WallRespawn")
        {
            Debug.Log("Despawner");
            transform.position = new Vector3(transform.position.x, PlatformGeneratorManager.transform.position.y+5, transform.position.z);
           // randomizePlatform();
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * moveSpeed, Space.World);
        //if (transform.position.y < -10)
    }

   

}
