using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어블 캐릭터 추가를 대비한 캐릭터 기본 클래스
public class CharacterBaseController : MonoBehaviour
{
    protected Rigidbody2D rb; // 캐릭터는 고정되어 있어도 점프 등의 행동을 위해 필요
    protected Animator anim;

    [Header("Character State")]
    [Tooltip("캐릭터의 디폴트 스테이터스")]
    protected float maxHp = 3f;    // memo: 3개의 하트를 가지고 있을 경우, 반만 깎는 상황을 상정하여 float로 지정
    protected float currentHp = 0f;
    protected float moveSpeed = 5f;
    protected float currentSpeed = 0f;
    protected float jumpPower = 3f;
    protected float currentJumpPower = 0f;
    protected int maxJumpCount = 1;    // 캐릭터가 공중 점프 횟수
    protected int jumpCount = 0;
    protected bool isJumping = false;
    protected float slidePower = 2f;
    protected bool isSliding = false;
    protected bool isGround = false;

    [Header("Character Interaction")]
    protected Vector2 knockBack = Vector2.zero;   // 장애물에 부딪힌 이후 캐릭터가 밀려나는 힘
    protected float knockBackDuration = 0f;

    protected float invincibleDuration = 2f; // 무적 시간
    protected bool isInvincible = false;  // 무적 상태 체크
    private Coroutine invincibleCoroutine = null;

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
        if (knockBackDuration > 0f)
        {
            knockBackDuration -= Time.fixedDeltaTime;
        }
    }

    protected virtual void SetCharacterState()
    {

    }

    // memo: 슬라이딩 시, 속도를 가속시키는 로직이 필요할까?
    protected virtual void Jump()
    {

    }

    protected virtual void Slide()
    {

    }

    protected virtual void EndSlide()
    {

    }

    public virtual void Damage(float damage)
    {

    }

    public virtual void Heal()
    {
        if (currentHp < maxHp)
        {
            currentHp++;

            if (currentHp > maxHp)
            {
                currentHp = maxHp;
            }
        }
    }

    public virtual void Dead()
    {
        rb.velocity = Vector3.zero;

        foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())
        {
            component.enabled = false;
        }

        Destroy(gameObject, 2f);
    }

    protected virtual void ApplyKnockBack(Transform other, float power, float duration)
    {
        knockBackDuration = duration;
        knockBack = (other.position - transform.position).normalized * power;
    }

    protected virtual void ApplyInvincible()
    {
        if (invincibleCoroutine != null)    // 코루틴 중복 실행 방지
        {
            StopCoroutine(invincibleCoroutine);
        }

        invincibleCoroutine = StartCoroutine(InvincibleCoroutine(invincibleDuration));
    }

    private IEnumerator InvincibleCoroutine(float duration)
    {
        isInvincible = true;

        // memo: 무적 상태 테스트용 코드. 애니메이션이 준비되면 애니메이션 연결
        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = renderer.color;
            color.a = 0.5f;
            renderer.color = color;
        }

        yield return new WaitForSeconds(duration);  // 무적 시간 동안 무적 종료 함수 호출 대기

        EndInvincible();

        invincibleCoroutine = null;
    }

    protected virtual void EndInvincible()
    {
        isInvincible = false;

        // memo: 무적 상태 테스트용 코드. 애니메이션이 준비되면 애니메이션 연결
        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = renderer.color;
            color.a = 1f;
            renderer.color = color;
        }
    }
}
