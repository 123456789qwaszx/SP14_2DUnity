using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// -스테이지 선택-
// 스테이지 선택
// 뒤로가기
// 설정

public class SelectModeUI : MonoBehaviour
{
    [SerializeField] private GameObject selectmodeUICanvas;

    Button stageButton;
    Button endlessButton;
    Button backButton;

    private void Start()
    {
        Init();
    }

    private void Init() // 초기화
    {
        // 컴포넌트와 연결
        Transform selectCanvas = selectmodeUICanvas.transform;

        stageButton = selectCanvas.Find("Button - Stage").GetComponent<Button>();
        endlessButton = selectCanvas.Find("Button - Endless").GetComponent<Button>();
        backButton = selectCanvas.Find("Button - Back").GetComponent<Button>();

        stageButton.onClick.AddListener(OnClickStageButton);
        endlessButton.onClick.AddListener(OnClickEndlessButton);
        backButton.onClick.AddListener(OnClickBackButton);
    }

    private void OnClickStageButton() // 스테이지 선택으로 이동
    {
        Debug.Log("스테이지 선택로 이동");
        SceneManager.LoadScene("StageSelect");
    }

    private void OnClickEndlessButton() // 무한모드로 이동
    {
        Debug.Log("무한모드로 이동");
    }

    private void OnClickBackButton() // 뒤로가기
    {
        Debug.Log("뒤로이동");
        SceneManager.LoadScene("Main");
    }
}
