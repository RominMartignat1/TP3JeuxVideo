using System.Collections;
using UnityEngine;
using teams;

public class PlayerController : MonoBehaviour
{
    private int isGrounded;
    [SerializeField] Teams team;
    private Rigidbody2D rigidBody;
    private Animator animator;
    private float horizontal;
    private int collisionThreshold;
    private float jumpTimer;
    private float jumpIntensity;
    private float jumpPower;
    private bool hasDied;
    private bool canPlayerMove;
    private float deathTimer;
    private float acceleration;
    public bool isRespawning = false;
    [SerializeField] private Finder finder;
    [SerializeField] private GameSceneManager sceneManager;
    AudioSource soundSource;
    private int doubleJumpExtraCounter = 0;
    private int doubleJumpCounter = 0;
    [SerializeField] private float knockbackHitForce = 3;
    private bool isInvulnerable = false;

    public PlayerController()
    {
        canPlayerMove = true;
        acceleration = 0.0f;
    }
    private void Awake()
    {
        soundSource = GetComponent<AudioSource>();
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!PauseManager.GameIsPaused || !GameSceneManager.GameIsEnded)
        {
            if (canPlayerMove)
            {
                rigidBody.velocity = new Vector2(horizontal + acceleration, rigidBody.velocity.y + jumpPower + jumpIntensity);
                jumpPower = 0.0f;
                animator.SetBool("Is Jumping", false);
            }
            if (jumpIntensity <= 10.0f)
            {
                jumpIntensity = 0.0f;
            }
        }

    }

    private void OnDisable()
    {
        rigidBody.velocity = new Vector2(0.0f, 0.0f);
        soundSource.PlayOneShot(SoundManager.Instance.PlayerDeath);
        hasDied = true;
        canPlayerMove = false;
        acceleration = 0.0f;
        jumpIntensity = 0.0f;
        jumpPower = 0.0f;
    }

    private void OnEnable()
    {
        if (hasDied)
        {
            respawnSequence();
        }
    }

    private void Update()
    {
        if (!PauseManager.GameIsPaused)
        {
            if (canPlayerMove)
            {
                ManageMovement();
                bool flipped = horizontal < 0f;
                transform.rotation = Quaternion.Euler(0, flipped? 180: 0, transform.rotation.z);
                animator.SetFloat("Speed",System.Math.Abs(horizontal));
            }
        }
    }

    private void ManageMovement()
    {
        if (team == Teams.Blue)
        {
            horizontal = Input.GetAxis("Horizontal");
        }
        else if (team == Teams.Red)
        {
            horizontal = Input.GetAxis("HorizontalP2");
        }
        if (isRespawning)
        {
            horizontal *= 1.5f;
        }
        else
        {
            if (horizontal > 0)
            {
                if (acceleration < 4.0f)
                {
                    acceleration += 0.1f;
                }
            }
            else if (horizontal < 0)
            {
                if (acceleration > -4.0f)
                {
                    acceleration -= 0.1f;
                }
            }
            else
            {
                if (acceleration > 0)
                {
                    acceleration -= 0.2f;
                }
                else if (acceleration < 0)
                {
                    acceleration += 0.2f;
                }
            }

          
            
            if (IsGoingToJump())
            {
                animator.SetBool("Is Jumping", true);
                jumpPower = 3.0f;
                if (System.Math.Abs(acceleration) > 1.0f && System.Math.Abs(acceleration) < 2.0f)
                {
                    soundSource.PlayOneShot(SoundManager.Instance.PlayerJumpMid);
                    jumpIntensity = 6.5f;
                }
                else if (System.Math.Abs(acceleration) > 2.0f && System.Math.Abs(acceleration) < 3.0f)
                {
                    soundSource.PlayOneShot(SoundManager.Instance.PlayerJumpLow);
                    jumpIntensity = 2.0f;
                }
                else if (System.Math.Abs(acceleration) > 3.0f)
                {
                    soundSource.PlayOneShot(SoundManager.Instance.PlayerJumpHigh);
                    jumpIntensity = 5.0f;
                }

                if (doubleJumpExtraCounter > 0)
                {

                    if (IsGrounded())
                    {
                        doubleJumpCounter = 0;
                    }
                    else
                    {
                        doubleJumpCounter++;
                    }

                    if (doubleJumpCounter > 1)
                    {
                        doubleJumpExtraCounter--;
                    }
                }
            }
        }
    }

    private bool IsGoingToJump()
    {
        return ((Input.GetButtonDown("Jump") && team == Teams.Blue) || (Input.GetButtonDown("JumpP2") && team == Teams.Red)) && (IsGrounded() ||
             (doubleJumpExtraCounter > 0 && doubleJumpCounter < 1));
    }
    private void respawnSequence()
    {
        soundSource.PlayOneShot(SoundManager.Instance.PlayerDeath);
        GetComponent<Rigidbody2D>().isKinematic = true;
        canPlayerMove = true;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        hasDied = false;
        isRespawning = true;
        StartCoroutine(Blink(0.5f));
        isRespawning = true;
        isRespawning = false;
        canPlayerMove = true;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        GetComponent<Rigidbody2D>().isKinematic = false;
    }

    public bool isPlayerDeadOrRespawning()
    {
        return hasDied || isRespawning;
    }

    private void OnCollisionEnter2D(Collision2D Collider2D)
    {
        if (!hasDied && !isRespawning)
        {
            if (Collider2D.gameObject.tag == "GreenMushroom")
            {
                sceneManager.AddLife(team);
                soundSource.PlayOneShot(SoundManager.Instance.PowerUp);
            }

            if (Collider2D.gameObject.tag == "wall")
            {
                float oldAcceleration = acceleration;
                if (rigidBody.velocity.x > 0)
                {
                    acceleration = 0.0f;
                    acceleration = 5.0f;
                }
                else
                {
                    acceleration = 0.0f;
                    acceleration = -5.0f;
                }
            }
            if (Collider2D.gameObject.tag == "Player")
            {

                Collider2D.gameObject.GetComponent<PlayerController>().acceleration = acceleration;
            }
        }
    }

    public float GetPlayerAcceleration()
    {
        return acceleration;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasDied && !isRespawning)
        {
            if (collision.gameObject.tag == "Despawner")
            {
                sceneManager.SubstractLife(team);
                gameObject.SetActive(false);
            }

            if (collision.gameObject.tag == "BulletPowerUp")
            {
                this.GetComponent<ShotsController>().addHommingBullet();
            }

            if (collision.gameObject.tag == "DoubleJumpBonus")
            {
                doubleJumpExtraCounter += 5;
            }

            if (collision.gameObject.tag == "Bullet")
            {
                if (collision.gameObject.GetComponent<BulletsManager>().GetTeam() == team && !isInvulnerable)
                {
                    return;
                }
                else
                {
                    StartCoroutine(Blink(0.1f));
                    if (collision.transform.position.x > transform.position.x)
                    {
                        acceleration = -5.0f;
                    }
                    else if (collision.transform.position.x < (double)transform.position.x)
                    {
                        acceleration = 5.0f;
                    }
                    else
                    {
                        acceleration = -5.0f;
                    }
                    rigidBody.AddForce(new Vector2(Mathf.Abs(collision.gameObject.GetComponent<Rigidbody2D>().velocity.x) * knockbackHitForce, Mathf.Abs(collision.gameObject.GetComponent<Rigidbody2D>().velocity.y) * knockbackHitForce), ForceMode2D.Impulse);
                    isInvulnerable = true;
                }
            }
            if (collision.gameObject.tag == "Block")
            {
                isGrounded++;
                collisionThreshold = 0;
            }
        }

    }

    private IEnumerator Blink(float delay)
    {
        for (int i = 0; i < 3; i++)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(delay);
            GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(delay);
        }
        if(isInvulnerable) {
            isInvulnerable = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Block") && !IsGrounded())
        {
            if (collision.transform.position.x > transform.position.x)
            {
                collisionThreshold = 1;
            }
            else if (collision.transform.position.x < (double)transform.position.x)
            {
                collisionThreshold = -1;
            }
            else
            {
                collisionThreshold = 0;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Block" && !IsGrounded() && collisionThreshold != 0)
        {
            collisionThreshold = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Block")
        {
            isGrounded--;
        }
    }

    public bool IsGrounded()
    {
        return isGrounded != 0;
    }

    public Teams GetTeam()
    {
        return team;
    }


}
