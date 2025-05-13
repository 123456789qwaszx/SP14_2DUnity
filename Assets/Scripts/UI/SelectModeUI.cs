using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// -�������� ����-
// �������� ����
// �ڷΰ���
// ����

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

    private void Init() // �ʱ�ȭ
    {
        // ������Ʈ�� ����
        Transform selectCanvas = selectmodeUICanvas.transform;

        stageButton = selectCanvas.Find("Button - Stage").GetComponent<Button>();
        endlessButton = selectCanvas.Find("Button - Endless").GetComponent<Button>();
        backButton = selectCanvas.Find("Button - Back").GetComponent<Button>();

        stageButton.onClick.AddListener(OnClickStageButton);
        endlessButton.onClick.AddListener(OnClickEndlessButton);
        backButton.onClick.AddListener(OnClickBackButton);
    }

    private void OnClickStageButton() // �������� �������� �̵�
    {
        Debug.Log("�������� ���÷� �̵�");
        SceneManager.LoadScene("StageSelect");
    }

    private void OnClickEndlessButton() // ���Ѹ��� �̵�
    {
        Debug.Log("���Ѹ��� �̵�");
    }

    private void OnClickBackButton() // �ڷΰ���
    {
        Debug.Log("�ڷ��̵�");
        SceneManager.LoadScene("Main");
    }
}
