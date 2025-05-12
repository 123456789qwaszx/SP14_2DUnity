using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ResourceManager
{
    // 처음 맵 오브젝트 생성시,
    // Resource의 Instantiate 부분 안에 PoolManager의 Pop함수 호출 -> 풀이 존재 하지 않는 상태이므로 CreatePool 호출 -> 풀을 생성하고 Pool클래스의 Init호출 ->
    // count의 디폴트 값은 3이므로 3개의 게임 오브젝트를 생성해서 스택에 넣어준다.
    // 스택에 넣어주는 Pool클래스의 Push함수 과정에서 이 오브젝트의 부모는 @Pool_Root 산하가 된다.
    // 첫번째 오브젝트는 풀을 만들고 스택 3개를 생성해 넣어주는 위 과정을 끝내고 즉시 Pop으로 튀어나온다.
    // 2~3번째 오브젝트들은 위에서 만든 3크기의 스택에서(현재 크기2) 하나하나 빼와서 재활용 한다. 비활성화 된 상태에서 풀에 있던 오브젝트들을 빼온 뒤 SetActive(true)


    // 원본 'GameObject prefab'을 참조하여 씬에 복사를 해줌.
    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject original = Load<GameObject>($"Prefabs/{path}");
        if (original == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        if (original.GetComponent<Poolable>() != null)
            return Managers.Pool.Pop(original, parent).gameObject;

        GameObject go = Object.Instantiate(original, parent);
        go.name = original.name;
        return go;
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        //만약게 풀링이 필요한 아이라면, 풀링 매니저에게 위탁.
        Poolable poolable = go.GetComponent<Poolable>();
        if (poolable != null)
        {
            Managers.Pool.Push(poolable);
            return;
        }

        Object.Destroy(go);
    }


    // 메모리에 로드를 하는 방식. 컴포넌트의 변수에 스프라이트이미지 등을 연결할 때 사용
    // 씬에서는 객체가 표현되지 않고, 메모리로만 들고 있음.
    public T Load<T>(string path) where T : Object
    {
        if (typeof(T) == typeof(GameObject))
        {
            string name = path;
            int index = name.LastIndexOf('/');
            if (index >= 0)
                name = name.Substring(index + 1);

            GameObject go = Managers.Pool.GetOriginal(name);
            if (go != null)
                return go as T;
        }

        return Resources.Load<T>(path);
    }


    public void LoadMap(int mapid)
    {
        string mapName = "Map_" + mapid.ToString("000");
        GameObject map = Managers.Resource.Instantiate($"Map/{mapName}");
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
