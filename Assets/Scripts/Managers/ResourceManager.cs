using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    
    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Resources.Load<GameObject>($"Prefabs/{path}");
        if (prefab == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        return Object.Instantiate(prefab, parent);
    }

    public void Destroy(GameObject go)
    {
        if(go == null)  
        return;

        Object.Destroy(go);
    }


    // 컴포넌트의 변수에 스프라이트이미지 등을 연결할 때 사용
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }
}
