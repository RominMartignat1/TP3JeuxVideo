using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsManager : MonoBehaviour
{

    [SerializeField] private GameObject source;
    [SerializeField] private GameObject bulletSpawnPoint;

    [SerializeField] private float bulletSpeed = 75;
    [SerializeField] private float maxTimeActive = 4;

    //bullet team enum
    public enum BulletTeam
    {
        Team1,
        Team2
    }

    //bullet team
    [SerializeField] private BulletTeam bulletTeam;

    //bullet team
    //public BulletTeam GetBulletTeam { get { return bulletTeam; } }
    //get bullet team
    public BulletTeam GetBulletTeam()
    {
        return bulletTeam;
    }

    private float timeActive = 0f;

    void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if(isActiveAndEnabled)
        {
            timeActive += Time.deltaTime;
        }

        if(timeActive >= maxTimeActive)
        {
            gameObject.SetActive(false);
            timeActive = 0f;
        }

    }

    private void OnEnable()
    {
        GetComponent<Rigidbody2D>().transform.position = bulletSpawnPoint.transform.position;
        GetComponent<Rigidbody2D>().transform.forward = bulletSpawnPoint.transform.forward;
        GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.transform.forward.x, gameObject.transform.forward.y) * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        //if touches a player disable bullet after 0.5 seconds
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DisableBullet());
        }

        /*if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.name.Contains("Pickup") && !collision.gameObject.name.Contains("Bullet"))
        {
            Debug.Log("touched something" + collision.name);

            gameObject.SetActive(false);
        }*/
    }

    //disable bullet after 0.5 seconds
    IEnumerator DisableBullet()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }


    public GameObject GetSource()
    {
        return this.source;
    }
}
