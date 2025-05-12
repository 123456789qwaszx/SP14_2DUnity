using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ItemCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision, Items items)
    {
        CharacterController _player = collision.gameObject.GetComponent<CharacterController>();

        if (_player == null) return;

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
