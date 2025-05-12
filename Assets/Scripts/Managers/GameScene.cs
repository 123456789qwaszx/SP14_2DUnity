using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    public List<GameObject> MapInstances = new List<GameObject>();

    protected override void Init()
    {
        base.Init();

        GameObject player = Managers.Resource.Instantiate("Player");
        player.name = "Player";

        GameObject parallax = Managers.Resource.Instantiate("Parallax/Parallax_001");
        parallax.name = "Parallax";

        GameObject map = Managers.Resource.Instantiate("Map/Map_default");

        //Managers.Resource.Instantiate("Map/Map_001");

        for (int i = 1; i < 7; i++)
        Managers.Resource.LoadMap(i);

        //Managers.Map.SpawnRandonMap();


    
    }

    void Start()
    {

        //StratCoroutine(CreateMap());
    }

    IEnumerator CreateMap()
    {
        while (true)
        {
            // 우린 맵을 순서대로 생성하기로 했으니까.
            // 풀매니저에서 GetPool을 해준다.
            // 그리고 count ++ 로 인덱스를 1더해주고
            // yield (화면바깥까지의 거리 - 처음생성위치)의 절대값 / 속도의 절대값 마다 반복실행
        }
    }

    

    public override void Clear()
    {
        
    }
}
