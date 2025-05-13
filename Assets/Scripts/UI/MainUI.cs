using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainUI : MonoBehaviour
{
    // -���� ȭ��-
    // ����, ������
    // ����

    public GameObject _mainUICanvas;
    public GameObject _soundUICanvas;

    Button startButton;
    Button exitButton;
    Button soundButton;
    Button closeButton;

    private void Start()
    {
        Init();
    }

    public void Init() // �ʱ�ȭ
    {
        // ������Ʈ�� ����
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

    private void OnClickStartButton() // ���۹�ư
    {
        SceneManager.LoadScene("SelectMode");
    }

    private void OnClickExitButton() // ������ ��ư
    {
        Debug.Log("������");
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