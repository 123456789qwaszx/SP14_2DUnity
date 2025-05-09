using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainUI : MonoBehaviour
{
    // -메인 화면-
    // 시작, 나가기
    // 설정

    public GameObject mainUICanvas;
    Button startButton;
    Button exitButton;

    private void Start()
    {
        Init();
    }

    public void Init() // 초기화
    {
        // 컴포넌트와 연결
        Transform mainCanvas = mainUICanvas.transform;

        startButton = mainCanvas.Find("StartButton").GetComponent<Button>();
        exitButton = mainCanvas.Find("ExitButton").GetComponent<Button>();

        startButton.onClick.AddListener(OnClickStartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    private void OnClickStartButton() // 시작버튼
    {
        SceneManager.LoadScene("SelectMode");
    }

    private void OnClickExitButton() // 나가기 버튼
    {
        Debug.Log("나가기");
        Application.Quit();
    }
}
