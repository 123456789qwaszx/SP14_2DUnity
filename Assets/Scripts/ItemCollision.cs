using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ItemCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterController _player = collision.gameObject.GetComponent<CharacterController>();

        Items items = GetComponentInParent<Items>();

        if (_player == null || items == null)
        {
            Debug.LogWarning("Player 또는 Items가 null입니다.");
            return;
        }
        if (gameObject.CompareTag("HpRecovery"))
        {
            items.HpRecovery(_player);
        }
        else if (gameObject.CompareTag("ScaleUp"))
        {
            items.ScaleUp(_player, items.Duration);
        }
        else if (gameObject.CompareTag("SpeedUp"))
        {
            items.SpeedUp(_player, items.MaxSpeed, items.Duration);
        }
    }
}
