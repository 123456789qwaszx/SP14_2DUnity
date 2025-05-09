using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// -�ΰ���-

// ü�¹� ǥ��
// ���� ǥ��
// �Ͻ����� ǥ��
// �����̵�, ���� ǥ��


public class GameUI : MonoBehaviour
{
    public GameObject gameUICanvas;
    TextMeshProUGUI currentScoreTxt; // ���� ���� 
    TextMeshProUGUI bestScoreTxt; // �ְ� ����
    Button jumpButton; // ���� ��ư
    Button slidingButton; // �����̵� ��ư
    //SpriteRenderer hpBar; // ü��
    //Button stopButton; // �Ͻ� ����

    private void Start()
    {
        Init();
    }

    public void Init() // �ʱ�ȭ
    {
        if (gameUICanvas == null) return;

        // ������Ʈ�� ���� - GameUI�� UI��ũ��Ʈ�� ����
        Transform gamecanvasTrans = gameUICanvas.transform;

        currentScoreTxt = gamecanvasTrans.Find("CurrentScoreText").GetComponent<TextMeshProUGUI>();
        bestScoreTxt = gamecanvasTrans.Find("BestScoreText").GetComponent<TextMeshProUGUI>();

        jumpButton = gamecanvasTrans.Find("JumpButton").GetComponent<Button>();
        slidingButton = gamecanvasTrans.Find("SlidingButton").GetComponent<Button>();

        jumpButton.onClick.AddListener(OnClickJumpButton);
        slidingButton.onClick.AddListener(OnClickSlidingButton);
    }

    public void SetUI(int currentscore, int bestscore)
    {
        currentScoreTxt.text = currentscore.ToString();
        bestScoreTxt.text = bestscore.ToString();
    }

    private void OnClickJumpButton() // ���� ��ư Ŭ��
    {
        Debug.Log("����");
    }

    private void OnClickSlidingButton() // �����̵� ��ư Ŭ��
    {
        Debug.Log("�����̵�");
    }
}
