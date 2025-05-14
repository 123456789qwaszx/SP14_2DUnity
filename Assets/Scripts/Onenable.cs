using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onenable : MonoBehaviour
{
    [SerializeField]
    GameObject[] GameObj;

    private void OnEnable()
    {
        for (int i = 0; i < GameObj.Length; i++)
        {
            GameObj[i].SetActive(true);
        }
    }
}
