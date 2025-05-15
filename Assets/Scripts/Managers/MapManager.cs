using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager
{
    private List<GameObject> MapInstances = new List<GameObject>();

    // 직접호출 x, player onTrigger로만 호출
    public GameObject LoadMap(int mapid)
    {
        string mapName = "Map_" + mapid.ToString("000");
        GameObject map = Managers.Resource.Instantiate($"Map/{mapName}");

        return map;
    }

    // 시작 맵설정
    public void SetdefaultMap()
    {
        MapInstances.Add(Managers.Resource.Instantiate($"Map/Map_001"));
        MapInstances.Add(Managers.Resource.Instantiate($"Map/Map_002"));
        MapInstances.Add(Managers.Resource.Instantiate($"Map/Map_003"));
        MapInstances.Add(Managers.Resource.Instantiate($"Map/Map_004"));

        foreach (GameObject obj in MapInstances)
        {
            Managers.Resource.Destroy(obj);
        }

        Managers.Resource.Instantiate("Map/Map_default");
        GameObject map = Managers.Resource.Instantiate($"Map/Map_001");
        
        float mapWidth = Managers.Map.GetMapWorldWidth(map);
        map.transform.position = new Vector3(mapWidth, 0);
    }


    public float GetMapWorldWidth(GameObject tilemapObj)
    {
        Tilemap tilemap = tilemapObj.GetComponent<Tilemap>();

        if (tilemap == null)
        {
            Debug.Log("fail to find tilemap. default = 20");
            return 20;
        }

        tilemap.CompressBounds();
        Bounds tilemapBounds = tilemap.localBounds;
        Vector3 worldSize = Vector3.Scale(tilemapBounds.size, tilemap.transform.lossyScale);
        return worldSize.x;
    }

}
