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

        // ����
        if (jumpCount < maxJumpCount && !isSliding)
        {
            // �׽�Ʈ�� ���� �ڵ�. �����δ� ����� ȯ�濡 ���� OnClick���� ���� ����
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            /*  ����Ű Ȧ�� �� ������ ���� ����
            if (Input.GetKey(KeyCode.Space))
            {
                isJumpHold = true;   // ����Ű�� ������ �ִ��� Ȯ��
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                isJumpHold = false;
            }
            */
        }

        // �����̵�
        if (!isSliding && isGround)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Slide();
            }
        }

        // �����̵� ����
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
        /* ����Ű�� ������ ���� ������ ����
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

        jumpPower = 15f;
        maxJumpCount = 2;
        slidePower = 5f;
    }

    public override void Jump()
    {
        isJumping = true;

        rb.velocity = Vector2.zero;   // ���� ��, ������ ���ϸ� ���� ���� �ӵ��� �ʱ�ȭ

        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

        if (jumpCount == 0)
        {
            anim.SetBool("isJump", true);   // �ִϸ��̼� ���� �� ���
        }
        else if (jumpCount >= 1)
        {
            anim.SetBool("isJump", false);   // �ִϸ��̼� ���� �� ���
            anim.SetBool("isDoubleJump", true);   // �ִϸ��̼� ���� �� ���
        }

        jumpCount++;
    }

    public override void Slide()
    {
        isSliding = true;
        anim.SetBool("isSliding", isSliding); // �ִϸ��̼� ���� �� ���
    }

    public override void EndSlide()
    {
        isSliding = false;
        anim.SetBool("isSliding", isSliding); // �ִϸ��̼� ���� �� ���
    }

    private void IncreaseSpeed()
    {
        Debug.Log("���ǵ� ����: " + currentSpeed);   // memo: �ʿ����� Player�±׸� ������ ���� �÷��� ���� ĳ������ currentSpeed�� �� ���ǵ�� ����
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;

            if (isJumping)
            {
                isJumping = false;

                anim.SetBool("isJump", isJumping);   // �ִϸ��̼� ���� �� ���
                anim.SetBool("isDoubleJump", isJumping);   // �ִϸ��̼� ���� �� ���

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
        if (collision.gameObject.CompareTag("MapRoutin"))
        {
            int randomIndex = UnityEngine.Random.Range(1, 3);
            GameObject randomMap = Managers.Map.LoadMap(randomIndex);

            float mapWidth = Managers.Map.GetMapWorldWidth(randomMap);
            randomMap.transform.position = new Vector3(mapWidth, 0);
        }
    }

    public override void ApplyInvincible()
    {
        base.ApplyInvincible();
        // anim.SetBool("isInvincible", isInvincible);   // �ִϸ��̼� ���� �� ���
    }

    // ���� ������Ʈ�� �ִ� �������� ���� ĳ������ ü���� ���ҽ�Ŵ
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

        // to do: ���� ���� ó�� (���� �Ŵ����� ���� ���� ȣ��) �߰�
    }
}
