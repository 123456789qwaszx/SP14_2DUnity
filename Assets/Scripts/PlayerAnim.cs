using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private float baseJump = 30f;

    Rigidbody2D rb;
    Animator anim;

    int jumpCount = 2;
    int maxJumpCount = 2;

    bool isJump;
    bool jDown;

    float hAxis;

    Vector2 movementVelo;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        
    }
    private void FixedUpdate()
    {
        GetInput();
        Move();
        Jump();
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        jDown = Input.GetButtonDown("Jump");
    }
    private void Move()
    {
        Vector2 movement = new Vector2(hAxis, 0);
        rb.velocity = movement * baseSpeed;

        anim.SetBool("isRun", rb.velocity != Vector2.zero);
    }
    void Jump()
    {
        if (jDown && jumpCount > 0)
        {
            rb.AddForce(Vector2.up * baseJump, ForceMode2D.Impulse);

            if (jumpCount == maxJumpCount) 
            {
                jumpCount--;
                anim.SetTrigger("doJump");
                Debug.Log($"OneJump {jumpCount}");
            }
            else if(jumpCount != 0)
            {
                jumpCount--;
                anim.SetTrigger("doDoubleJump");
                Debug.Log($"TwoJump {jumpCount}");
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = maxJumpCount;

        }
    }
}
