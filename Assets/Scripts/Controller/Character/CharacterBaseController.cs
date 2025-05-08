using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어블 캐릭터 추가를 대비한 캐릭터 기본 클래스
public class CharacterBaseController : MonoBehaviour
{
    protected Rigidbody rb; // 캐릭터는 고정되어 있어도 점프 등의 행동을 위해 필요

    [SerializeField] private SpriteRenderer characterSprite;
    [SerializeField] private Animator anim;

    private Vector2 knockBack = Vector2.zero;   // 장애물에 부딪힌 이후 캐릭터가 밀려나는 힘
    private float knockBackDuration = 0f;

    private float invincibleDuration = 0f; // 무적 시간

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }

    protected virtual void FixedUpdate()
    {

    }

    protected virtual void ApplyKnockBack(Transform other, float power, float duration)
    {
        knockBackDuration = duration;
        knockBack = (other.position - transform.position).normalized * power;
    }

    protected virtual void ApplyInvincible()
    {

    }
}
