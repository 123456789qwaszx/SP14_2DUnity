using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private float baseJump = 5f;

    Rigidbody2D rb;
    Animator anim;

    bool isJump;

    Vector2 movementVelo;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }
    private void FixedUpdate()
    {
        Move();
        Run();
        Jump();
    }

    private void Move()
    {
        // 본 게임은 Horizontal, Vertical이 없음 
        // Vertical 자동 이동임
        float moveh = Input.GetAxisRaw("Horizontal");
        float movev = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(moveh, movev);
        rb.velocity = movement * baseSpeed;
        movementVelo = rb.velocity;

    }
    void Run()
    {
        if(movementVelo != Vector2.zero)
        {
            anim.SetBool("isRun", true);
        }
        else
        {
            anim.SetBool("isRun", false);
        }
    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && !isJump)
        {
            Debug.Log("Jump");
            rb.AddForce(Vector2.up * baseJump, ForceMode2D.Impulse);

            isJump = true;
            anim.SetBool("isJump", true);
        }
    }
}
