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
        MapInstances.Add(Managers.Resource.Instantiate($"Map/Map_005"));

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

    // 아이템 생성관련 
    // public Grid CurrentGrid {get; private set;}

    // public int MinX {get; set;}
    // public int MaxX {get; set;}
    // public int MinY {get; set;}
    // public int MaxY {get; set;}

    // public int SizeX {get {return MaxX - MinX +1;}}
    // public int SizeY {get {return MaxY = MinY +1;}}

    // bool[,] _collision;

    
    // public GameObject GenerateItem()
    // {
    //     GameObject[] gameObjects = Resources.LoadAll<GameObject>("Prefabs/Map");

    //     foreach (GameObject go in gameObjects)
    //     {
    //         Tilemap tmBase = FindChild<Tilemap>(go, "Tilemap_Base", true);
    //         Tilemap tmScore10 = FindChild<Tilemap>(go, "Tilemap_score10", true);
    //         Tilemap tmScore50 = FindChild<Tilemap>(go, "Tilemap_Score50", true);
    //         Tilemap tmHpRecovery = FindChild<Tilemap>(go, "Tilemap_HpRecovery", true);
    //         Tilemap tmSpeedUp = FindChild<Tilemap>(go, "Tilemap_SpeedUp", true);
    //         Tilemap tmScaleUp = FindChild<Tilemap>(go, "Tilemap_Scale Up", true);

    //         for (int y = tmBase.cellBounds.yMax; y >= tmBase.cellBounds.yMin; y--)
    //         {
    //             for (int x = tmBase.cellBounds.xMin; x <= tmBase.cellBounds.xMax; x++)
    //             {
    //                 TileBase tile0 = tmScore10.GetTile(new Vector3Int(x, y, 0));
    //                 if (tile0 !=null)
    //                 {
    //                     GameObject score10 = Managers.Resource.Instantiate("Items/score10");
    //                     score10.transform.position = new Vector3Int(x, y, 0);
    //                     Debug.Log("1");
    //                     return score10;
    //                 }
                    
    //                 TileBase tile1 = tmScore50.GetTile(new Vector3Int(x, y, 0));
    //                 if (tile1 !=null)
    //                 {
    //                     GameObject score50 = Managers.Resource.Instantiate("Items/score50");
    //                     score50.transform.position = new Vector3Int(x, y, 0);
    //                     Debug.Log("12");
    //                     return score50;
    //                 }

    //                 TileBase tile2 = tmHpRecovery.GetTile(new Vector3Int(x, y, 0));
    //                 if (tile2 !=null)
    //                 {
    //                     GameObject HpRecovery = Managers.Resource.Instantiate("Items/HpRecovery");
    //                     HpRecovery.transform.position = new Vector3Int(x, y, 0);
    //                     return HpRecovery;
    //                 }

    //                 TileBase tile3 = tmSpeedUp.GetTile(new Vector3Int(x, y, 0));
    //                 if (tile3 !=null)
    //                 {
    //                     GameObject SpeedUp = Managers.Resource.Instantiate("Items/SpeedUp");
    //                     SpeedUp.transform.position = new Vector3Int(x, y, 0);
    //                     return SpeedUp;
    //                 }

    //                 TileBase tile4 = tmScaleUp.GetTile(new Vector3Int(x, y, 0));
    //                 if (tile4 !=null)
    //                 {
    //                     GameObject ScaleUp = Managers.Resource.Instantiate("Items/ScaleUp");
    //                     ScaleUp.transform.position = new Vector3Int(x, y, 0);
    //                     return ScaleUp;
    //                 }
    //             }
    //         }
    //     }
    //     return null;
    // }

    // public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    // {
    //     Transform transform = FindChild<Transform>(go, name, recursive);
    //     if (transform == null)
    //     return null;

    //     return transform.gameObject;
    // }

    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false)where T : UnityEngine.Object
    {
        if (go == null)
            return null;
            
        if (recursive == false)
        {
            for(int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if(string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                    return component;
                    
                }
            }
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if(string.IsNullOrEmpty(name) || component.name == null)
                return component;
            }
        }

        return null;
    }

}
