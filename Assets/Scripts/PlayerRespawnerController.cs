using UnityEngine;

public class PlayerRespawnerController : MonoBehaviour
{
    [SerializeField] private Finder finder;
    [SerializeField] private GameObject parentPlayer;
    void Update()
    {
        foreach (GameObject player in finder.GetChilds(parentPlayer))
        {
            if (!player.activeSelf)
            {
                RespawnPlayer(player);
            }
        }
    }

    public void RespawnPlayer(GameObject player)
    {
        player.SetActive(true);
        player.transform.position = new Vector2(transform.position.x, transform.position.y);
    }
}
