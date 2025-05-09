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