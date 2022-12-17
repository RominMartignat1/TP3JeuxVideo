using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenMushroomController : MonoBehaviour
{
    private float timeActive = 0f;
    private float maxTimeActive = 10f;
    private float speed = 2.5f;
    private Vector2 currentDirection = Vector2.left;

    void Start()
    {
    }

    private void OnEnable()
    {
        transform.position = new Vector2(Random.Range(-5f, 5f), 2f);
    }

    private void OnDisable()
    {
    }

    void Update()
    {
        if (gameObject.activeSelf)
        {
            transform.Translate(currentDirection * speed * Time.deltaTime);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            currentDirection *= -1;
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
