using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private float baseJump = 30f;

    Rigidbody2D rb;
    Animator anim;

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
        Vector3 movement = new Vector3(hAxis, 0);
        rb.velocity = movement * baseSpeed;
        movementVelo = rb.velocity;

        anim.SetBool("isRun", movementVelo != Vector2.zero);
    }
    //void Run()
    //{ 
    //    if(movementVelo != Vector2.zero)
    //    {
    //        anim.SetBool("isRun", true);
    //    }
    //    else
    //    {
    //        anim.SetBool("isRun", false);
    //    }
    //} 
    void Jump()
    {
        if (jDown && !isJump)
        {
            Debug.Log("Jump");
            rb.AddForce(Vector3.up * baseJump, ForceMode2D.Impulse);

            anim.SetBool("isJump", true);
            isJump = true;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("isJump", false);
            isJump = false;

        }
    }
}
