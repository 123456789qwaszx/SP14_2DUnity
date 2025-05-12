using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �÷��̾�� ĳ���� �߰��� ����� ĳ���� �⺻ Ŭ����
public class CharacterBaseController : MonoBehaviour
{
    protected Rigidbody2D rb; // ĳ���ʹ� �����Ǿ� �־ ���� ���� �ൿ�� ���� �ʿ�
    protected Animator anim;

    [Header("Character State")]
    [Tooltip("ĳ������ ����Ʈ �������ͽ�")]
    protected float maxHp = 3f;    // memo: 3���� ��Ʈ�� ������ ���� ���, �ݸ� ��� ��Ȳ�� �����Ͽ� float�� ����
    protected float currentHp = 0f;
    protected float moveSpeed = 5f;
    protected float currentSpeed = 0f;
    protected float jumpPower = 3f;
    protected float currentJumpPower = 0f;
    protected int maxJumpCount = 1;    // ĳ���Ͱ� ���� ���� Ƚ��
    protected int jumpCount = 0;
    protected bool isJumping = false;
    protected float slidePower = 2f;
    protected bool isSliding = false;
    protected bool isGround = false;

    [Header("Character Interaction")]
    protected Vector2 knockBack = Vector2.zero;   // ��ֹ��� �ε��� ���� ĳ���Ͱ� �з����� ��
    protected float knockBackDuration = 0f;

    protected float invincibleDuration = 2f; // ���� �ð�
    protected bool isInvincible = false;  // ���� ���� üũ
    private Coroutine invincibleCoroutine = null;

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

        if (knockBackDuration <= 0f)
        {
            RecoverKnockBack();
        }
    }

    protected virtual void SetCharacterState()
    {

    }

    // memo: �����̵� ��, �ӵ��� ���ӽ�Ű�� ������ �ʿ��ұ�?
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
        
        if (currentHp <= 0f)    // ü���� 0 ���Ϸ� �������� �������� �Ծ��� ���, ��� ó��
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

        // to do: ��� �ִϸ��̼� �߰�

        Destroy(gameObject, 2f);
    }

    protected virtual void ApplyKnockBack(Transform other, float power)
    {
        knockBackDuration = power / 1;  // �о�� ���� ���Ҽ��� �������� ���ư���
        knockBack = (other.position - transform.position).normalized * power;

        rb.velocity -= knockBack;
    }

    // ���� ��, ĳ���͸� ȭ�� �߾����� ���ͽ�Ŵ
    protected virtual void RecoverKnockBack()
    {
        
    }

    protected virtual void ApplyInvincible()
    {
        if (invincibleCoroutine != null)    // �ڷ�ƾ �ߺ� ���� ����
        {
            StopCoroutine(invincibleCoroutine);
        }

        invincibleCoroutine = StartCoroutine(InvincibleCoroutine(invincibleDuration));
    }

    private IEnumerator InvincibleCoroutine(float duration)
    {
        isInvincible = true;

        yield return new WaitForSeconds(duration);  // ���� �ð� ���� ���� ���� �Լ� ȣ�� ���

        EndInvincible();

        invincibleCoroutine = null;
    }

    protected virtual void EndInvincible()
    {
        isInvincible = false;
    }
}
