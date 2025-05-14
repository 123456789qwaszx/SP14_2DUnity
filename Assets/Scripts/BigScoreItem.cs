using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 점수를 대량으로 올려주는 아이템템
public class BigScoreItem : MonoBehaviour
{
    private int score = 50;

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
