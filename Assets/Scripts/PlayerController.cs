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
        canPlayerMove = true;
        maxThreshold = 0.2f;
        acceleration = 0.0f;
    }
    private void Awake()
    {
        //audioSource = GetComponent<AudioSource>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (canPlayerMove)
        {
            Debug.Log("isgrounded: " + IsGrounded());


            rigidBody.velocity = new Vector2(horizontal + acceleration, rigidBody.velocity.y + jumpPower + jumpIntensity);
            jumpPower = 0.0f;
        }

        if (jumpIntensity <= 10.0f)
        {
            jumpIntensity = 0.0f;
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
            deathTimer += Time.deltaTime;
            transform.localScale = new Vector3(1f - deathTimer, 1f - deathTimer, 1f - deathTimer);
        }
        if (canPlayerMove)
        {
            ManageMovement();
            transform.rotation = Quaternion.Euler(0, transform.rotation.y, transform.rotation.z);
        }

    }

    private void ManageMovement()
    {
        horizontal = Input.GetAxis("Horizontal");

        if(horizontal > 0)
        {
            if(acceleration < 4.0f)
            {
                acceleration += 0.1f;
            }
        }
        else if(horizontal < 0)
        {
            if(acceleration > -4.0f)
            {
                acceleration -= 0.1f;
            }
        }
        else
        {
            if(acceleration > 0)
            {
                acceleration -= 0.2f;
            }
            else if(acceleration < 0)
            {
                acceleration += 0.2f;
            }
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Debug.Log("Jump");
            jumpPower = 3.0f;
            //jumpIntensity = 10.0f;


            if(System.Math.Abs(acceleration) > 1.0f && System.Math.Abs(acceleration) < 2.0f)
            {
                jumpIntensity = 10.5f;
            }
            else if(System.Math.Abs(acceleration) > 2.0f && System.Math.Abs(acceleration) < 3.0f)
            {
                jumpIntensity = 2.0f;
            }
            else if(System.Math.Abs(acceleration) > 3.0f )
            {
                jumpIntensity = 5.0f;
            }

        }
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
        if (collision.gameObject.tag == "Block" || collision.gameObject.tag == "Platform")
        {
            isGrounded--;
        }
    }

    public bool IsGrounded()
    {
        return isGrounded != 0;
    }
}
