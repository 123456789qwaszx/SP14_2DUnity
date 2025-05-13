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
    BoxCollider2D boxCollider;

    int jumpCount = 2;
    int maxJumpCount = 2;

    float hAxis;

    bool isJump;
    bool jDown;
    public bool sDown;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        boxCollider = GetComponentInParent<BoxCollider2D>();
    }
    private void FixedUpdate()
    {
        Move();
        //Jump();
        if (jDown) // UI ��ư Ŭ������ jDown�� true�� �Ǹ� ���� ����
        {
            Jump();
            jDown = false; // ���� ó�� �� �ʱ�ȭ
        }
        //if (sDown) // UI ��ư Ŭ������ jDown�� true�� �Ǹ� ���� ����
        //{
        //    Sliding();
        //    sDown = false; // ���� ó�� �� �ʱ�ȭ
        //}
        Sliding();
    }
    private void Update()
    {
        GetInput();
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        //jDown = Input.GetButtonDown("Jump");
        //sDown = Input.GetButton("Fire3");
    }
    private void Move()
    {
        Vector2 movement = new Vector2(hAxis, 0);
        rb.velocity = movement * baseSpeed;

        anim.SetBool("isRun", rb.velocity != Vector2.zero);
    }
    public void Jump()
    {
        if (jDown && jumpCount > 0)
        {

            rb.AddForce(Vector2.up * baseJump, ForceMode2D.Impulse);

            if (jumpCount == maxJumpCount)
            {
                jumpCount--;
                anim.SetBool("isJump", true);
            }
            else if (jumpCount == 1)
            {
                jumpCount--;
                anim.SetBool("isJump", false);
                anim.SetBool("isDoubleJump", true);
            }
            isJump = true;
        }
    }

    void Sliding()
    {
        anim.SetBool("isSliding", sDown);
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

    public void OnJumpInput()
    {
        jDown = true;
    }

    public void OnSlidingInput()
    {
        sDown = true;
    }
}
