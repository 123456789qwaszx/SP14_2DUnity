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
    public bool isInvincible = false;  // 무적 상태 체크
    private Coroutine invincibleCoroutine = null;

    private Vector3 returnPosition = Vector3.zero;   // 캐릭터 복귀 위치
    private float returnDistance = 5f; // 캐릭터 복귀 속도

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
        else if (knockBack != Vector2.zero)   // 넉백이 끝났을 경우, 넉백 상태를 초기화
        {
            RecoverKnockBack();
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
        currentHp -= damage;
        
        if (currentHp <= 0f)    // 체력이 0 이하로 떨어지는 데미지를 입었을 경우, 사망 처리
        {
            Dead();
        }
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

        // to do: 사망 애니메이션 추가

        Destroy(gameObject, 2f);
    }

    public virtual void ApplyKnockBack(Transform other, float power)
    {
        returnPosition.x = transform.position.x;   // 캐릭터 복귀 위치를 넉백 전의 위치로 설정

        knockBackDuration = power / 3;  // 밀어내는 힘이 강할수록 오랫동안 날아간다
        knockBack = (other.position - transform.position).normalized * power;

        rb.velocity -= knockBack;
    }

    // 경직 후, 캐릭터를 화면 중앙으로 복귀시킴
    protected virtual void RecoverKnockBack()
    {
        float currentXPos = transform.position.x;   // 넉백 직후 캐릭터 x좌표값
        float targetXPos = returnPosition.x;

        if (Mathf.Abs(currentXPos - targetXPos) < 0.001f)   // 넉백이 끝나고, 캐릭터가 복귀 위치에 도달했을 경우
        {
            knockBack = Vector2.zero;
            rb.velocity = Vector2.zero;

            return;
            // returnPosition.x = currentX;   // 복귀 위치를 현재 위치로 설정
        }

        float returnXPos = Mathf.Lerp(currentXPos, targetXPos, Time.deltaTime * returnDistance);

        transform.position = new Vector3(returnXPos, transform.position.y, transform.position.z);
    }

    public virtual void ApplyInvincible()
    {
        if (invincibleCoroutine != null)    // 코루틴 중복 실행 방지
        {
            StopCoroutine(invincibleCoroutine);
        }

        invincibleCoroutine = StartCoroutine(InvincibleCoroutine(invincibleDuration));
    }

    protected IEnumerator InvincibleCoroutine(float duration)
    {
        isInvincible = true;

        yield return new WaitForSeconds(duration);  // 무적 시간 동안 무적 종료 함수 호출 대기

        EndInvincible();

        invincibleCoroutine = null;
    }

    protected virtual void EndInvincible()
    {
        isInvincible = false;
    }
}
