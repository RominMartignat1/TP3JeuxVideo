using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float timeScore = 120;
    [SerializeField] private float speed = 2;
    [SerializeField] private float chargedJumpStrength = 20;
    [SerializeField] private int blockCollisions;
    private AudioSource playerSounds;
    private Rigidbody2D rigidBody;
    private float horizontalInput;
    private int collisionFailSafe;
    private float deathTimer;
    private float jumpTimer;
    private float coyoteTime = 0.2f;
    private float chargedJump;
    private float jumpForce;
    private bool canMove = true;
    private bool isDead = false;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerSounds = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            rigidBody.velocity = new Vector2(horizontalInput * speed, rigidBody.velocity.y + jumpForce + chargedJump);
            jumpForce = 0;
            chargedJump = 0;
        }
    }

    private void Update()
    {
        if (isDead)
        {
            deathTimer += Time.deltaTime;
            float playerScale = 1 - deathTimer;
            transform.localScale = new Vector3(playerScale, playerScale, playerScale);
        }
        if (canMove)
        {
            Move();
        }
        timeScore = DecreaseTimer(timeScore);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HardBlock") || collision.gameObject.CompareTag("Platform"))
        {
            collisionFailSafe = 0;
            coyoteTime = 0.2f;
            blockCollisions++;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("HardBlock") && !IsGrounded())
        {
            if (collision.transform.position.x > transform.position.x)
            {
                collisionFailSafe = 1;
            }
            else if (collision.transform.position.x < transform.position.x)
            {
                collisionFailSafe = -1;
            }
            else
            {
                collisionFailSafe = 0;
            }
        }
    }

    private void Move()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        if (!IsGrounded() && coyoteTime > 0)
        {
            coyoteTime -= Time.deltaTime;
        }
        if (collisionFailSafe < 0 && horizontalInput < 0)
        {
            horizontalInput = 0;
        }
        else if (collisionFailSafe > 0 && horizontalInput > 0)
        {
            horizontalInput = 0;
        }
        if (Input.GetButtonUp("Jump"))
        {
            jumpTimer = 0;
            chargedJump = 0;
        }
        if (Input.GetButtonDown("Jump") && (IsGrounded() || coyoteTime > 0))
        {
            if (coyoteTime > 0)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
            }
            coyoteTime = 0;
            chargedJump = 0;
            jumpTimer = 0.25f;
            jumpForce = 3;
            playerSounds.PlayOneShot(SoundManager.Instance.PlayerJump);
        }
        if (jumpTimer > 0)
        {
            jumpTimer -= Time.deltaTime;
            chargedJump += Time.deltaTime * chargedJumpStrength;
        }
        else
        {
            chargedJump = 0;
        }
        if (transform.position.x > 3.87f)
        {
            transform.position = new Vector3(3.87f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -3.87f)
        {
            transform.position = new Vector3(-3.87f, transform.position.y, transform.position.z);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("HardBlock") && !IsGrounded() && collisionFailSafe != 0)
        {
            collisionFailSafe = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HardBlock") || collision.gameObject.CompareTag("Platform"))
        {
            blockCollisions--;
        }
    }

    public bool IsGrounded()
    {
        return blockCollisions != 0;
    }

    public float DecreaseTimer(float timer)
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            return 0;
        }
        return timer;
    }
}
