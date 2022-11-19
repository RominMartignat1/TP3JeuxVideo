using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private int isGrounded;
    //private AudioSource audioSource;
    private Rigidbody2D rigidBody;
    private float horizontal;
    private int collisionThreshold;
    private float jumpTimer;
    private float jumpIntensity;
    private float jumpPower;
    private float maxThreshold;
    private bool hasDied;
    private bool canPlayerMove;
    private float deathTimer;
    private float acceleration;

    public PlayerController()
    {
        this.canPlayerMove = true;
        this.maxThreshold = 0.2f;
        this.acceleration = 0.0f;
    }
    private void Awake()
    {
        //this.audioSource = this.GetComponent<AudioSource>();
        this.rigidBody = this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (canPlayerMove)
        {
            Debug.Log("acceleration: " + acceleration);


            this.rigidBody.velocity = new Vector2(this.horizontal + this.acceleration, this.rigidBody.velocity.y + this.jumpPower + this.jumpIntensity);
            this.jumpPower = 0.0f;
        }

        if (jumpIntensity <= 10.0f)
        {
            this.jumpIntensity = 0.0f;
        }
    }

    private void Update()
    {
        /*if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }*/
        if (hasDied)
        {
            this.deathTimer += Time.deltaTime;
            this.transform.localScale = new Vector3(1f - this.deathTimer, 1f - this.deathTimer, 1f - this.deathTimer);
        }
        if (canPlayerMove)
        {
            ManageMovement();
            this.transform.rotation = Quaternion.Euler(0, transform.rotation.y, transform.rotation.z);
        }

    }

    private void ManageMovement()
    {
        this.horizontal = Input.GetAxis("Horizontal");

        if(this.horizontal > 0)
        {
            if(this.acceleration < 4.0f)
            {
                this.acceleration += 0.05f;
            }
        }
        else if(this.horizontal < 0)
        {
            if(this.acceleration > -4.0f)
            {
                this.acceleration -= 0.05f;
            }
        }
        else
        {
            if(this.acceleration > 0)
            {
                this.acceleration -= 0.2f;
            }
            else if(this.acceleration < 0)
            {
                this.acceleration += 0.2f;
            }
        }

        if (Input.GetButtonDown("Jump") /*&& IsGrounded()*/)
        {
            Debug.Log("Jump");
            this.jumpPower = 3.0f;

            if(System.Math.Abs(acceleration) > 1.0f && System.Math.Abs(acceleration) < 2.0f)
            {
                this.jumpIntensity = 1.5f;
            }
            else if(System.Math.Abs(acceleration) > 2.0f && System.Math.Abs(acceleration) < 3.0f)
            {
                this.jumpIntensity = 2.0f;
            }
            else if(System.Math.Abs(acceleration) > 3.0f )
            {
                this.jumpIntensity = 5.0f;
            }

        }



        /*if (!this.IsGrounded() && this.maxThreshold > 0.0f)
        {
            this.maxThreshold -= Time.deltaTime;
        }

        if (this.collisionThreshold < 0 && this.horizontal < 0.0)
        {
            horizontal = 0.0f;
        }
        else if (this.collisionThreshold > 0 && this.horizontal > 0.0)
        {
            horizontal = 0.0f;
        }*/

        /*if (Input.GetButtonDown("Jump") && (IsGrounded() || maxThreshold > 0.0))
        {
            Debug.Log("Jump");
            if (maxThreshold > 0.0)
            {
                this.rigidBody.velocity = new Vector2(this.rigidBody.velocity.x, 0.0f);
            }
            maxThreshold = 0.0f;
            jumpTimer = 0.25f;
            jumpIntensity = 0.0f;
            jumpPower = 3f;
        }
        if (Input.GetButtonUp("Jump"))
        {
            jumpTimer = 0.0f;
            jumpIntensity = 0.0f;
        }
        if (jumpTimer > 0.0)
        {
            float deltaTime = Time.deltaTime;
            jumpIntensity += deltaTime * 20f;
            jumpTimer -= deltaTime;
        }
        else
        {
            this.jumpIntensity = 0.0f;
        }*/

        /*if (this.transform.position.x > 3.87f)
        {
            this.transform.position = new Vector3(3.87f, this.transform.position.y, this.transform.position.z);
        }
        else
        {
            if (!(this.transform.position.x >= -3.87))
            {
                this.transform.position = new Vector3(-3.87f, this.transform.position.y, this.transform.position.z);
            }
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasDied)
        {
            if ( collision.gameObject.tag == "Block")
            {
                isGrounded++;
                collisionThreshold = 0;
                maxThreshold = 0.2f;
            }
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Block") && !this.IsGrounded())
        {
            if (collision.transform.position.x > this.transform.position.x)
            {
                this.collisionThreshold = 1;
            }
            else if (collision.transform.position.x < (double)this.transform.position.x)
            {
                this.collisionThreshold = -1;
            }
            else
            {
                this.collisionThreshold = 0;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Block" && !this.IsGrounded() && this.collisionThreshold != 0)
        {
            this.collisionThreshold = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Block" || collision.gameObject.tag == "Platform")
        {
            this.isGrounded--;
        }
    }

    public bool IsGrounded()
    {
        return this.isGrounded != 0;
    }
}