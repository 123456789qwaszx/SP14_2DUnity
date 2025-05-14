using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectCharacterUI : MonoBehaviour
{
    [SerializeField] private GameObject selectCharacterUICanvas;
    Button selectBtn;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        Transform characterCanvas = selectCharacterUICanvas.transform;
        selectBtn = characterCanvas.Find("ApplyBtn").GetComponent<Button>();

        selectBtn.onClick.AddListener(OnClickSelectBtn);
    }

    void OnClickSelectBtn()
    {
        Debug.Log("모드 선택으로 이동");
        SceneManager.LoadScene("SelectMode");
    }
}
