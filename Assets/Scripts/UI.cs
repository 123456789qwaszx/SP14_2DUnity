using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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



    // �ΰ���UI
    #region 

    TextMeshProUGUI currentScoreText; // ���� ���� 
    TextMeshProUGUI bestScoreText; // �ְ� ����
    SpriteRenderer hpBar; // ü��
    //Button stopButton; // �Ͻ� ����
    Button jumpButton; // ���� ��ư
    Button slidingButton; // �����̵� ��ư

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        // ������Ʈ�� ���� - GameUI�� UI��ũ��Ʈ�� ����
        Transform canvasTransform = transform.Find("Canvas");

        currentScoreText = canvasTransform.transform.Find("CurrentScoreText").GetComponent<TextMeshProUGUI>();
        bestScoreText = canvasTransform.transform.Find("BestScoreText").GetComponent<TextMeshProUGUI>();

        //hpBar = transform.Find("HpBar").GetComponent<SpriteRenderer>();

        //stopButton = transform.Find("StopButton").GetComponent<Button>();
        jumpButton = canvasTransform.transform.Find("JumpButton").GetComponent<Button>();
        slidingButton = canvasTransform.transform.Find("SlidingButton").GetComponent<Button>();

        jumpButton.onClick.AddListener(OnClickJumpButton);
        slidingButton.onClick.AddListener(OnClickSlidingButton);
    }

    public void SetUI(int currentscore, int bestscore)
    {
        currentScoreText.text = currentscore.ToString();
        bestScoreText.text = bestscore.ToString();
    }

    private void OnClickJumpButton()
    {
        Debug.Log("����");
    }

    private void OnClickSlidingButton()
    {
        Debug.Log("�����̵�");
    }
    #endregion
}
