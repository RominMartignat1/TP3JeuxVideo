using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [Header("Walls")]
    public GameObject leftWallPrefab;
    public GameObject rightWallPrefab;
    public float wallTall = 11.5f;
    public float distanceBeforeSpawn = 10f;
    public int initialWalls = 6;
    public float wallX = 9.5f;
    private float wallY = 11.5f;
    private List<GameObject> leftWalls;
    private List<GameObject> rightWalls;

    [Header("Platforms")]
    public GameObject shortPlatform;
    public GameObject mediumPlatform;
    public GameObject longPlatform;
    private Finder finder;
    private List<GameObject> platforms;
    private float heightBetweenPlatforms = 1.5f;
    private int distBetweenPlatforms = 2;
    public int minPlatformLength = 3;
    public int maxPlatformLength = 10;
    public float distBeforeSpawn = 10f;
    public int maxPlatforms = 10;
    public int platformMaxX = 12;
    private int platformY;
    private int platformLength = 0;

    private void Awake()
    {
        InitSideWalls();
        InitPlatforms();
    }

    void Start()
    {
        finder = GetComponent<Finder>();
    }
    void Update()
    {
        if (wallY - finder.GetPositionOfHighestPlayer().y < distanceBeforeSpawn)
        {
            SpawnWall(leftWalls);
            SpawnWall(rightWalls);
        }
        if (platformY - finder.GetPositionOfHighestPlayer().y < distBeforeSpawn)
        {
            SpawnPlatforms();
        }

    }

    private void InitSideWalls()
    {
        for (int i = 0; i < initialWalls; ++i)
        {
            Vector2 position = new Vector2(-9.5f, wallY);
            GameObject leftWall = Instantiate(leftWallPrefab, position, Quaternion.identity, transform);
            position.x = -position.x;
            GameObject rightWall = Instantiate(rightWallPrefab, position, Quaternion.identity, transform);
            leftWalls.Add(leftWall);
            rightWalls.Add(leftWall);
            wallY += wallTall;
        }
    }

    private void InitPlatforms()
    {
        for (int i = 0; i < maxPlatforms; i++)
        {
            Vector2 startPosition = new Vector2(Random.Range(-12, platformMaxX - platformLength), platformY);
            GameObject prefab = SelectRandomPlatform();
            GameObject platform = Instantiate(prefab, startPosition, Quaternion.identity, transform);
            platforms.Add(platform);
            platformY += distBetweenPlatforms;
        }
    }

    private void SpawnWall(List<GameObject> walls)
    {
        walls[0].transform.position = new Vector2(0, wallY);
        walls[0].transform.position = new Vector2(0, wallY);
        wallY += wallTall;

        GameObject temp = leftWalls[0];
        walls.RemoveAt(0);
        walls.Add(temp);
    }

    private void SpawnPlatforms()
    {
        CreatePlatform();
        platformY += distBetweenPlatforms;
        GameObject newPlatform = platforms[0];
        platforms.RemoveAt(0);
        platforms.Add(newPlatform);
    }



    private void CreatePlatform()
    {
        GameObject platform = SelectRandomPlatform();
        platforms[0].transform.position = new Vector2(Random.Range(-12, platformMaxX - platformLength), platformY);
        platformY += distBetweenPlatforms;

        GameObject temp = platforms[0];
        platforms.RemoveAt(0);
        platforms.Add(temp);
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
