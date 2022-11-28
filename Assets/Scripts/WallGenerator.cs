using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGenerator : MonoBehaviour
{
    private GameObject walls;
    private Finder finder;
    private float height;
    private bool spawningTime = false;
    private int counter = 0;
    private const int SPACE_BETWEEN = 0;
    // Start is called before the first frame update
    void Start()
    {
        walls = GameObject.Find("WallPaper");
        finder = FindObjectOfType<Finder>();
        height = gameObject.transform.parent.position.y;


    }

    // Update is called once per frame
    void Update()
    { 
    }


    void SpawnAPlatform()
    {

      
    }

    public void RespawnWall(GameObject wallToRespawn)
    {
        Vector3 vector = finder.GetPositionOfTheHighestChild(walls);
        if (vector == Vector3.zero) return;
        //height = gameObject.transform.position.y;
        //GameObject child = finder.GetAChildNotActive(walls);
        if (wallToRespawn == null) return;
        wallToRespawn.transform.position = new Vector3(vector.x, vector.y + wallToRespawn.GetComponent<Renderer>().bounds.size.y, vector.z);
    }
}
