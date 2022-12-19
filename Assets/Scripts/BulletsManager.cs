using System.Collections;
using UnityEngine;
using teams;

public class BulletsManager : MonoBehaviour
{
    [SerializeField] private Teams team;
    [SerializeField] private GameObject source;
    [SerializeField] private float bulletSpeed = 75;
    [SerializeField] private float maxTimeActive = 4;
    [SerializeField] private float timeActive = 0;
    [SerializeField] private GameObject parentPlayer;
    private Finder finder;
    private bool isHomingBullet = false;

    [SerializeField] private float velocityThreshold = 7;

    [SerializeField] private float  minVelocity = 4;

    void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isActiveAndEnabled)
        {
            timeActive += Time.deltaTime;
        }

        if (timeActive >= maxTimeActive)
        {
            gameObject.SetActive(false);
            timeActive = 0f;
        }


        if (isHomingBullet)
        {
            GetComponent<Renderer>().material.color = Color.yellow;
            foreach (GameObject player in finder.GetPlayers())
            {
                if (player != source)
                {
                    transform.position = Vector3.MoveTowards(transform.position, player.transform.position, bulletSpeed * Time.deltaTime);
                }
            }
        }
    }

    public void Shoot(GameObject gun, bool homing)
    {
        GetComponent<Rigidbody2D>().transform.position = gun.transform.position;
        GetComponent<Rigidbody2D>().transform.LookAt(gun.transform.position);
        GetComponent<Rigidbody2D>().transform.forward = gun.transform.forward;
        GetComponent<Rigidbody2D>().velocity = new Vector2(transform.forward.x, transform.forward.y) * bulletSpeed;
        limitVelocity(GetComponent<Rigidbody2D>().velocity);
        SetHoming(homing);
    }

    public void limitVelocity(Vector3 velocity) {
        if(velocity.x <=  minVelocity) {
            velocity = new Vector2(minVelocity, velocity.y);
        }
        if(velocity.y <=  minVelocity) {
            velocity = new Vector2(velocity.x,  minVelocity);
        }
        if(velocity.x > velocityThreshold) {
            velocity = new Vector2(velocityThreshold, velocity.y);
        }
        if(velocity.y > velocityThreshold) {
            velocity = new Vector2(velocity.x, velocityThreshold);
        }
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

    public Teams GetTeam()
    {
        return team;
    }
}