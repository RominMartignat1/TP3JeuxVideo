using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using teams;

public class ShotsController : MonoBehaviour
{
    public GameObject gun;
    public GameObject bullet;
    private List<GameObject> bullets = new List<GameObject>();
    private GameObject player;
    public float bulletSpeed = 75f;
    private float shotcooldown = 0f;
    private float shootingCooldown = 2f;
    [SerializeField] private Finder finder;
    public int maxBulletsEachTeam = 40;
    AudioSource soundSource;
    private int homingBulletExtraCount = 0;
    private Teams team;

    void Start()
    {
        //gun = GetChildWithTag(gameObject, "Bullets");
        //bullets = gun.transform.GetChild(0).gameObject;//GetChildsSpawnerActive(gun.transform.GetChild(0).gameObject);
        gun = GetChildWithTag(gameObject, "Gun");
        soundSource = gameObject.GetComponent<AudioSource>();
        team = GetComponent<PlayerController>().GetTeam();
    }

    void Awake()
    {
        InitBullets();
    }

    private void InitBullets()
    {
        for (int i = 0; i < maxBulletsEachTeam; i++)
        {
            GameObject b = Instantiate(bullet);
            b.gameObject.SetActive(false);
            bullets.Add(b);
        }
    }

    public GameObject[] GetChildsSpawnerActive(GameObject parent)
    {
        GameObject[] array = new GameObject[parent.transform.childCount];
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            //if (parent.transform.GetChild(i).gameObject.activeSelf)
            //{
            array[i] = parent.transform.GetChild(i).gameObject;
            //}
        }
        return array;
    }


    public GameObject GetChildWithTag(GameObject parent, string tag)
    {
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            if (parent.transform.GetChild(i).gameObject.tag == tag)
            {
                return parent.transform.GetChild(i).gameObject;
            }
            else
            {
                GameObject child = GetChildWithTag(parent.transform.GetChild(i).gameObject, tag);
                if (child != null)
                {
                    return child;
                }
            }
        }
        return null;
    }


    void Update()
    {
        if (!PauseManager.GameIsPaused || !GameSceneManager.GameIsEnded)
        {
            shotcooldown = DecreaseCooldown(shotcooldown);
            if (Input.GetButtonDown("Fire1") && team == Teams.Blue || Input.GetButtonDown("Fire1P2") && team == Teams.Red)
            {
                if (shotcooldown <= 0)
                {
                    soundSource.PlayOneShot(SoundManager.Instance.FireBulletSound);
                    GameObject bullet = finder.GetFirstAvailableObject(bullets);
                    SpawnBullet(bullet);
                }
            }
            if (Input.GetButtonDown("Fire2") && team == Teams.Blue || Input.GetButtonDown("Fire2P2") && team == Teams.Red)
            {
                if (homingBulletExtraCount > 0)
                {
                    if (shotcooldown <= 0)
                    {
                        homingBulletExtraCount--;
                        GameObject bullet = finder.GetFirstAvailableObject(bullets);
                        SpawnBullet(bullet, true);
                    }
                }
            }
        }
    }

    private void SpawnBullet(GameObject bullet, bool homing = false)
    {
        if (bullet != null)
        {
            soundSource.PlayOneShot(SoundManager.Instance.FireBulletSound);
            bullet.GetComponent<BulletsManager>().Shoot(gun, homing);
            shotcooldown = shootingCooldown;
        }
    }


    public void addHommingBullet()
    {
        homingBulletExtraCount += 3;
    }


    private float DecreaseCooldown(float cooldown)
    {
        if (cooldown > 0)
            return cooldown -= Time.deltaTime;
        else
            return 0;
    }

}
