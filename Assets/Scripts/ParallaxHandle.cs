using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxHandle : MonoBehaviour
{
    // 직접 패럴랙스 프리팹에 연결해서 사용

    // 난이도가 올라감에 따라 속도 상승. obstacle, Item 등과 같이 쓰는 아마 난이도계수 같은 걸 추가해서 곱하면 될듯
    public float Bg_MoveSpeed = 1.5f;

    // Bg_SpeedMultiplier[], Layer_Objects[] 둘다 직접 연결, 프리팹
    [Header("Layer Setting")]
    public float[] Bg_SpeedMultiplier = new float[5];
    public GameObject[] Layer_Objects = new GameObject[5];

    public Vector3 startPosition { get; set; } = Vector3.zero;

    void Start()
    {

    }

    void Update()
    {
        startPosition += Vector3.left * Time.deltaTime * Bg_MoveSpeed;
        transform.position = startPosition;


        for (int i = 0; i < 5; i++)
        {
            Layer_Objects[i].transform.position = new Vector3(transform.position.x * (1 + Bg_SpeedMultiplier[i]), startPosition.y);

        }
    }
}

// private float backgroundWidth; // 배경 가로넓이
// private float backgroundCount; // 배경 숫자
// float repeatDistance = this.backgroundWidth * this.backgroundCount / 2f;
// float distance = playerTransformpositionx - this.transform.position.x;

//repeatDistance보다 많이 이동 시, transform.position을 -1로 뒤집음

// if (Mathf.Abs(distance) > repeatDistance)
// {
//     this.transform.position += new Vector3(repeatDistance * Mathf.Sign(distance), 0f, 0f);
// }


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
//     private float boundSizeX;
//     private float sizeX;

//         sizeX = Layer_Objects[0].transform.localScale.x;
//         boundSizeX = Layer_Objects[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x;


