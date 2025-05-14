using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어의 체력을 회복시키는 아이템
public class HealItem : Items
{
    private float healPoint = 1f;   // 현재 체력 회복 함수의 회복량은 1으로 고정되어 있음

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
