using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager
{
    private int randomIndex;
    private float mapWidth = 0.0f;

    // 이런 배열을 오브젝트 매니저에 넣고 싶긴한데 이거 하나론 의미가 없어. 걍 여기에 씀.
    private List<GameObject> MapInstances = new List<GameObject>();


    // 활성 맵이 3개 이상이면 가장 오래된 맵을 제거
    // 또 페이드효과를 줄 수도 있을까?


    public void CreateMapRoutin()
    {
        // 처음 타일맵 프리팹은 (0,0,0) 위치에 생성
        // 화면바깥까지의 거리 - 처음생성위치 / 속도 마다 반복실행
        // mapwidth = GetTilemapWorldLength(currentTilemap);맵의 가로 길이 계산
        // 새로운 맵생성위치를 맵 가장 오른쪽 끝 위치로 설정
        // 다음 타일맵 프리팹을 활성 및 그 위치로 이동
        // 
    }


    public void SpawnRandonMap()
    {
        this.randomIndex = UnityEngine.Random.Range(0, MapInstances.Count);

        GameObject randomMap = MapInstances[randomIndex];

        randomMap.SetActive(true);
        randomMap.transform.position = new Vector3(mapWidth, 0);

        // 이렇게 아래로 빼둬야 첫 맵은 기본값인 (0, 0) 좌표에서 생성
        mapWidth = GetMapWorldWidth(randomMap);

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
