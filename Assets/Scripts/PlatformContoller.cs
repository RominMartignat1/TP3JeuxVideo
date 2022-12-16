using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformContoller : MonoBehaviour
{
    //private List<GameObject> children = new List<GameObject>();
    private GameObject[] blocks;
    private float moveSpeed = 5f;
    private GameObject platformGeneratorManager;

    public const int MAX_PLATFORM_COUNT = 11;
    private Finder finder;

    void Start()
    {
        blocks = finder.GetChilds(gameObject);
        platformGeneratorManager = GameObject.FindGameObjectWithTag("PlatformSpawner");
        finder = FindObjectOfType<Finder>();
    }
    private void OnEnable()
    {
        randomizePlatform();
    }

    public void randomizePlatform()
    {
        if (blocks == null) return;
        foreach (GameObject block in blocks)
        {
            block.SetActive(true);
        }
        int platformStart = Random.Range(0, MAX_PLATFORM_COUNT - 2);
        int platformEnd = Random.Range(platformStart, MAX_PLATFORM_COUNT);

        for(int i = 0; i < MAX_PLATFORM_COUNT; i++)
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Despawner")
        {
            gameObject.SetActive(false);
        }
    }
}
