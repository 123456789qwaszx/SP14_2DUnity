using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : CharacterBaseController
{
    /*
     * 캐릭터 동작 정의 클래스
     * 1. 점프
     * 2. 슬라이딩
     * 3. 아이템 습득
     * 4. 아이템 습득에 따른 동작 구분 ( 점수 증가, 속도 증가, 속도 감소 등 )
     */

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
        /*
         * 모바일 환경이기 때문에 OnClick을 상정해야 함
         * 어떤 버튼을 누르냐에 따라 점프, 슬라이드가 행해짐
         * 점프를 누르면 캐릭터의 Jump를 호출하고 슬라이드를 누르면 Slide를 호출
         * Slide의 경우 누르는 동안 계속 캐릭터 속도를 높이며 슬라이딩
         */


        // 점프
        if (jumpCount < maxJumpCount && !isSliding)
        {
            // 테스트용 점프 코드. 실제로는 모바일 환경에 맞춰 OnClick으로 구현 예정
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }

        // 슬라이딩
        if (!isSliding && !isJumping)
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

    protected override void SetCharacterState()
    {
        base.SetCharacterState();

        maxHp = 3f;
        currentHp = maxHp;

        moveSpeed = 5f;
        currentSpeed = moveSpeed;

        jumpPower = 15f;
        maxJumpCount = 2;
        slidePower = 5f;
    }

    /* 구현 순서
     * 1. 일반 점프
     * 2. 2단 점프
     * 3. 점프를 꾹 누르면 점프 높이 증가
     */
    public override void Jump()
    {
        isJumping = true;

        rb.velocity = Vector2.zero;   // 낙하 중, 점프력 저하를 막기 위해 속도를 초기화

        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

        if (jumpCount == 0)
        {
            anim.SetBool("isJump", true);   // 애니메이션 연결 후 사용
        }
        else if (jumpCount >= 1)
        {
            anim.SetBool("isJump", false);   // 애니메이션 연결 후 사용
            anim.SetBool("isDoubleJump", true);   // 애니메이션 연결 후 사용
        }

        jumpCount++;
    }

    public override void Slide()
    {
        isSliding = true;
        anim.SetBool("isSliding", isSliding); // 애니메이션 연결 후 사용
    }

    public override void EndSlide()
    {
        isSliding = false;
        anim.SetBool("isSliding", isSliding); // 애니메이션 연결 후 사용
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

                anim.SetBool("isJump", isJumping);   // 애니메이션 연결 후 사용
                anim.SetBool("isDoubleJump", isJumping);   // 애니메이션 연결 후 사용

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
        // 장애물 충돌 처리
        if (collision.gameObject.CompareTag("Obstacle") && !isInvincible)
        {
            float damage = collision.gameObject.GetComponent<ObstacleBaseController>().Damage;
            float knockBackPower = collision.gameObject.GetComponent<ObstacleBaseController>().KnockBackPower;

            Damage(damage);
            ApplyKnockBack(collision.gameObject.transform, knockBackPower);
            ApplyInvincible();
        }
    }

    protected override void ApplyInvincible()
    {
        base.ApplyInvincible();
        // anim.SetBool("isInvincible", isInvincible);   // 애니메이션 연결 후 사용
    }

    // 닿은 오브젝트가 주는 데미지에 따라 캐릭터의 체력을 감소시킴
    public override void Damage(float damage)
    {
        base.Damage(damage);
    }

    protected override void ApplyKnockBack(Transform other, float power)
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
