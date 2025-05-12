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

    // 그런데 이 속도관련 정보를 맵에서 가지고 있는게 맞을까? 실제로 움직이진 않아도 플레이어쪽에서 크기나 골드 등을
    // 싹 관리하면서 속도수치를 같이 가지고 있는게 맞을 것 같아. 속도에 따른 변화들을 한번에 뿌려야하니까.
    // 또 아이템을 먹었을 때 발동하는 스킬들ex) ModifySpeed를 맵이 아니라 플레이어가 가지는게 맞아.

    // 맵의 가로 길이를 월드 좌표 기준으로 계산
    
    

    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        // mapWidth = Managers.Map.GetMapWorldWidth(gameObject);

        // if (transform.position.x <= -mapWidth)
        // {
        //     gameObject.SetActive(false);
        // }
    }

    
}