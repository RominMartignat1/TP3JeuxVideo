using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGeneratorManager : MonoBehaviour
{
    
    private GameObject platform;
    private const float HEIGHT_BETWEEN_PLATFORM  = 1.5f;
    private Finder finder;
    private float height;
    private int counter = 0;
    private bool spawningTime = false;

    //public List<GameObject> platforms = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        platform = GameObject.Find("Platforms");
        finder = FindObjectOfType<Finder>();
        height = gameObject.transform.parent.position.y;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckHeight();
        if (spawningTime && counter == 0) SpawnAPlatform();
    }

    private void CheckHeight()
    {
        if(height + HEIGHT_BETWEEN_PLATFORM <= gameObject.transform.parent.position.y)
        {
            counter = 0;
            height+= HEIGHT_BETWEEN_PLATFORM;
            spawningTime = true;
        }
    }

    void SpawnAPlatform()
    {
        spawningTime = false;
        counter++;
        Vector3 vector = finder.GetPositionOfTheHighestChild(platform);
        if (vector == Vector3.zero) return;
        //height = gameObject.transform.position.y;
        GameObject child = finder.GetAChildNotActive(platform);
        if (child == null ) return;
        child.transform.position = new Vector3(vector.x, vector.y + HEIGHT_BETWEEN_PLATFORM + 1, vector.z);
        child.SetActive(true);
        





    }
    
}
