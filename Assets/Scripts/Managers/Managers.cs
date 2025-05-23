using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;


    static Managers Instance { get { Init(); return s_instance; } }
    
    MapManager _map = new MapManager();
    PoolManager _pool = new PoolManager();
    ResourceManager _resource = new ResourceManager();
    DataManager _data = new DataManager();

    public static MapManager Map { get { return Instance._map; } }
    public static PoolManager Pool { get { return Instance._pool; } } 
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static DataManager data { get { return Instance._data; } }

    void Start()
    {
        Init();
    }

    void Update()
    {

    }


    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
        }

        s_instance._pool.Init();
    }

}
