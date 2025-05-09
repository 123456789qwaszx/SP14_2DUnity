using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private float baseJump = 5f;

    Rigidbody2D rb;
    Animator anim;

    int jumpCount = 2;
    int maxJumpCount = 2;

    float hAxis;

    bool isJump;
    bool jDown;
    bool sDown;
 
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }
    private void FixedUpdate()
    {
        Move();
        Jump();
    }
    private void Update()
    {
        GetInput();
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        jDown = Input.GetButtonDown("Jump");
        sDown = Input.GetButton("Fire3");
    }
    private void Move()
    {
        Vector2 movement = new Vector2(hAxis, 0);
        rb.velocity = movement * baseSpeed;

        anim.SetBool("isRun", rb.velocity != Vector2.zero);
        anim.SetBool("isSliding", sDown);
    }
    void Jump()
    {
        if (jDown && jumpCount > 0)
        {
            rb.AddForce(Vector2.up * baseJump, ForceMode2D.Impulse);

            if (jumpCount == maxJumpCount)
            {
                Debug.Log($"Jump");
                jumpCount--;
                anim.SetBool("isJump", true);
            }
            else if (jumpCount == 1)
            {
                Debug.Log($"DoubleJump");
                jumpCount--;
                anim.SetBool("isJump", false);
                anim.SetBool("isDoubleJump", true);
            }
            isJump = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = maxJumpCount;
            isJump = false;
            anim.SetBool("isJump", false);
            anim.SetBool("isDoubleJump", false);
        }
        if (collision.gameObject.name == "TestTrap")
        {
            anim.SetBool("isHit", true);
            anim.SetBool("isHit", false);
        }
    }
}
