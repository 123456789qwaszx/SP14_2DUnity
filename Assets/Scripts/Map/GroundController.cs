using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    private float scrollAmount;
    private float moveSpeed = 4;

    public float SetScrollAmount()
    {
        return scrollAmount ;
    }

    public float SetMoveSpeed()
    {
        return moveSpeed ;
    }


    void Start()
    {
        gameObject.AddComponent <BoxCollider2D>();
        gameObject.AddComponent <SpriteRenderer>();
        
        //이미지 찾을 시 넣기
        SpriteRenderer _sprite = GetComponent<SpriteRenderer>();
        // _sprite.sprite = Managers.Resource.Load<Sprite>("BG/BG1_Moutain/layer_1");
    }

    void Update()
    {
        
        scrollAmount = transform.lossyScale.x;
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x <= -scrollAmount)
        {
            transform.position = target.position - Vector3.left * scrollAmount;
            
        }


    }
}