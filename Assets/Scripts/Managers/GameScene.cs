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

    
    }

    public IEnumerator MapRoutin()
    {
        // float a = (mapWidth / 2) / moveSpeed;
        yield return new WaitForSeconds(6f);
        Managers.Map.SpawnRandonMap();
    }

    void Start()
    {
        GameObject map = Managers.Resource.Instantiate("Map/Map_default");
        map.name = "Map";

        Managers.Map.LoadMap(1);
        Managers.Map.LoadMap(2);
        Managers.Map.LoadMap(3);
        Managers.Map.LoadMap(4);
        Managers.Map.LoadMap(5);
        Managers.Map.LoadMap(6);

        Managers.Map.SpawnRandonMap();
        StartCoroutine(MapRoutin());
    }

    

    public override void Clear()
    {

    }
}
