using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    private float mapWidth;
    private float moveSpeed { get; set; } = 5.0f;
    private float speedModifier = 0f;

    public float CurrentSpeed => this.moveSpeed + speedModifier;


    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        mapWidth = Managers.Resource.GetMapWorldWidth(gameObject);

        if (transform.position.x <= -mapWidth)
        {
            //Managers.Resource.Destroy(gameObject);
        }
    }
}