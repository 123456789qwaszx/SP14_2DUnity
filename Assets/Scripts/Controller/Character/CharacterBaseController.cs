using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �÷��̾�� ĳ���� �߰��� ����� ĳ���� �⺻ Ŭ����
public class CharacterBaseController : MonoBehaviour
{
    protected Rigidbody2D rb; // ĳ���ʹ� �����Ǿ� �־ ���� ���� �ൿ�� ���� �ʿ�
    protected Animator anim;

    [Header("Character State")]
    [Tooltip("ĳ������ ����Ʈ �������ͽ�")]
    protected float maxHp = 3f;    // memo: 3���� ��Ʈ�� ������ ���� ���, �ݸ� ��� ��Ȳ�� �����Ͽ� float�� ����
    protected float currentHp = 0f;
    protected float moveSpeed = 5f;
    protected float currentSpeed = 0f;
    protected float jumpPower = 3f;
    protected float currentJumpPower = 0f;
    protected int maxJumpCount = 1;    // ĳ���Ͱ� ���� ���� Ƚ��
    protected int jumpCount = 0;
    protected bool isJumping = false;
    protected float slidePower = 2f;
    protected bool isSliding = false;
    protected bool isGround = false;

    [Header("Character Interaction")]
    private Vector2 knockBack = Vector2.zero;   // ��ֹ��� �ε��� ���� ĳ���Ͱ� �з����� ��
    private float knockBackDuration = 0f;

    private float invincibleDuration = 0f; // ���� �ð�

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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

    protected virtual void SetCharacterState()
    {

    }

    // memo: �����̵� ��, �ӵ��� ���ӽ�Ű�� ������ �ʿ��ұ�?
    protected virtual void Jump()
    {

    }

    protected virtual void Slide()
    {

    }

    protected virtual void EndSlide()
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
