using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager
{
    private int randomIndex;
    private float mapWidth {get; set;} = 20.0f;

    private GameObject[] mapPrefabs;
    private List<GameObject> MapInstances = new List<GameObject>();


    void SpawnMap(int mapid)
    {
        foreach (var prefab in mapPrefabs)
        {
            string mapName = "Map_" + mapid.ToString("000");

            GameObject go = Managers.Resource.Instantiate($"Map/{mapName}");
            go.SetActive(false);
            MapInstances.Add(go);
        }

    }


    void UseRandomMap()
    {
        this.randomIndex = UnityEngine.Random.Range(0, MapInstances.Count);

        GameObject randomMap = MapInstances[randomIndex];

        randomMap.SetActive(true);
        randomMap.transform.position = new Vector3(mapWidth, 0);
    }

    
    public void DestroyMap()
    {
        GameObject map = GameObject.Find("Map");
        if (map != null)
        {
            GameObject.Destroy(map);
        }

    }
}
