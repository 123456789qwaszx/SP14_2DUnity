using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    // -���� ȭ��-
    // ����, ������
    // ����

    // -�������� ����-
    // �������� ����
    // �ڷΰ���
    // ����

    // -�ΰ���-
    // ü�¹� ǥ��
    // ���� ǥ��
    // �Ͻ����� ǥ��
    // �����̵�, ���� ǥ��

    // -���� ����-
    // �ٽ��ϱ�, ������
    // ����




    // ���� ȭ��
    //public GameObject mainUICanvas;
    Button startButton;
    Button exitButton;

    //// ��� ���� ȭ��
    //public GameObject selectmodeUICanvas;
    //Button stageButton;
    //Button endlessButton;

    //// �ΰ���UI
    //public GameObject gameUICanvas;
    //TextMeshProUGUI currentScoreTxt; // ���� ���� 
    //TextMeshProUGUI bestScoreTxt; // �ְ� ����
    ////SpriteRenderer hpBar; // ü��
    ////Button stopButton; // �Ͻ� ����
    //Button jumpButton; // ���� ��ư
    //Button slidingButton; // �����̵� ��ư

    private void Start()
    {
        MainUIInit();
        //SelectModeUIInit();
        //GameUIInit();
    }

    #region ���� UI

    public void MainUIInit() // �ʱ�ȭ
    {
        //if (startUICanvas == null) return;

        // ������Ʈ�� ���� - MainUI�� UI��ũ��Ʈ�� ����
        //Transform startcanvasTrans = startUICanvas.transform;
        Transform mainCanvas = transform.Find("Canvas");

        if (mainCanvas != null)
        {
            startButton = mainCanvas.Find("StartButton").GetComponent<Button>();
            exitButton = mainCanvas.Find("ExitButton").GetComponent<Button>();

            //startButton.onClick.AddListener(OnClickStartButton);
            //exitButton.onClick.AddListener(OnClickExitButton);
            if (startButton != null) startButton.onClick.AddListener(OnClickStartButton);
            else Debug.LogError("StartButton�� ã�� �� �����ϴ�!");

            if (exitButton != null) exitButton.onClick.AddListener(OnClickExitButton);
            else Debug.LogError("ExitButton�� ã�� �� �����ϴ�!");
        }
        {
            Debug.LogError("Canvas ������Ʈ�� ã�� �� �����ϴ�!");
        }
    }

    private void OnClickStartButton()
    {
        if (startButton != null)
        {
            Debug.Log("����");
            SceneManager.LoadScene("SelectMode");
        }
    }

    private void OnClickExitButton()
    {
        if (exitButton != null)
        {
            Debug.Log("������");
            Application.Quit();
        }
    }

    #endregion

    //#region �ΰ��� UI

    //public void GameUIInit() // �ʱ�ȭ
    //{
    //    if (gameUICanvas == null) return;

    //    // ������Ʈ�� ���� - GameUI�� UI��ũ��Ʈ�� ����
    //    Transform gamecanvasTrans = gameUICanvas.transform;

    //    currentScoreTxt = gamecanvasTrans.Find("CurrentScoreText").GetComponent<TextMeshProUGUI>();
    //    bestScoreTxt = gamecanvasTrans.Find("BestScoreText").GetComponent<TextMeshProUGUI>();

    //    jumpButton = gamecanvasTrans.Find("JumpButton").GetComponent<Button>();
    //    slidingButton = gamecanvasTrans.Find("SlidingButton").GetComponent<Button>();

    //    jumpButton.onClick.AddListener(OnClickJumpButton);
    //    slidingButton.onClick.AddListener(OnClickSlidingButton);
    //}

    //public void SetUI(int currentscore, int bestscore)
    //{
    //    currentScoreTxt.text = currentscore.ToString();
    //    bestScoreTxt.text = bestscore.ToString();
    //}

    //private void OnClickJumpButton() // ���� ��ư Ŭ��
    //{
    //    Debug.Log("����");
    //}

    //private void OnClickSlidingButton() // �����̵� ��ư Ŭ��
    //{
    //    Debug.Log("�����̵�");
    //}

    //#endregion

    //#region ��� ���� UI

    //private void SelectModeUIInit()
    //{
    //    Transform selectCanvasTrans = selectmodeUICanvas.transform;

    //    stageButton = selectCanvasTrans.Find("StageButton").GetComponent<Button>();
    //    endlessButton = selectCanvasTrans.Find("EndlessButton").GetComponent<Button>();

    //    stageButton.onClick.AddListener(OnClickStageButton);
    //    endlessButton.onClick.AddListener(OnClickEndlessButton);
    //}

    //private void OnClickStageButton()
    //{
    //    Debug.Log("���������� �̵�");
    //}

    //private void OnClickEndlessButton()
    //{
    //    Debug.Log("���Ѹ��� �̵�");
    //}

    //#endregion
}
