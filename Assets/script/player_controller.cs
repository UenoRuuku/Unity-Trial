using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D coll;
    private Animator anim;

    public float speed, jumpForce;
    private float horizontalMove;
    public Transform groundCheck;
    public LayerMask ground;

    public bool isGround, isJump, isDashing;

    bool jumpPressed;
    int jumpCount;
    bool isPreparing;

    // Start is called before the first frame update
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            jumpPressed = true;
        }
    }

    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);

        GroundMovement();

        Jump();

        prepare();

        // SwitchAnim();
    }

    void GroundMovement()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");//只返回-1，0，1
        rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);

        if (horizontalMove != 0)
        {
            transform.localScale = new Vector3(horizontalMove*Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

    }

    void Jump()//跳跃
    {
        if (isGround)
        {
            jumpCount = 2;//可跳跃数量
            isJump = false;
        }
        if (jumpPressed && isGround)
        {
            isJump = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
        }
        else if (jumpPressed && jumpCount > 0 && isJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
        }
    }

    void prepare()
    {
        float down = Input.GetAxisRaw("Vertical");
        if (isGround && down < 0 && rb.velocity.y == 0)
        {
            isPreparing = true;
            rb.velocity = new Vector2(0, rb.velocity.y);
            //蓄力……
        }
        else
        {
            isPreparing = false;
        }

    }

    // void SwitchAnim()
    // {
    //     anim.SetFloat("walk", Math.Abs(rb.velocity.x));
    //     if (isPreparing)
    //     {
    //         anim.SetBool("preparing", true);
    //     }
    //     else
    //     {
    //         anim.SetBool("preparing", false);
    //     }
    //     if (isGround)
    //     {
    //         anim.SetBool("falling", false);
    //     }
    //     else if (!isGround && rb.velocity.y > 0)
    //     {
    //         anim.SetBool("jumping", true);
    //         anim.SetBool("falling", false);
    //     }
    //     else if (rb.velocity.y < 0)
    //     {
    //         anim.SetBool("jumping", false);
    //         anim.SetBool("falling", true);
    //     }
    // }
}
