using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ �뷮���� �÷��ִ� ��������
public class BigScoreItem : Items
{
    private int score = 50;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterController _player = collision.gameObject.GetComponent<CharacterController>();

        if (_player == null) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            ScoreUp(_player, score);
        }
    }
}
