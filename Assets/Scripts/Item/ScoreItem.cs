using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ ������Ű�� ������
public class ScoreItem : Items
{
    private int score = 10;

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
