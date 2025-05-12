using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager
{
    private int randomIndex;
    private float mapWidth = 0.0f;
    private float moveSpeed = 5.0f;

    // 이런 배열을 오브젝트 매니저에 넣고 싶긴한데 이거 하나론 의미가 없어. 걍 여기에 씀.
    private List<GameObject> MapInstances = new List<GameObject>();


    // 활성 맵이 3개 이상이면 가장 오래된 맵을 제거
    // 또 페이드효과를 줄 수도 있을까?


    public void CreateMapRoutin()
    {
        // 화면바깥까지의 거리 - 처음생성위치 / 속도 마다 반복실행
        // 바깝까지의 거리 = mapWidth /2
        // 이동속도 = moveSpeed
        // 즉 현재는 2초마다 1회 반복하면 됨.

        // mapwidth = GetTilemapWorldLength(currentTilemap);맵의 가로 길이 계산
        // moveSpeed는 플레이어 완성되면 받아오기


        // 또 이렇게 배열로 만든다면 내부쿨타임이 필요함.
        // 랜덤 주사위를 굴리더라도 최소한 2초동안은 똑같은 수가 나오면 안됨.


    }

    public IEnumerator MapRoutin(int mapWidth, int moveSpeed)
    {
        float a = (mapWidth / 2) / moveSpeed;
        yield return new WaitForSeconds(a);
        SpawnRandonMap();
    }


    public void SpawnRandonMap()
    {
        randomIndex = UnityEngine.Random.Range(0, MapInstances.Count);
        GameObject randomMap = MapInstances[randomIndex];

        randomMap.SetActive(true);
        mapWidth = GetMapWorldWidth(randomMap);
        randomMap.transform.position = new Vector3(mapWidth, 0);
    }


    public void LoadMap(int mapid)
    {

        string mapName = "Map_" + mapid.ToString("000");
        GameObject map = Managers.Resource.Instantiate($"Map/{mapName}");

        map.SetActive(false);
        MapInstances.Add(map);
    }


    public void DestroyMap()
    {
        GameObject map = GameObject.Find("Map");
        if (map != null)
        {
            GameObject.Destroy(map);
        }
        MapInstances.Remove(map);
    }


    public float GetMapWorldWidth(GameObject tilemapObj)
    {
        Tilemap tilemap = tilemapObj.GetComponent<Tilemap>();

        if (tilemap == null)
        {
            Debug.Log("fail to find tilemap. default = 10");
            return 10;
        }

        tilemap.CompressBounds();

        Bounds tilemapBounds = tilemap.localBounds;
        Vector3 worldSize = Vector3.Scale(tilemapBounds.size, tilemap.transform.lossyScale);
        return worldSize.x;
    }
}
