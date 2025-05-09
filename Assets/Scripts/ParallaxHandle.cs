using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxHandle : MonoBehaviour
{
    // 패럴랙스 특성상, 자동화시키더라도,
    // 아티스트가 직접 미묘한 느낌을 잡아야함
    // Unity 상에서 아래 네 개의 수치를 직접 조절 할 것
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float scrollAmount;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private Vector3 moveDirection;

    void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        if (transform.position.x <= -scrollAmount)
        {
            transform.position = target.position - moveDirection * scrollAmount;
        }
    }
}

// 자동화시 참고

// private float backgroundWidth;
// private float backgroundCount;
// float repeatDistance = this.backgroundWidth * this.backgroundCount / 2f;
// float distance = playerTransformpositionx - this.transform.position.x;

//repeatDistance보다 많이 이동 시, transform.position을 -1로 뒤집음

// if (Mathf.Abs(distance) > repeatDistance)
// {
//     this.transform.position += new Vector3(repeatDistance * Mathf.Sign(distance), 0f, 0f);
// }


// private float Bg_MoveSpeed = 3f;

//     [Header("Layer Setting")]
//     public GameObject[] Layer_Objects = new GameObject[5];
//     public float[] Bg_SpeedMultiplier = new float[5];

// for (int i = 0; i < 5; i++)
// {
//     Layer_Objects[i].transform.position = new Vector3(transform.position.x * Bg_SpeedMultiplier[i], startPosition.y);

//     if (transform.position.x * (1 - Bg_SpeedMultiplier[i]) > Layer_Objects[i].transform.position.x + boundSizeX * sizeX)
//     {
//         Layer_Objects[i].transform.position = new Vector3(boundSizeX * sizeX, startPosition.y);
//     }

//     else if (transform.position.x * (1 - Bg_SpeedMultiplier[i]) < Layer_Objects[i].transform.position.x - boundSizeX * sizeX)
//     {
//         Layer_Objects[i].transform.position = new Vector3(boundSizeX * sizeX, startPosition.y);
//     }
//  }


//  private float boundSizeX;
//  private float sizeX;
//      sizeX = Layer_Objects[0].transform.localScale.x;
//      boundSizeX = Layer_Objects[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x;

//      for (int i = 0; i < 5; i++)
//      {
//          Layer_Objects[i].transform.position = new Vector3(transform.position.x * (1 + Bg_SpeedMultiplier[i]), transform.position.y);
//      }

