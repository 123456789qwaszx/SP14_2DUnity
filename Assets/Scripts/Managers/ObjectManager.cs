using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager
{
    List<GameObject> _objects = new List<GameObject>();

    public void Add(GameObject go)
    {
        _objects.Add(go);
    }

    public void Remove(GameObject go)
	{
		_objects.Remove(go);
	}

    public void Clear()
	{
		_objects.Clear();
	}
    
    public void Find()
    {
        
    }
}
