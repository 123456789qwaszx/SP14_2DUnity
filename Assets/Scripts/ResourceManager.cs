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

}
