using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBaseController : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("Obstacle State")]
    public float damage = 1f;
    public virtual float Damage { get { return damage; } }

    private float knockBackPower = 3f;
    public virtual float KnockBackPower { get { return knockBackPower; } }

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }

    // memo: �÷��̾�� ��ֹ� �浹ó���� �� ���, Obstacle�� ��ӹ޴� Ŭ�������� �ٸ� ó���� �ϱ� ��������� ����
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾� �浹 ó��
        if (collision.gameObject.CompareTag("Player"))
        {
            CharacterController character = collision.gameObject.GetComponent<CharacterController>();

            if (character.isInvincible)
            {
                character.Damage(damage);
                character.ApplyKnockBack(transform, knockBackPower);
                character.ApplyInvincible();
            }
        }
    }
}
