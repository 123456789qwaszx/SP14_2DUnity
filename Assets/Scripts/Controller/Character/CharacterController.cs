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

        // ����
        if (jumpCount < maxJumpCount && !isSliding)
        {
            // �׽�Ʈ�� �ڵ�
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            /*
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

        rb.velocity = Vector2.zero;   // ���� ��, ������ ���ϸ� ���� ���� �ӵ��� �ʱ�ȭ

        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

        if (jumpCount == 0)
        {
            anim.SetBool("isJump", true);   // �ִϸ��̼� ���� �� ���?
        }
        else if (jumpCount >= 1)
        {
            anim.SetBool("isJump", false);   // �ִϸ��̼� ���� �� ���?
            anim.SetBool("isDoubleJump", true);   // �ִϸ��̼� ���� �� ���?
        }

        jumpCount++;
    }

    public override void Slide()
    {
        isSliding = true;
        anim.SetBool("isSliding", isSliding); // �ִϸ��̼� ���� �� ���?
    }

    public override void EndSlide()
    {
        isSliding = false;
        anim.SetBool("isSliding", isSliding); // �ִϸ��̼� ���� �� ���?
    }

    private void IncreaseSpeed()
    {
        Debug.Log("���ǵ� ����: " + currentSpeed);   // memo: �ʿ����� Player�±׸� ������ ���� �÷��� ���� ĳ������ currentSpeed�� �� ���ǵ��? ����
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;

            if (isJumping)
            {
                isJumping = false;

                anim.SetBool("isJump", isJumping);   // �ִϸ��̼� ���� �� ���?
                anim.SetBool("isDoubleJump", isJumping);   // �ִϸ��̼� ���� �� ���?

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


    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    GameObject gameUIObject = GameObject.FindGameObjectWithTag("GameUI");
    //    gameUI = gameUIObject.GetComponent<GameUI>();

    //    if (collision.gameObject.CompareTag("Score10"))
    //    {
    //        Score += 10;

    //        if (Score >= BestScore)
    //        {
    //            BestScore = Score;
    //        }

    //        gameUI.SetUI(Score, BestScore);
    //        Destroy(collision.gameObject);
    //        Debug.Log($"{Score}");
    //    }
    //    else if (collision.gameObject.CompareTag("Score50"))
    //    {
    //        Score += 50;

    //        if (Score >= BestScore)
    //        {
    //            BestScore = Score;
    //            gameUI.SaveBestScore();
    //        }

    //        gameUI.SetUI(Score, BestScore);
    //        Destroy(collision.gameObject);
    //        Debug.Log($"{Score}");
    //    }
    //    else if (collision.gameObject.CompareTag("HpRecovery"))
    //    {
    //        if (CurrentHp > 0 && CurrentHp < 3)
    //        {
    //            CurrentHp += 1;
    //            gameUI.HpUp = true;
    //            gameUI.UpdateHealthUI();
    //            Debug.Log("ü��ȸ��");
    //            Debug.Log($"���� ü��: {CurrentHp}");
    //            Debug.Log($"{"HpRecovery!"}");
    //        }
    //        else
    //        {

    //        }
    //        Destroy(collision.gameObject);
            
    //    }
    //    //else if (collision.gameObject.CompareTag("SpeedUp"))
    //    //{
    //    //    StartCoroutine(SpeedUpCoroutine(maxSpeed, duration));
    //    //    Destroy(collision.gameObject);
    //    //    Debug.Log($"SpeedUp!");
    //    //}
    //    //else if (collision.gameObject.CompareTag("ScaleUp"))
    //    //{
    //    //    StartCoroutine(ScaleUpCoroutine(duration));
    //    //    Destroy(collision.gameObject);
    //    //    Debug.Log($"ScaleUp!");
    //    //}
    //    else if (collision.gameObject.CompareTag("Obstacle"))
    //    {
    //        isInvincible = true;
    //        if (isInvincible)
    //        {
    //            ObstacleBaseController obstacle = collision.gameObject.GetComponent<ObstacleBaseController>();

    //            Damage(damage);
    //            ApplyKnockBack(transform, knockBackPower);
    //            ApplyInvincible();
    //            Debug.Log("ü�°���");
    //            Debug.Log($"���� ü��: {CurrentHp}");
    //            gameUI.UpdateHealthUI();
    //            Debug.Log($"obstacle!");
    //        }

    //    }
    //    else if (collision.gameObject.CompareTag("MapRoutin"))
    //    {
    //        //�� �߰��� �������� ��������
    //        int randomIndex = UnityEngine.Random.Range(1, 5);

    //        GameObject randomMap = Managers.Map.LoadMap(randomIndex);

    //        float mapWidth = Managers.Map.GetMapWorldWidth(randomMap);
    //        randomMap.transform.position = new Vector3(mapWidth, 0);
    //    }
    //    else
    //    {
    //        Debug.Log("failed to find tag");
    //    }
    //}

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
        // anim.SetBool("isInvincible", isInvincible);   // �ִϸ��̼� ���� �� ���?
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
