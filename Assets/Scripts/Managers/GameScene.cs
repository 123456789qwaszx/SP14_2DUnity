using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    List<GameObject> maplist = new List<GameObject>();

    protected override void Init()
    {
        base.Init();

        GameObject player = Managers.Resource.Instantiate("Player");
        player.name = "Player";

        GameObject parallax = Managers.Resource.Instantiate("Parallax/Parallax_001");
        parallax.name = "Parallax";


        for (int i = 1; i < 5; i++)
        maplist.Add(Managers.Resource.Instantiate($"Map/Map_001"));
        maplist.Add(Managers.Resource.Instantiate($"Map/Map_002"));

        foreach (GameObject obj in maplist)
        {
            Managers.Resource.Destroy(obj);
        }

        Managers.Resource.Instantiate("Map/Map_default");
        Managers.Resource.Instantiate($"Map/Map_001");
        GameObject RoutinMap = GameObject.Find("Map_001");
        
        float mapWidth = Managers.Resource.GetMapWorldWidth(RoutinMap);
        RoutinMap.transform.position = new Vector3(mapWidth, 0);

        //StartCoroutine(CreateMap());
        
    }

    void Start()
    {

    }

    // IEnumerator CreateMap()
    // {
    //     int randomIndex = UnityEngine.Random.Range(0, maplist.Count);

    //     while (true)
    //     {
    //         Managers.Resource.Instantiate($"Map/Map_001");
    //         GameObject RoutinMap = GameObject.Find("Map_001");
    //         float mapWidth = Managers.Resource.GetMapWorldWidth(RoutinMap);
    //         RoutinMap.transform.position = new Vector3(mapWidth, 0);

    //         yield return new WaitForSeconds(8.0f);

    //         // 우린 맵을 순서대로 생성하기로 했으니까.
    //         // 풀매니저에서 GetPool을 해준다.
    //         // 그리고 count ++ 로 인덱스를 1더해주고
    //         // yield (화면바깥까지의 거리 - 처음생성위치)의 절대값 / 속도의 절대값 마다 반복실행
    //     }
    // }
    

    

    public override void Clear()
    {
        
    }
}
