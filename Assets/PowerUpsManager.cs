using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{
    [SerializeField]private Finder finder;
    private GameObject[] childPowerUpsList;
    private float spawnCooldown = 0f;
    private float spawnCooldownMax;
    GameObject powerUp;

    void Start()
    {
        childPowerUpsList = finder.GetChilds(this.gameObject);
    }

    void Update()
    {
        if (spawnCooldown <= 0)
        {
            spawnCooldownMax = Random.Range(7f, 12f);
            spawnCooldown = spawnCooldownMax;
            SpawnPowerUp();
        }
        else
        {
            spawnCooldown -= Time.deltaTime;
        } 
    }

    private void SpawnPowerUp()
    {
        //powerUp = finder.GetRandomInactiveChild(this.gameObject);
        //if (powerUp != null)
        //{
        //    powerUp.SetActive(true);
        //}
    }
}
