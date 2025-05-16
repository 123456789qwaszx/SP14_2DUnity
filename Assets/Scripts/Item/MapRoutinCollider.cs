using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRoutinCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            int randomIndex = UnityEngine.Random.Range(1, 5);

            GameObject randomMap = Managers.Map.LoadMap(randomIndex);

            float mapWidth = Managers.Map.GetMapWorldWidth(randomMap);
            randomMap.transform.position = new Vector3(mapWidth, 0);
        }
    }
}
