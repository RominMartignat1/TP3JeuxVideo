using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExtraManager : MonoBehaviour
{
    private float timeActive = 0f;
    private float maxTimeActive = 10f;
    private Vector2 currentDirection = Vector2.left;

    void Start()
    {
    }

    private void OnEnable()
    {
        transform.position = new Vector2(Random.Range(-6f, 6f), 2f);
    }

    void Update()
    {
        if (gameObject.activeSelf)
        {
            if (timeActive >= maxTimeActive)
            {
                gameObject.SetActive(false);
                timeActive = 0f;
            }
            else
            {
                timeActive += Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
            timeActive = 0f;
        }
    }
}
