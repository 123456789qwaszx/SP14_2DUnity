using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어의 크기를 증가시키는 아이템
public class GigantizeItem : Items
{
    // memo: 슈퍼모드 부여 여부도 고려해볼 것
    private float scaleUpDuration = 2f; // 사이즈업은 이 아이템의 고유변수이므로 이관

    public List<ObstacleBaseController> ob = new List<ObstacleBaseController>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterController _player = collision.gameObject.GetComponent<CharacterController>();

        if (_player == null) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            if (_player.isInvincible)
            {
                _player.Damage(1);

                _player.ApplyKnockBack(GameObject.FindWithTag("Obstacle").transform, Duration);

                _player.ApplyInvincible();

            }
            ScaleUp(_player, scaleUpDuration);
        }
    }
}
