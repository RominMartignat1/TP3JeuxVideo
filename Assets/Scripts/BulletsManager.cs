using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsManager : MonoBehaviour
{

    [SerializeField] private GameObject source;
    [SerializeField] private float bulletSpeed = 75;
    [SerializeField] private float maxTimeActive = 4;

    private GameObject[] players;

    private bool isHomingBullet = false;

    public enum BulletTeam
    {
        Blue,
        Red
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
            GetComponent<Renderer>().material.color = Color.yellow;
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

    public void Shoot(GameObject gun, bool homing) {
            GetComponent<Rigidbody2D>().transform.position = gun.transform.position;
            GetComponent<Rigidbody2D>().transform.forward = gun.transform.forward;
            GetComponent<Rigidbody2D>().velocity = new Vector2(transform.forward.x, transform.forward.y) * bulletSpeed;
            SetHoming(homing);
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