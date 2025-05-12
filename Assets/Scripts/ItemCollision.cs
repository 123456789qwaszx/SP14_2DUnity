using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ItemCollision : MonoBehaviour
{
    Items items = new Items();
    private void OnCollisionEnter2D(Collision2D collision)
    {
        CharacterController _player = collision.gameObject.GetComponent<CharacterController>();

        if (_player == null) return;

        if (collision.gameObject.CompareTag("HpRecovery"))
        {
            items.HpRecovery(_player);
        }
        else if (collision.gameObject.CompareTag("ScaleUp"))
        {
            items.ScaleUp(_player, items.Duration);
        }
        else if (collision.gameObject.CompareTag("SpeedUp"))
        {
            items.SpeedUp(_player, items.MaxSpeed, items.Duration);
        }
    }
}
