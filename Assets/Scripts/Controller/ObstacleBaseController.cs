using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBaseController : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("Obstacle State")]
    public float damage = 1f;
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

    // memo: 플레이어에서 장애물 충돌처리를 할 경우, Obstacle을 상속받는 클래스마다 다른 처리를 하기 힘들어지기 때문
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어 충돌 처리
        if (collision.gameObject.CompareTag("Player"))
        {
            CharacterController character = collision.gameObject.GetComponent<CharacterController>();

            if (character.isInvincible)
            {
                character.Damage(damage);
                character.ApplyKnockBack(transform, knockBackPower);
                character.ApplyInvincible();
            }
        }
    }
}
