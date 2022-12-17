using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotsController : MonoBehaviour
{

    private GameObject gun;
    private GameObject bullets; 
    private GameObject player;
    private float bulletSpeed = 10f;
    private float bulletLifeTime = 2f;
    private float timeBetweenShots = 0.5f;
    private float timeSinceLastShot = 0f;
    private float bulletDamage = 1f;
    private float bulletSpread = 0.1f;
    private float bulletSize = 0.1f;
    private float bulletSpeedMultiplier = 1f;
    private float bulletDamageMultiplier = 1f;
    private float bulletSpreadMultiplier = 1f;
    private float bulletSizeMultiplier = 1f;
    private float bulletLifeTimeMultiplier = 1f;
    private float bulletSpeedMultiplierDuration = 0f;
    private float bulletDamageMultiplierDuration = 0f;
    private float shotcooldown = 0f;
    private float shootingCooldown = 2f;
    [SerializeField] private Finder finder;


    private int homingBulletExtraCount = 0;






    void Start()
    {
        //gun = GetChildWithTag(gameObject, "Bullets");
        //bullets = gun.transform.GetChild(0).gameObject;//GetChildsSpawnerActive(gun.transform.GetChild(0).gameObject);
        gun = GetChildWithTag(gameObject, "Gun");
        bullets = GameObject.FindGameObjectWithTag("Player1Bullets");
        //Debug.Log(bullets.Length + "bullets");
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
        shotcooldown = DecreaseCooldown(shotcooldown);
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Fire1");
            if(shotcooldown <= 0)
            {
                Debug.Log("Fire1 AGAIN");
                GameObject bullet = finder.GetFirstAvailableObject(bullets);
                Debug.Log(bullet);
                if (bullet != null)
                {
                    bullet.SetActive(true);
                    bullet.GetComponent<BulletsManager>().SetHoming(false);
                    shotcooldown = shootingCooldown;
                }
            }
        }
        if (Input.GetButtonDown("Fire2"))
        {
            //if player has homming bullets
            if (homingBulletExtraCount > 0)
            {
                if(shotcooldown <= 0)
                {
                    homingBulletExtraCount--;
                    GameObject bullet = finder.GetFirstAvailableObject(bullets);
                    if (bullet != null)
                    {
                        bullet.SetActive(true);
                        bullet.GetComponent<BulletsManager>().SetHoming(true);
                        shotcooldown = shootingCooldown;
                    }
                }
            }
        }
    }


    public void addHommingBullet()
    {
        homingBulletExtraCount+= 3;
    }


    private float DecreaseCooldown(float cooldown)
    {
        if (cooldown > 0)
            return cooldown -= Time.deltaTime;
        else
            return 0;
    }

}
