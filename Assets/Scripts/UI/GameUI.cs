using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

// -�ΰ���-

// ü�¹� ǥ��
// ���� ǥ��
// �Ͻ����� ǥ��
// �����̵�, ���� ǥ��


public class GameUI : MonoBehaviour
{
    // ��ũ��Ʈ ������Ʈ�� ������Ʈ �Ҵ�
    public GameObject _gameUICanvas;
    public GameObject _gameStateUICanvas;
    private CharacterController _character;
    public TextMeshProUGUI _gameStateText; // ���� ���� ����
    public string[] _gameStateMessages; // ���� ���� ���� �迭

    TextMeshProUGUI currentScoreTxt; // ���� ���� 
    TextMeshProUGUI bestScoreTxt; // �ְ� ����
    Button jumpButton; // ���� ��ư
    Button restartButton; // ����� ��ư
    Button backButton; // �ڷΰ��� ��ư
    Button homeButton; // Ȩ���� ���� ��ư
    Button pauseButton; // �Ͻ����� ��ư
    Button slidingButton; // �����̵� ��ư

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        ShowGameStateUI();
    }

    public void Init() // �ʱ�ȭ
    {
        if (_gameUICanvas == null || _gameStateUICanvas == null)
        {
            Debug.LogError("UI Canvas�� ����� �ε���� �ʾҽ��ϴ�.");
            return;
        }

        // �ν����Ϳ� ������Ʈ ����
        Transform gameCanvas = _gameUICanvas.transform;
        Transform gameStateCanvas = _gameStateUICanvas.transform;

        currentScoreTxt = gameCanvas.Find("CurrentScoreText").GetComponent<TextMeshProUGUI>();
        bestScoreTxt = gameCanvas.Find("BestScoreText").GetComponent<TextMeshProUGUI>();

        jumpButton = gameCanvas.Find("Button - Jump").GetComponent<Button>();
        slidingButton = gameCanvas.Find("Button - Sliding").GetComponent<Button>();
        restartButton = gameStateCanvas.Find("Button - Restart").GetComponent<Button>();
        homeButton = gameStateCanvas.Find("Button - Home").GetComponent<Button>();
        backButton = gameStateCanvas.Find("Button - Next").GetComponent<Button>();
        pauseButton = gameCanvas.Find("Button - Pause").GetComponent<Button>();

        jumpButton.onClick.AddListener(OnClickJumpButton);
        slidingButton.onClick.AddListener(OnClickSlidingButton);
        restartButton.onClick.AddListener(OnClickRestartButton);
        backButton.onClick.AddListener(OnClickNextButton);
        homeButton.onClick.AddListener(OnClickHomeButton);
        pauseButton.onClick.AddListener(OnClickPauseButton);

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        _character = playerObject.GetComponent<CharacterController>();
        _gameUICanvas.SetActive(true);

        Time.timeScale = 1.0f; // ���ӽ���
    }

    public void SetUI(int currentscore, int bestscore) // ������ �޾ƿ�
    {
        currentScoreTxt.text = currentscore.ToString();
        bestScoreTxt.text = bestscore.ToString();
    }

    private void ShowGameStateUI() // ���ӻ��� UI
    {
        //if (Die) // ���� ������ �� - ���� ����
        //{
        //_gameStateText.text = _gameStateMessages[1]; // "���� ����" ���
        //_gameStateUICanvas.SetActive(true);
        //}
        //else if (stageClear) // �������� Ŭ����
        //{
        //_gameStateText.text = _gameStateMessages[0]; // "�������� Ŭ����" ���
        //_gameStateUICanvas.SetActive(true);
        //}
    }

    #region ��ư

    private void OnClickJumpButton() // ���� ��ư
    {
        _character.Jump();
    }

    private void OnClickSlidingButton() // �����̵� ��ư
    {
        if (_character.isSliding == false)
        {
            _character.Slide();
            _character.isSliding = true;
        }
        else
        {
            _character.EndSlide();
            _character.isSliding = false;
        }
    }

    private void OnClickRestartButton() // ����� ��ư
    {
        Time.timeScale = 1f; // ���� ����
        SceneManager.LoadScene("Game");
        Debug.Log("�����");
    }

    private void OnClickNextButton() // ���� �������� ��ư
    {
        Debug.Log("���� ��������");
    }

    private void OnClickHomeButton() // Ȩ ��ư
    {
        SceneManager.LoadScene("Main");
    }

    private void OnClickPauseButton() // �Ͻ����� ��ư
    {
        Time.timeScale = 0f; // ���� ����
        _gameStateText.text = _gameStateMessages[2]; // "�Ͻ�����" ���
        _gameStateUICanvas.SetActive(true);
    }

    #endregion
}