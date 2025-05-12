using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("Obstacle State")]
    private float damage = 0f;
    public float Damage { get { return damage; } }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = Vector2.left * 2f; // 테스트용
    }
}
