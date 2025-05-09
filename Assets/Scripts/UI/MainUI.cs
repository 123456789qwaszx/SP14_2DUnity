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

    public GameObject mainUICanvas;
    Button startButton;
    Button exitButton;

    private void Start()
    {
        Init();
    }

    public void Init() // �ʱ�ȭ
    {
        // ������Ʈ�� ����
        Transform mainCanvas = mainUICanvas.transform;

        startButton = mainCanvas.Find("StartButton").GetComponent<Button>();
        exitButton = mainCanvas.Find("ExitButton").GetComponent<Button>();

        startButton.onClick.AddListener(OnClickStartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    private void OnClickStartButton() // ���۹�ư
    {
        SceneManager.LoadScene("SelectMode");
    }

    private void OnClickExitButton() // ������ ��ư
    {
        Debug.Log("������");
        Application.Quit();
    }
}
