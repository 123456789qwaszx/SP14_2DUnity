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

        // ï¿½ï¿½ï¿½ï¿½
        if (jumpCount < maxJumpCount && !isSliding)
        {
            // Å×½ºÆ®¿ë ÄÚµå
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

        // ï¿½ï¿½ï¿½ï¿½ï¿½Ìµï¿½ ï¿½ï¿½ï¿½ï¿½
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
        /* ï¿½ï¿½ï¿½ï¿½Å°ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½
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

        rb.velocity = Vector2.zero;   // ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½, ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½Ï¸ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½Óµï¿½ï¿½ï¿½ ï¿½Ê±ï¿½È­

        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

        if (jumpCount == 0)
        {
            anim.SetBool("isJump", true);   // ï¿½Ö´Ï¸ï¿½ï¿½Ì¼ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ ï¿½ï¿½ï¿?
        }
        else if (jumpCount >= 1)
        {
            anim.SetBool("isJump", false);   // ï¿½Ö´Ï¸ï¿½ï¿½Ì¼ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ ï¿½ï¿½ï¿?
            anim.SetBool("isDoubleJump", true);   // ï¿½Ö´Ï¸ï¿½ï¿½Ì¼ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ ï¿½ï¿½ï¿?
        }

        jumpCount++;
    }

    public override void Slide()
    {
        isSliding = true;
        anim.SetBool("isSliding", isSliding); // ï¿½Ö´Ï¸ï¿½ï¿½Ì¼ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ ï¿½ï¿½ï¿?
    }

    public override void EndSlide()
    {
        isSliding = false;
        anim.SetBool("isSliding", isSliding); // ï¿½Ö´Ï¸ï¿½ï¿½Ì¼ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ ï¿½ï¿½ï¿?
    }

    private void IncreaseSpeed()
    {
        Debug.Log("ï¿½ï¿½ï¿½Çµï¿½ ï¿½ï¿½ï¿½ï¿½: " + currentSpeed);   // memo: ï¿½Ê¿ï¿½ï¿½ï¿½ï¿½ï¿½ Playerï¿½Â±×¸ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½Ã·ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ Ä³ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ currentSpeedï¿½ï¿½ ï¿½ï¿½ ï¿½ï¿½ï¿½Çµï¿½ï¿? ï¿½ï¿½ï¿½ï¿½
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;

            if (isJumping)
            {
                isJumping = false;

                anim.SetBool("isJump", isJumping);   // ï¿½Ö´Ï¸ï¿½ï¿½Ì¼ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ ï¿½ï¿½ï¿?
                anim.SetBool("isDoubleJump", isJumping);   // ï¿½Ö´Ï¸ï¿½ï¿½Ì¼ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ ï¿½ï¿½ï¿?

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
    //            Debug.Log("Ã¼·ÂÈ¸º¹");
    //            Debug.Log($"ÇöÀç Ã¼·Â: {CurrentHp}");
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
    //            Debug.Log("Ã¼·Â°¨¼Ò");
    //            Debug.Log($"ÇöÀç Ã¼·Â: {CurrentHp}");
    //            gameUI.UpdateHealthUI();
    //            Debug.Log($"obstacle!");
    //        }

    //    }
    //    else if (collision.gameObject.CompareTag("MapRoutin"))
    //    {
    //        //¸Ê Ãß°¡½Ã ·£´ý¹üÀ§ Á÷Á¢Á¶Á¤
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
        // anim.SetBool("isInvincible", isInvincible);   // ï¿½Ö´Ï¸ï¿½ï¿½Ì¼ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ ï¿½ï¿½ï¿?
    }

    // ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Æ®ï¿½ï¿½ ï¿½Ö´ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ Ä³ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ Ã¼ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½Ò½ï¿½Å´
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

        // to do: ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ Ã³ï¿½ï¿½ (ï¿½ï¿½ï¿½ï¿½ ï¿½Å´ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ È£ï¿½ï¿½) ï¿½ß°ï¿½
    }
}
