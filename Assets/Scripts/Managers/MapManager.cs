using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager
{
    private List<GameObject> MapInstances;

    // 직접호출 x, player onTrigger로만 호출
    public GameObject LoadMap(int mapid)
    {
        string mapName = "Map_" + mapid.ToString("000");
        GameObject map = Managers.Resource.Instantiate($"Map/{mapName}");

        return map;
    }


    List<GameObject> _Item = new List<GameObject>();
    public void SetItemToList()
    {
        // loadAll 해서 배열에 싹다 넣어버린다.
        GameObject[] parents = Resources.LoadAll<GameObject>("Prefabs/Map");
        List<GameObject> childItem = new List<GameObject>();
        List<GameObject> _Item = new List<GameObject>();

        // parents 배열에 들어가 있는놈의 child Object를 받아온다.
        for (int i = 0; i < parents.Length; i++)
        {
            childItem.Add(parents[i].transform.GetComponentInChildren<GameObject>());
        }

        // 배열에 들어가있는 놈들 중, "_Item" 인 것을 찾아서 Item리스트에 넣는다.
        for (int i = 0; i < childItem.Count; i++)
        {
            if (childItem[i].name.Contains("_Item"))
            {
                _Item.Add(parents[i]);
            }
        }
    }
    // 시작 맵설정
    public void SetdefaultMap()
    {
        MapInstances.Add(Managers.Resource.Instantiate($"Map/Map_001"));
        MapInstances.Add(Managers.Resource.Instantiate($"Map/Map_002"));
        MapInstances.Add(Managers.Resource.Instantiate($"Map/Map_003"));
        MapInstances.Add(Managers.Resource.Instantiate($"Map/Map_004"));
        //여기서 MapInstances에 추가할 필요는 없지.

        foreach (GameObject obj in MapInstances)
        {
            Managers.Resource.Destroy(obj);
        }
        // 이것도 굳이 foreach문을 돌릴 필요는 없어.

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


    //------------------------------------------------------------------------------
    // 공부용

    // 이건 
    // for (int i = 0; i <parents.Length; i++)
    //         {
    //             GameObject child = parents[i].GetComponentInChildren<GameObject>();

    //             if (child.name.Contains("_Item"))
    //             _Item.Add(child);
    //         }

    public void SetItemTrue()
    {
        //아무튼 _Item에 FindChild로 채워놨다고 치면, 얘들은 SetActivefalse인 상태...
        //그런데 SetActivefalse인 상태로 이 메소드가 먹을까? 안되면 맵을 시작하면서 이걸 메소드를 호출하자.

        // 그럼 이제 _Item의 멤버를 쭉 훝으면서, 캐릭터가 부딪혔는지 여부를 체크
        foreach (GameObject go in _Item)
        {
            if (go.activeSelf == false)
                go.SetActive(true);
        }
    }

    // 프리팹의 Map들을 <GameObject>배열에 집어 넣은 뒤, 그 배열[i].transform의 child를 쫙 불러온 뒤에 _Item이 붙어있는 것들은 모두
    // List<Transform>Item[]에 집어넣어 버린다.
    // 그리고 맵이 불러와질때, public void OnenableItem() {foreach item in Item{item.SetActive(true)};}} 이런느낌으로 켜버리는 걸 만들고, Map.LoadMap()에 저걸 넣어버린다.

    // 문제는 GameObject와 Transform 간의 자연스러운 이동과, 또 Child Transform 원본을 찾아서 넣는 방법을 모르겠어.
    // 이렇게 만드는 게 일반적인 방법은 아닌 것 같아서 정보를 찾기 힘들 것 같은데...

    //         List<GameObject> parentMap = new List<GameObject>();
    //         List<Transform> T_Item = new List<Transform>();

    //         for(int i = 0; i < parents.Length; i++)
    //         {
    //             {
    //                 parentMap.Add(parents[i].transform);
    //             }

    //             List<Transform>children = GetChildren(parentMap[i]);
    //             foreach (Transform _Item in children)
    //             {
    //                 if (_Item.name.Contains("_Item"))
    //                 T_Item.Add(_Item);
    //             }
    //         }

    //         List<Transform> GetChildren(Transform parent)
    //         {
    //             List<Transform> children = new List<Transform>();

    //             foreach(Transform child in parent)
    //             {
    //                 children.Add(child);
    //             }
    //             return children;
    //         }






    // Tilemap 좌표를 받아와서 아이템 종류에 맞춰 0,1,2,3,4로 이루어진 메모장맵을 만든 뒤, GameScene에서 그걸 토대로 아이템을 생성하는 방식
    // 장점은... 모르겠다. 하지만 가능할 것 같아.

    // 이런 함수를 쓸거면 private List<GameObject> MapInstances; 뒤에 new List<GameObject>; 이렇게 붙인다음에 여기 필드를 진짜로 계산하면서 쓴다음 그걸 꺼내가는건데 그렇게 하면 안됨.
    // public GameObject GetMap(int i)
    // {
    //     return MapInstances[i];
    // }

    // 따라서 어차피 그렇게 안 쓸거면 List를 new로 잡는게 아니라 그냥 깡통만 선언해서 쓰는게 맞음. 그걸 수정함

    // 반면 public GameObject GenerateItem() 이걸통해 캐싱하려고 했는데, 그 경우에는 실제로
    // GameObject[] gameObjects = Resources.LoadAll<GameObject>("Prefabs/Map"); 이거를 함수 내가 아니라 필드에 선언 후에, --->>>>>> 왜냐면 맵프리팹은 이미 만들어서 asset으로 만들어뒀으니까!
    // 그리고 저건 이중 for문을 돌면서 맵좌표를 쫙 훝고, 실제 타일이 있는지 체크 하고, 있다면 그것의 Vector 값을 return 받는게 맞지. ---> 이건 오직 그 Prefabs/Map 폴더와만 서로 정보를 주고 받아.
    // Q. 그런데 이렇게 한다면 list든 dic이든 그 안에 vector3값, item종류를 담아야 하는데 이게 맞을까?


    // 그러면 CheckObstacle, CheckItemHp, CheckItemScale 등등
    // 이건
    //    GameObject[] gameObjects = Resources.LoadAll<GameObject>("Prefabs/Map");
    //     foreach (GameObject go in gameObjects)
    //         Tilemap tmBase = FindChild<Tilemap>(go, "Tilemap_Base", true);
    //         Tilemap tmScore10 = FindChild<Tilemap>(go, "Tilemap_score10", true);
    // 이렇게 한번에 받아온뒤에 쭈욱~~ 선언을 해. 그런데 문제는 이렇게 받아오는 과정에서

    //      for (int y = tmBase.cellBounds.yMax; y >= tmBase.cellBounds.yMin; y--)
    //         {
    //             for (int x = tmBase.cellBounds.xMin; x <= tmBase.cellBounds.xMax; x++)
    //             {
    //                 TileBase tile0 = tmScore10.GetTile(new Vector3Int(x, y, 0));
    //                 여기까지만 적으면 Vector3Int값을 리턴 받을 수 있어.
    //List<Vector3Int> tilemapTransformPosition = new List<Vector3Int>();
    // 여기다가 벡터3인트값을 넣어!... 그런데 벌써 이게 맞는지 모르겠다. 난 모르겠다~
    // 그렇다고 여기서 Instantiate를 하는 건 어불성설. 진짜 싫어 끔찍해.
    // 차라리 빈 공간에 0을 넣고, ItemHp = 1, ItemScore = 2, 이런식으로 해서 메모장에 써버린다?
    // 그리고 새로운 함수를 만들어서, 메모장을 읽고 0이면 ~~ 1~~ 2~~ 이런식으로 알맞게 생성한다??
    // 그럴듯해...


    //                 if (tile0 !=null)
    //                 {
    //                      GameObject score10 = Managers.Resource.Instantiate("Items/score10");
    //   이 부분은 여기서 하는게 아니라, GameScene 필드에서 할 일.          score10.transform.position = new Vector3Int(x, y, 0);





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

    // public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    // {
    //     if (go == null)
    //         return null;

    //     if (recursive == false)
    //     {
    //         for (int i = 0; i < go.transform.childCount; i++)
    //         {
    //             Transform transform = go.transform.GetChild(i);
    //             if (string.IsNullOrEmpty(name) || transform.name == name)
    //             {
    //                 T component = transform.GetComponent<T>();
    //                 if (component != null)
    //                     return component;

    //             }
    //         }
    //     }
    //     else
    //     {
    //         foreach (T component in go.GetComponentsInChildren<T>())
    //         {
    //             if (string.IsNullOrEmpty(name) || component.name == null)
    //                 return component;
    //         }
    //     }

    //     return null;
    // }

}
