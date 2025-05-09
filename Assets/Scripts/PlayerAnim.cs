using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 5f;

    Rigidbody2D rb;
    Animator anim;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float moveh = Input.GetAxisRaw("Horizontal");
        float movev = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(moveh, movev);
        rb.velocity = movement * baseSpeed;
        Vector2 movementVelo = rb.velocity;


    }
}
