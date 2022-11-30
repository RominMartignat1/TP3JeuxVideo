using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenMushroomController : MonoBehaviour
{
    private float timeActive = 0f;
    private float maxTimeActive = 10f;
    private float speed = 5f;

    void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        timeActive = 10f;
        transform.position = new Vector2(Random.Range(-8f, 8f), 5f);
    }

    void Update()
    {
        if (gameObject.activeSelf)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
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


    //on collision enter 2d if its a player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            transform.Rotate(0, 0, 180);
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
