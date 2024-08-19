using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{
    public GameObject[] powerUps;
    [SerializeField] private Finder finder;
    private GameObject[] childPowerUpsList;
    private float spawnCooldown = 0f;
    private float spawnCooldownMax;
    private int maxPowerUpsPerType = 20;

    void Start()
    {
        childPowerUpsList = finder.GetChilds(gameObject);
        InitPowerUps();
    }

    private void InitPowerUps()
    {
        for (int i = 0; i < maxPowerUpsPerType; i++)
        {
            for (int j = 0; j < powerUps.Length; j++)
            {
                Instantiate(powerUps[j], Vector2.zero, Quaternion.identity, transform);
            }
        }
    }

    void Update()
    {
        if (spawnCooldown <= 4)
        {
            spawnCooldownMax = Random.Range(3f, 7f);
            spawnCooldown = spawnCooldownMax;
            SpawnPowerUp();
        }
        else
        {
            spawnCooldown -= Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(CompareTag("Despawner")) {
            gameObject.SetActive(false);
        }
    }

    private void SpawnPowerUp()
    {
        GameObject powerUp = finder.GetRandomInactiveChild(gameObject);
        if (powerUp != null)
        {
            powerUp.SetActive(true);
        }
    }
}
