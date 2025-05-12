using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance; // 유일성이 보잠됨.

    // public static Managers GetInstance()
    //{
    //    return s_instance;
    //}
    //위처럼 쓰면, 다른 곳에서 불러올 때, Managers.GetInstance() 이렇게 불러오게됨. 그게 보기 싫어 아래처럼 바꿈.
    // Managers.Instance. 이런식으로 쓰기 편하게 다듬은 것.

    static Managers Instance { get { Init(); return s_instance; } } //유일한 매니저를 가지고 온다.
    // Init() 재귀식으로 여러번 실행될까 걱정되지만, 실제론 Manager를 불러올 때 1번씩만 실행됨.

    ObjectManager _object = new ObjectManager();
    ResourceManager _resource = new ResourceManager();

    public static ObjectManager Object {get { return Instance._object; }} 
    public static ResourceManager Resource { get { return Instance._resource; } }

    void Start()
    {
        // s_instance = this; 이렇게 기본적인 형태로 불러오면 @Managers가 여러개일 때 static이 불성립으로 오류남
        // 따라서
        // GameObject go = GameObject.Find("@Managers");
        //  s_instance = go.GetComponent<Managers>() 이렇게 원본 하나를 꼭 찍어서 선언함.

        // 그런데 위의 내용을 Init으로 빼놓지 않고 void Start에서 실행할 경우
        // start 이전의 void awake 같은 곳에서 Manager.Instance를 호출할 경우 아직 s_instance는 null이라 뻥 터짐.

        // 이걸 해결하기 해설강의에선 void awake에 작성을 했는데,
        // 그 방법 대신 Init()으로 뺴둔 뒤, Instance를 만드는 동시에 한번 실행하고 넣어버림. 이게 16번 줄에 Init()이 들어간 이유
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
    }

}
