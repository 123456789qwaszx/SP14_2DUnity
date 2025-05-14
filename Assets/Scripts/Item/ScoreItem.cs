using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 점수를 증가시키는 아이템
public class ScoreItem : Items
{
    private int score = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CharacterController _player = collision.gameObject.GetComponent<CharacterController>();

        if (_player == null) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            // ScoreUp(score);  // 점수 증가 함수
        }
    }
}
