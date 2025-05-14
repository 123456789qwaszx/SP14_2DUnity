using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 점수를 대량으로 올려주는 아이템템
public class BigScoreItem : Items
{
    private int score = 50;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterController _player = collision.gameObject.GetComponent<CharacterController>();

        if (_player == null) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            ScoreUp(_player, score);  // 점수 증가 함수
        }
    }
}
