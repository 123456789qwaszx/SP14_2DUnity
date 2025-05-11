using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    private float mapWidth {get;} = 20.0f;
    private float moveSpeed = 4.0f;

    public float SetScrollAmount()
    {
        return mapWidth ;
    }

    public float SetMoveSpeed()
    {
        return moveSpeed ;
    }


    void Start()
    {
        // gameObject.AddComponent <BoxCollider2D>();
        // gameObject.AddComponent <SpriteRenderer>();
        
        // //이미지 찾을 시 넣기
        // SpriteRenderer _sprite = GetComponent<SpriteRenderer>();
        // // _sprite.sprite = Managers.Resource.Load<Sprite>("BG/BG1_Moutain/layer_1");
    }

    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x <= -mapWidth)
        {
            gameObject.SetActive(false);
            //transform.position = target.position - Vector3.left * scrollAmount;
        }


    }
}