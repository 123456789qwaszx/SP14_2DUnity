using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using UnityEngine.Events; // UnityAction�� ����ϱ� ���� �߰�

// -�ΰ���-

// ü�¹� ǥ��
// ���� ǥ��
// �Ͻ����� ǥ��
// �����̵�, ���� ǥ��


public class GameUI : MonoBehaviour
{
    public GameObject gameUICanvas;
    public GameObject gameoverUICanvas;

    TextMeshProUGUI currentScoreTxt; // ���� ���� 
    TextMeshProUGUI bestScoreTxt; // �ְ� ����
    Button jumpButton; // ���� ��ư
    Button slidingButton; // �����̵� ��ư
    Button restartButton; // ����� ��ư
    Button backButton; // �ڷΰ��� ��ư
    Button homeButton; // Ȩ���� ���� ��ư

    private void Start()
    {
        Init();
    }

    public void Init() // �ʱ�ȭ
    {
        // �ν����Ϳ� ������Ʈ ����
        Transform gameCanvas = gameUICanvas.transform;
        Transform gameoverCanvas = gameoverUICanvas.transform;

        currentScoreTxt = gameCanvas.Find("CurrentScoreText").GetComponent<TextMeshProUGUI>();
        bestScoreTxt = gameCanvas.Find("BestScoreText").GetComponent<TextMeshProUGUI>();

        jumpButton = gameCanvas.Find("JumpButton").GetComponent<Button>();
        slidingButton = gameCanvas.Find("SlidingButton").GetComponent<Button>();
        restartButton = gameoverCanvas.Find("RestartButton").GetComponent <Button>();
        homeButton = gameoverCanvas.Find("HomeButton").GetComponent<Button>();
        backButton = gameoverCanvas.Find("BackButton").GetComponent<Button>();

        jumpButton.onClick.AddListener(OnClickJumpButton);
        slidingButton.onClick.AddListener(OnClickSlidingButton);
        restartButton.onClick.AddListener(OnClickRestartButton);
        backButton.onClick.AddListener(OnClickBackButton);
        homeButton.onClick.AddListener(OnClickHomeButton);
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

    private void OnClickRestartButton() // ����� ��ư Ŭ��
    {
        SceneManager.LoadScene("Game");
        Debug.Log("�����");
    }

    private void OnClickBackButton() // �ڷΰ��� ��ư Ŭ��
    {
        SceneManager.LoadScene("StageSelect");
        Debug.Log("�ڷΰ���");
    }

    private void OnClickHomeButton() // Ȩ ��ư Ŭ��
    {
        SceneManager.LoadScene("Main");
        Debug.Log("Ȩ���� �̵�");
    }
}