using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatformGenerator : MonoBehaviour
{
    private Tilemap tilemap;
    public TileBase leftPlatform;
    public TileBase centerPlatform;
    public TileBase rightPlatform;
    private Finder finder;
    private List<GameObject> platforms;
    private float heightBetweenPlatforms = 1.5f;
    private int distBetweenPlatforms = 2;
    public int minPlatformLength = 3;
    public int maxPlatformLength = 10;
    public float distBeforeSpawnPlatform = 10f;
    public int maxPlatforms = 10;
    public int MaxPlatformX = 12;
    public int platformY;

    private void Awake()
    {
        InitBlocks();
    }

    void Start()
    {
        finder = GetComponent<Finder>();
        tilemap = GetComponent<Tilemap>();
    }
    void Update()
    {
        if (platformY - finder.GetPositionOfHighestPlayer().y < distBeforeSpawnPlatform)
        {
            SpawnBlocks();
        }
    }

    private void SpawnBlocks()
    {
        platformY += distBetweenPlatforms;
        GameObject newPlatform = platforms[0];
        platforms.RemoveAt(0);
        platforms.Add(newPlatform);
    }

    private void InitBlocks()
    {
        for (int i = 0; i < maxPlatforms; i++)
        {
            CreatePlatform();
            platformY += distBetweenPlatforms;
        }
    }

    private void CreatePlatform()
    {
        Vector3Int startPosition = new Vector3Int(Random.Range(-9, 9), platformY);
        tilemap.SetTile(startPosition, leftPlatform);
        int platformLength = GetPlatformLength(startPosition);
        for (int i = startPosition.x + 1; i < platformLength; startPosition.x++)
        {
            tilemap.SetTile(startPosition, leftPlatform);
        }
        startPosition.x += 1;
        tilemap.SetTile(startPosition, rightPlatform);
    }

    private int GetPlatformLength(Vector3Int startPosition)
    {
        int maxX = MaxPlatformX - startPosition.x;
        if (startPosition.x > 0)
        {
            if (maxX > maxPlatformLength)
            {
                maxX = maxPlatformLength;
            }
        }
        else
        {
            maxX = maxPlatformLength;
        }
        return Random.Range(minPlatformLength, maxX);
    }

}
