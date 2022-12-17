using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [Header("Platforms")]
    public GameObject shortPlatform;
    public GameObject mediumPlatform;
    public GameObject longPlatform;
    private Finder finder;
    private List<GameObject> platforms = new List<GameObject>();
    private float heightBetweenPlatforms = 1.5f;
    private int distBetweenPlatforms = 2;
    public int minPlatformLength = 3;
    public int maxPlatformLength = 10;
    public float distBeforeSpawn = 10f;
    public int maxPlatforms = 10;
    public int platformMaxX = 12;
    public int platformY;
    private int platformLength = 0;

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
            Vector2 startPosition = new Vector2(Random.Range(-12, platformMaxX - platformLength), platformY);
            Debug.Log(startPosition);
            GameObject prefab = SelectRandomPlatform();
            GameObject platform = Instantiate(prefab, startPosition, Quaternion.identity, transform);
            platforms.Add(platform);
            platformY += distBetweenPlatforms;
        }
    }

    private void SpawnPlatforms()
    {
        platforms[0].transform.position = new Vector2(Random.Range(-12, platformMaxX - platformLength), platformY);
        if (!platforms[0].activeInHierarchy)
        {
            platforms[0].SetActive(true);
        }
        platformY += distBetweenPlatforms;
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
