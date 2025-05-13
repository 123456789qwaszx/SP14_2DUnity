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

    [SerializeField] private GameObject _mainUICanvas;
    [SerializeField] private GameObject _soundUICanvas;

    private Button startButton;
    private Button exitButton;
    private Button soundButton;
    private Button closeButton;

    private void Start()
    {
        Init();
    }

    public void Init() // 초기화
    {
        // 컴포넌트와 연결
        Transform mainCanvas = _mainUICanvas.transform;
        Transform soundCanvas = _soundUICanvas.transform;

        startButton = mainCanvas.Find("Button - Start").GetComponent<Button>();
        exitButton = mainCanvas.Find("Button - Exit").GetComponent<Button>();
        soundButton = mainCanvas.Find("Button - Sound").GetComponent<Button>();
        closeButton = soundCanvas.Find("Button - Close").GetComponent<Button>();

        startButton.onClick.AddListener(OnClickStartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
        soundButton.onClick.AddListener(OnClickSoundButton);
        closeButton.onClick.AddListener(OnClickSoundUICloseButton);
    }

    private void OnClickStartButton() // 시작버튼
    {
        SceneManager.LoadScene("SelectMode");
    }

    private void OnClickExitButton() // 나가기 버튼
    {
        Debug.Log("나가기");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    private void OnClickSoundButton()
    {
        _soundUICanvas.SetActive(true);
    }

    private void OnClickSoundUICloseButton()
    {
        _soundUICanvas.SetActive(false);
    }
}