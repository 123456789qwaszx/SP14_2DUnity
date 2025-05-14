using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterController : CharacterBaseController
{

    [SerializeField] private GameObject _scaleUP;
    [SerializeField] private GameObject _hpRecovery;
    [SerializeField] private GameObject _speedUP;

    public List<ParallaxHandle> parallaxHandles = new List<ParallaxHandle>();

    bool isItem = false;

    private float maxSpeed = 5f;
    private float duration = 3f;
    public float MaxSpeed { get { return maxSpeed; } set { maxSpeed = value; } }
    public float Duration { get { return duration; } set { duration = value; } }

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

        if (jumpCount < maxJumpCount && !isSliding)
        {
            // 테스트용 코드
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
            /* 롱 점프 구현현
            if (Input.GetKey(KeyCode.Space))
            {
                isJumpHold = true;
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                isJumpHold = false;
            }
            */
        }

        // 
        if (!isSliding && isGround)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Slide();
            }
        }

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
        /* 롱 점프 구현현
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

    public override void SetCharacterState()
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

    public override void Jump()
    {
        isJumping = true;

        rb.velocity = Vector2.zero;

        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

        if (jumpCount == 0)
        {
            anim.SetBool("isJump", true);
        }
        else if (jumpCount >= 1)
        {
            anim.SetBool("isJump", false);
            anim.SetBool("isDoubleJump", true);
        }

        jumpCount++;
    }

    public override void Slide()
    {
        isSliding = true;
        anim.SetBool("isSliding", isSliding);
    }

    public override void EndSlide()
    {
        isSliding = false;
        anim.SetBool("isSliding", isSliding);
    }

    private void IncreaseSpeed()
    {
        Debug.Log("increase Speed: " + currentSpeed);   // memo: 속도 증가 로직 추가
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;

            if (isJumping)
            {
                isJumping = false;

                anim.SetBool("isJump", isJumping);
                anim.SetBool("isDoubleJump", isJumping);

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
        GameObject gameUIObject = GameObject.FindGameObjectWithTag("GameUI");
        gameUI = gameUIObject.GetComponent<GameUI>();

        if (collision.gameObject.CompareTag("MapRoutin"))
        {
            //맵 추가시 랜덤범위 직접조정
            int randomIndex = UnityEngine.Random.Range(1, 5);

            GameObject randomMap = Managers.Map.LoadMap(randomIndex);

            float mapWidth = Managers.Map.GetMapWorldWidth(randomMap);
            randomMap.transform.position = new Vector3(mapWidth, 0);
        }
    }

    private IEnumerator SpeedUpCoroutine(float _speedUp, float _duration)
    {

        foreach (ParallaxHandle phUp in parallaxHandles)
        {
            phUp.SetMoveSpeed(CurrentSpeed + _speedUp);
        }

        Destroy(_speedUP);

        yield return new WaitForSeconds(_duration);

        foreach (ParallaxHandle phDown in parallaxHandles)
        {
            phDown.SetMoveSpeed(CurrentSpeed);
        }
    }

    private IEnumerator ScaleUpCoroutine(float _duration)
    {
        Vector3 originalScale = transform.localScale;

        transform.localScale = originalScale + new Vector3(1.0f, 1.0f, 0f);

        Destroy(_scaleUP);

        yield return new WaitForSeconds(_duration);

        transform.localScale = originalScale;
    }

    public override void ApplyInvincible()
    {
        base.ApplyInvincible();
        // anim.SetBool("isInvincible", isInvincible);
    }

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

        // to do: 게임 오버 처리 추가가
    }
}
