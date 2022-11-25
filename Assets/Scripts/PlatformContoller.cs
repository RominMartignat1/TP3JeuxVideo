using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformContoller : MonoBehaviour
{
    //get every children in a list
    //private List<GameObject> children = new List<GameObject>();

    private GameObject[] blocks;
    private float moveSpeed = 5f;
    private GameObject platformGeneratorManager;
    private Finder finder;


    // Start is called before the first frame update
    void Start()
    {
        blocks = GetEveryBlock(gameObject);
        platformGeneratorManager = GameObject.FindGameObjectWithTag("PlatformSpawner");
        finder = FindObjectOfType<Finder>();
        Debug.Log(finder.enabled + "Finder is active");
    }


    private void OnEnable()
    {

        randomizePlatform();   

    }


    private GameObject[] GetEveryBlock(GameObject parent)
    {
        GameObject[] array = new GameObject[parent.transform.childCount];
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            array[i] = parent.transform.GetChild(i).gameObject;
        }

        return array;
    }

    public void randomizePlatform()
    {
        if (blocks == null) return;
        foreach (GameObject block in blocks)
        {
            block.SetActive(true);
        }

        int platformStart = Random.Range(0,11);
        int platformEnd = Random.Range(platformStart,11);

        for(int i = 0; i < 11; i++)
        {
            if(i >= platformStart && i <= platformEnd)
            {
                blocks[i].SetActive(true);
            }
            else
            {
                blocks[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Despawner")
        {
            Debug.Log("je touche le despawner");
            gameObject.SetActive(false);
        } 
    }
}
