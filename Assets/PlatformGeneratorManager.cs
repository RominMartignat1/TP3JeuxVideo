using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGeneratorManager : MonoBehaviour
{
    
    private GameObject[] platform;

    //public List<GameObject> platforms = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        platform = GameObject.FindGameObjectsWithTag("PlatformSpawner");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
