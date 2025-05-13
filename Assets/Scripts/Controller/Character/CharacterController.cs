using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : CharacterBaseController
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();

        SetCharacterState();
    }

    protected override void Update()
    {
        // 점프
        if (jumpCount < maxJumpCount)
        {
            // 테스트용 점프 코드. 실제로는 모바일 환경에 맞춰 OnClick으로 구현 예정
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            /*  점프키 홀드 중 점프력 증가 로직
            if (Input.GetKey(KeyCode.Space))
            {
                isJumpHold = true;   // 점프키를 누르고 있는지 확인
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                isJumpHold = false;
            }
            */
        }

        // 슬라이딩
        if (!isSliding && isGround)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Slide();
            }
        }

        // 슬라이딩 종료
        if (isSliding)
        {
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                EndSlide();
            }
        }
    }

    protected override void FixedUpdate()
    {
        /* 점프키를 누르는 동안 점프력 증가
        if (isJumpHold && rb.velocity.y > 0f)
        {
            rb.gravityScale = 5f;
        }
        else
        {
            rb.gravityScale = 10f;
        }
        */
    }

    protected override void SetCharacterState()
    {
        base.SetCharacterState();

        maxHp = 3f;
        currentHp = maxHp;

        moveSpeed = 5f;
        currentSpeed = moveSpeed;

        jumpPower = 5f;
        maxJumpCount = 2;
        slidePower = 5f;
    }

    protected override void Jump()
    {
        isJumping = true;

        rb.velocity = Vector2.zero;   // 낙하 중, 점프력 저하를 막기 위해 속도를 초기화

        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

        if (jumpCount == 0)
        {
            // anim.SetBool("isJump", true);   // 애니메이션 연결 후 사용
        }
        else if (jumpCount >= 1)
        {
            // anim.SetBool("isJump", false);   // 애니메이션 연결 후 사용
            // anim.SetBool("isDoubleJump", true);   // 애니메이션 연결 후 사용
        }

        jumpCount++;
    }

    protected override void Slide()
    {
        isSliding = true;
        // anim.SetBool("isSliding", isSliding); // 애니메이션 연결 후 사용
    }

    protected override void EndSlide()
    {
        isSliding = false;
        // anim.SetBool("isSliding", isSliding); // 애니메이션 연결 후 사용
    }

    private void IncreaseSpeed()
    {
        Debug.Log("스피드 증가: " + currentSpeed);   // memo: 맵에서는 Player태그를 추적해 현재 플레이 중인 캐릭터의 currentSpeed를 맵 스피드로 적용
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;

            if (isJumping)
            {
                isJumping = false;

                jumpCount = 0;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    public override void ApplyInvincible()
    {
        base.ApplyInvincible();
        // anim.SetBool("isInvincible", isInvincible);   // 애니메이션 연결 후 사용
    }

    // 닿은 오브젝트가 주는 데미지에 따라 캐릭터의 체력을 감소시킴
    public override void Damage(float damage)
    {
        base.Damage(damage);
    }

    public override void ApplyKnockBack(Transform other, float power)
    {
        base.ApplyKnockBack(other, power);
    }

    public override void Heal()
    {
        base.Heal();
    }

    public override void Dead()
    {
        base.Dead();

        // to do: 게임 오버 처리 (게임 매니저의 게임 오버 호출) 추가
    }
}
