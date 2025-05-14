using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �÷��̾��? ĳ���� �߰��� �����? ĳ���� �⺻ Ŭ����
public class CharacterBaseController : MonoBehaviour
{
    protected Rigidbody2D rb; // ĳ���ʹ� �����Ǿ� �־ ���� ���� �ൿ�� ���� �ʿ�
    protected Animator anim;
    protected GameUI gameUI;

    [Header("Character State")]
    [Tooltip("ĳ������ ����Ʈ �������ͽ�")]
    public float maxHp = 3f;    // memo: 3���� ��Ʈ�� ������ ���� ���?, �ݸ� ���? ��Ȳ�� �����Ͽ� float�� ����
    protected float currentHp = 0f;
    protected float moveSpeed = 5f;
    protected float currentSpeed = 0f;
    public float jumpPower = 3f;
    public float currentJumpPower = 0f;
    public int maxJumpCount = 1;    // ĳ���Ͱ� ���� ���� Ƚ��
    public int jumpCount = 0;
    public bool isJumping = false;
    public bool isJumpHold = false;  // ����Ű ���� Ȯ��
    protected float slidePower = 2f;
    protected int score = 0;
    protected int bestScore = 0;
    public bool isSliding = false;
    public bool isGround = false;
    public float CurrentHp { get { return currentHp; } set { currentHp = value; } }
    public float CurrentSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
    public float CurrentJumpPower { get { return jumpPower; } set { jumpPower = value; } }
    public int Score { get { return score; } set { score = value; } }
    public int BestScore { get { return bestScore; } set { bestScore = value; } }
    protected float damage = 1f;

    protected float knockBackPower = 3f;
    public float KnockBackPower { get { return knockBackPower; } }


    [Header("Character Interaction")]

    protected Vector2 knockBack = Vector2.zero;   // ��ֹ���? �ε��� ���� ĳ���Ͱ� �з����� ��
    protected float knockBackDuration = 0f;

    protected float invincibleDuration = 2f; // ���� �ð�
    public bool isInvincible = false;  // ���� ���� üũ

    private Coroutine invincibleCoroutine = null;

    private Vector3 returnPosition = Vector3.zero;   // ĳ���� ���� ��ġ
    private float returnDistance = 5f; // ĳ���� ���� �ӵ�

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
        else if (knockBack != Vector2.zero)   // �˹��� ������ ���?, �˹� ���¸� �ʱ�ȭ
        {
            RecoverKnockBack();
        }
    }

    public virtual void SetCharacterState()
    {

    }

    // memo: �����̵� ��, �ӵ��� ���ӽ�Ű�� ������ �ʿ��ұ�?
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

        if (currentHp <= 0f)    // ü���� 0 ���Ϸ� �������� �������� �Ծ��� ���?, ���? ó��
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

        // to do: ���? �ִϸ��̼� �߰�

        Destroy(gameObject, 2f);
    }

    public virtual void ApplyKnockBack(Transform other, float power)
    {
        returnPosition.x = transform.position.x;   // ĳ���� ���� ��ġ�� �˹� ���� ��ġ�� ����

        knockBackDuration = power / 3;  // �о�� ���� ���Ҽ��� �������� ���ư���
        knockBack = (other.position - transform.position).normalized * power;

        rb.velocity -= knockBack;
    }

    // ���� ��, ĳ���͸� ȭ�� �߾����� ���ͽ�Ŵ
    protected virtual void RecoverKnockBack()
    {
        float currentXPos = transform.position.x;   // �˹� ���� ĳ���� x��ǥ��
        float targetXPos = returnPosition.x;

        if (Mathf.Abs(currentXPos - targetXPos) < 0.001f)   // �˹��� ������, ĳ���Ͱ� ���� ��ġ�� �������� ���?
        {
            knockBack = Vector2.zero;
            rb.velocity = Vector2.zero;

            return;
            // returnPosition.x = currentX;   // ���� ��ġ�� ���� ��ġ�� ����
        }

        float returnXPos = Mathf.Lerp(currentXPos, targetXPos, Time.deltaTime * returnDistance);

        transform.position = new Vector3(returnXPos, transform.position.y, transform.position.z);
    }

    public virtual void ApplyInvincible()
    {
        if (invincibleCoroutine != null)    // �ڷ�ƾ �ߺ� ���� ����
        {
            StopCoroutine(invincibleCoroutine);
        }

        invincibleCoroutine = StartCoroutine(InvincibleCoroutine(invincibleDuration));
    }

    protected IEnumerator InvincibleCoroutine(float duration)
    {
        isInvincible = true;

        yield return new WaitForSeconds(duration);  // ���� �ð� ���� ���� ���� �Լ� ȣ�� ���?

        EndInvincible();

        invincibleCoroutine = null;
    }

    protected virtual void EndInvincible()
    {
        isInvincible = false;
    }
}