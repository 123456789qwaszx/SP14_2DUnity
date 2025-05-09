using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager
{

    // Resources.Load()내에서 탐색과, 메모리 할당이 일어나는 듯. 각 상황 초기화시나 정적으로 사용하면 모르지만,
    // 실시간으로 사용을 많이 하게 되면, 성능에 영향을 줄 수 있으므로, 이렇게 불러온뒤엔 리스트에 넣어버리고 재사용하기...???
    
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
    
}
