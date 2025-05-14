using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �÷��̾��� �ӵ��� ������Ű�� ��������
public class AccelItem : Items
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        CharacterController _player = collision.gameObject.GetComponent<CharacterController>();

        if (_player == null) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            SpeedUp(_player, MaxSpeed, Duration);
        }
    }
}
