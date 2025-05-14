using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �÷��̾��� ũ�⸦ ������Ű�� ������
public class GigantizeItem : Items
{
    // memo: ���۸�� �ο� ���ε� ����غ� ��
    private float scaleUpDuration = 2f; // ��������� �� �������� ���������̹Ƿ� �̰�

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterController _player = collision.gameObject.GetComponent<CharacterController>();

        if (_player == null) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            ScaleUp(_player, scaleUpDuration);
        }
    }
}
