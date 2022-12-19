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
        gun = finder.GetChildWithTag(gameObject, "Gun");
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

    bool IsFiring(int shotNb) {
        Debug.Log(Input.GetAxisRaw("Fire" + shotNb));
        return Input.GetButtonDown("Fire" + shotNb) && team == Teams.Blue || Input.GetButtonDown("Fire" + shotNb + "P2") && team == Teams.Red || Input.GetAxisRaw("Fire" + shotNb) != 0 && team == Teams.Blue || Input.GetAxisRaw("Fire" + shotNb + "P2") != 0 && team == Teams.Red;
    }

    void Update()
    {
        if (!PauseManager.GameIsPaused || !GameSceneManager.GameIsEnded)
        {
            shotcooldown = DecreaseCooldown(shotcooldown);
            if (IsFiring(1))
            {
                if (shotcooldown <= 0)
                {
                    GameObject bullet = finder.GetFirstAvailableObject(bullets);
                    SpawnBullet(bullet);
                }
            }
            if (IsFiring(2))
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
