using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    // 원본 'GameObject prefab'을 참조하여 씬에 복사를 해줌.
    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject original = Resources.Load<GameObject>($"Prefabs/{path}");
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
        if(go == null)  
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
            if (index >=0)
            name = name.Substring(index + 1);

            GameObject go = Managers.Pool.GetOriginal(name);
            if (go != null)
            return go as T;
        }
        return Resources.Load<T>(path);
    }
}
