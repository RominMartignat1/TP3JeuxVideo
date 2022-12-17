using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BackgroundGenerator : MonoBehaviour
{
    private List<GameObject> backgroundTiles;
    private Finder finder;
    private GameObject background;
    public float distBeforeSpawn = 10f;
    public float heightBetween = 10f;
    private float backgroundY;
    void Start()
    {
        finder = FindObjectOfType<Finder>();
        backgroundTiles = finder.GetChilds(gameObject).ToList();
    }

    void Update()
    {
        if (backgroundTiles[4].transform.position.y - finder.GetPositionOfHighestPlayer().y < distBeforeSpawn)
        {
            float newHeight = backgroundTiles[4].transform.position.y + heightBetween;
            backgroundTiles[0].transform.position = new Vector2(backgroundTiles[0].transform.position.x, newHeight);
            GameObject newBackground = backgroundTiles[0];
            backgroundTiles.RemoveAt(0);
            backgroundTiles.Add(newBackground);
        }
    }
}
