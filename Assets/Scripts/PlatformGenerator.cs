using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    [Header("Platforms")]
    public GameObject shortPlatform;
    public GameObject mediumPlatform;
    public GameObject longPlatform;
    private Finder finder;
    private List<GameObject> platforms = new List<GameObject>();
    private float heightBetweenPlatforms = 1.5f;
    public int distBeforeSpawn = 10;
    public int maxPlatforms = 20;
    public float platformMinX = -9.15f;
    public float platformMaxX = 4.6f;
    public float platformY = -1.5f;
    private int platformLength;

    private void Awake()
    {
        InitPlatforms();
    }

    void Start()
    {
        finder = GetComponent<Finder>();
    }
    void Update()
    {
        if (platformY - finder.GetPositionOfHighestPlayer().y < distBeforeSpawn)
        {
            SpawnPlatforms();
        }
    }

    private void InitPlatforms()
    {
        for (int i = 0; i < maxPlatforms; i++)
        {
            GameObject prefab = SelectRandomPlatform();
            Vector2 startPosition = new Vector2(Random.Range(-platformMaxX, platformMaxX - platformLength), platformY);
            GameObject platform = Instantiate(prefab, startPosition, Quaternion.identity, transform);
            platforms.Add(platform);
            platformY += heightBetweenPlatforms;
        }
    }

    private void SpawnPlatforms()
    {
        platforms[0].transform.position = new Vector2(Random.Range(-platformMaxX, platformMaxX - platformLength), platformY);
        if (!platforms[0].activeInHierarchy)
        {
            platforms[0].SetActive(true);
        }
        platformY += heightBetweenPlatforms;
        GameObject newPlatform = platforms[0];
        platforms.RemoveAt(0);
        platforms.Add(newPlatform);
    }

    private GameObject SelectRandomPlatform()
    {
        int platformType = Random.Range(1, 3);
        switch (platformType)
        {
            case 1:
                platformLength = 4;
                return shortPlatform;
            case 2:
                platformLength = 7;
                return mediumPlatform;
            case 3:
                platformLength = 11;
                return longPlatform;
            default:
                return shortPlatform;
        }
    }

}
