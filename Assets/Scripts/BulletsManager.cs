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

    private GameObject[] players;

    private bool isRegularBullet = true;
    private bool isHomingBullet = false;

    public enum BulletTeam
    {
        Team1,
        Team2
    }

    [SerializeField] private BulletTeam bulletTeam;

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


        if (isHomingBullet)
        {
            GetComponent<Renderer>().material.color = Color.red;
            if(players == null)
            {
                players = GameObject.FindGameObjectsWithTag("Player");
            }
            else
            {
                foreach (GameObject player in players)
                {
                    if (player != source)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, bulletSpeed * Time.deltaTime);
                    }
                }
                
            }
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
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DisableBullet());
        }
    }

    IEnumerator DisableBullet()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }

    public void SetHoming(bool isHomingBullet)
    {
        this.isHomingBullet = isHomingBullet;
    }


    public GameObject GetSource()
    {
        return this.source;
    }
}