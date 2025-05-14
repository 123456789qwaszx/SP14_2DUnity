using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �÷��̾��� ü���� ȸ����Ű�� ������
public class HealItem : Items
{
    private float healPoint = 1f;   // ���� ü�� ȸ�� �Լ��� ȸ������ 1���� �����Ǿ� ����

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterController _player = collision.gameObject.GetComponent<CharacterController>();

        if (_player == null) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            HpRecovery(_player);
        }
    }
}
