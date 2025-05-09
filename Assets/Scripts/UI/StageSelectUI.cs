using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ��������
// �ڷΰ���
// ����

public class StageSelectUI : MonoBehaviour
{
    public GameObject stageUICanvas;
    Button stageButton;
    Button backButton;

    void Start()
    {
        Init();
    }

    private void Init()
    {
        Transform stageCanvas = stageUICanvas.transform;

        stageButton = stageCanvas.Find("StageButton").GetComponent<Button>();
        backButton = stageCanvas.Find("BackButton").GetComponent<Button>();

        stageButton.onClick.AddListener(OnClickStageButton);
        backButton.onClick.AddListener(OnClickBackButton);
    }

    private void OnClickStageButton()
    {
        Debug.Log("�������� �̵�");
        SceneManager.LoadScene("Game");
    }

    private void OnClickBackButton()
    {
        Debug.Log("�������� ���� �̵�");
        SceneManager.LoadScene("SelectMode");
    }
}