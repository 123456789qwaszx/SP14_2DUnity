using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBaseController : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("Obstacle State")]
    private float damage = 1f;
    public virtual float Damage { get { return damage; } }

    private float knockBackPower = 3f;
    public virtual float KnockBackPower { get { return knockBackPower; } }

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }
}
