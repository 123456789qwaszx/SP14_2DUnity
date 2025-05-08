using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �÷��̾�� ĳ���� �߰��� ����� ĳ���� �⺻ Ŭ����
public class CharacterBaseController : MonoBehaviour
{
    protected Rigidbody rb; // ĳ���ʹ� �����Ǿ� �־ ���� ���� �ൿ�� ���� �ʿ�

    [SerializeField] private SpriteRenderer characterSprite;
    [SerializeField] private Animator anim;

    private Vector2 knockBack = Vector2.zero;   // ��ֹ��� �ε��� ���� ĳ���Ͱ� �з����� ��
    private float knockBackDuration = 0f;

    private float invincibleDuration = 0f; // ���� �ð�

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }

    protected virtual void FixedUpdate()
    {

    }

    protected virtual void ApplyKnockBack(Transform other, float power, float duration)
    {
        knockBackDuration = duration;
        knockBack = (other.position - transform.position).normalized * power;
    }

    protected virtual void ApplyInvincible()
    {

    }
}
