using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    [SerializeField] private AudioClip playerJumpHigh;
    [SerializeField] private AudioClip playerJumpMid;
    [SerializeField] private AudioClip playerJumpLow;
    [SerializeField] private AudioClip playerKilled;
    [SerializeField] private AudioClip gameMusic;
    [SerializeField] private AudioClip bulletSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip powerUpSound;


    public static SoundManager Instance { get { return instance; } }
    public AudioClip PlayerJumpHigh { get { return playerJumpHigh; } }
    public AudioClip PlayerJumpMid { get { return playerJumpMid; } }
    public AudioClip PlayerJumpLow { get { return playerJumpLow; } }
    public AudioClip GameMusic { get { return gameMusic; } }
    public AudioClip FireBulletSound { get { return bulletSound; } }
    public AudioClip PlayerDeath { get { return deathSound; } }
    public AudioClip PowerUp { get { return powerUpSound; } }

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
            Destroy(gameObject);
    }
}