using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBaseController : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator anim;
    protected GameUI gameUI;

    [Header("Character State")]
    [Tooltip("Ä³ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½Æ® ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Í½ï¿½")]
    public float maxHp = 3f;
    protected float currentHp = 0f;
    protected float moveSpeed = 5f;
    protected float currentSpeed = 0f;
    public float jumpPower = 3f;
    public float currentJumpPower = 0f;
    public int maxJumpCount = 1;
    public int jumpCount = 0;
    public bool isJumping = false;
    public bool isJumpHold = false;
    protected float slidePower = 2f;
    protected int score = 0;
    protected int bestScore = 0;
    public bool isSliding = false;
    public bool isGround = false;
    public float CurrentHp {
        get {
            return currentHp;
        }
        set {
            if (currentHp != value)
            {
                if (currentHp > value) // ÇÃ·¹ÀÌ¾î Ã¼·ÂÀÌ °¨¼ÒÇßÀ» ¶§
                {
                    currentHp = value; // º¯µ¿ÇÑ ÇÃ·¹ÀÌ¾î Ã¼·Â Àû¿ë

                    gameUI.HpUp = false;
                }
                else if (currentHp < value) // ÇÃ·¹ÀÌ¾î Ã¼·ÂÀÌ Áõ°¡ÇßÀ» ¶§
                {
                    currentHp = value; // º¯µ¿ÇÑ ÇÃ·¹ÀÌ¾î Ã¼·Â Àû¿ë

                    gameUI.HpUp = true;
                }
            }
        }
    }
    public float CurrentSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
    public float CurrentJumpPower { get { return jumpPower; } set { jumpPower = value; } }
    public int Score { get { return score; } set { score = value; } }
    public int BestScore { get { return bestScore; } set { bestScore = value; } }
    protected float damage = 1f;

    protected float knockBackPower = 3f;
    public float KnockBackPower { get { return knockBackPower; } }


    [Header("Character Interaction")]

    protected Vector2 knockBack = Vector2.zero;
    protected float knockBackDuration = 0f;

    protected float invincibleDuration = 2f;
    public bool isInvincible = false;

    private Coroutine invincibleCoroutine = null;

    private Vector3 returnPosition = Vector3.zero;
    private float returnDistance = 5f;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

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
        else if (knockBack != Vector2.zero)
        {
            RecoverKnockBack();
        }
    }

    public virtual void SetCharacterState()
    {

    }

    public virtual void Jump()

    {

    }

    public virtual void Slide()
    {

    }

    public virtual void EndSlide()
    {

    }

    public virtual void Damage(float damage)
    {
        currentHp -= damage;

        if (currentHp <= 0f)
        {
            gameUI.CheckGameOver();
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

        // to do: °ÔÀÓ ¿À¹ö Ã³¸® Ãß°¡°¡

        Destroy(gameObject, 2f);
    }

    public virtual void ApplyKnockBack(Transform other, float power)
    {
        returnPosition.x = transform.position.x;

        knockBackDuration = power / 3;
        knockBack = (other.position - transform.position).normalized * power;

        rb.velocity -= knockBack;
    }

    protected virtual void RecoverKnockBack()
    {
        float currentXPos = transform.position.x;
        float targetXPos = returnPosition.x;

        if (Mathf.Abs(currentXPos - targetXPos) < 0.001f)
        {
            knockBack = Vector2.zero;
            rb.velocity = Vector2.zero;

            return;
            // returnPosition.x = currentX;
        }

        float returnXPos = Mathf.Lerp(currentXPos, targetXPos, Time.deltaTime * returnDistance);

        transform.position = new Vector3(returnXPos, transform.position.y, transform.position.z);
    }

    public virtual void ApplyInvincible()
    {
        if (invincibleCoroutine != null)
        {
            StopCoroutine(invincibleCoroutine);
        }

        invincibleCoroutine = StartCoroutine(InvincibleCoroutine(invincibleDuration));
    }

    protected IEnumerator InvincibleCoroutine(float duration)
    {
        isInvincible = true;

        yield return new WaitForSeconds(duration);

        EndInvincible();

        invincibleCoroutine = null;
    }

    protected virtual void EndInvincible()
    {
        isInvincible = false;
    }
}