using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;

// -�ΰ���-

// ü�¹� ǥ��
// ���� ǥ��
// �Ͻ����� ǥ��
// �����̵�, ���� ǥ��


public class GameUI : MonoBehaviour
{
    // ��ũ��Ʈ ������Ʈ�� ������Ʈ �Ҵ�
    [Header("ĵ���� ������Ʈ")]
    [SerializeField] private GameObject _gameUICanvas;
    [SerializeField] private GameObject _gameStateUICanvas;

    [Header("��Ʈ �̹���")]
    [SerializeField] private Sprite heart_full;
    [SerializeField] private Sprite heart_empty;
    [SerializeField] private List<Image> hearts;

    [Header("���� ���� ����")]
    [SerializeField] private TextMeshProUGUI _gameStateText; // ���� ���� ����
    [SerializeField] private string[] _gameStateMessages; // ���� ���� ���� �迭

    private CharacterBaseController character;

    TextMeshProUGUI currentScoreTxt; // ���� ���� 
    TextMeshProUGUI bestScoreTxt; // �ְ� ����
    Button jumpButton; // ���� ��ư
    Button restartButton; // ����� ��ư
    Button backButton; // �ڷΰ��� ��ư
    Button homeButton; // Ȩ���� ���� ��ư
    Button pauseButton; // �Ͻ����� ��ư
    Button slidingButton; // �����̵� ��ư

    public int Hp = 3; // ���Ƿ� ������ ü��
    public bool HpUp = false;
    public bool HpDown = false;

    private void Start()
    {
        Init();
        Debug.Log(character.CurrentHp);
        Debug.Log(character.maxHp);
    }

    private void Update()
    {
        ShowGameStateUI();
        UpdateHealthUI();

    }

    public void Init() // �ʱ�ȭ
    {
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

        character = playerObject.GetComponent<CharacterController>();
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
        if (Hp <= 0) // ���� ������ �� - ���� ����
        {
            _gameStateText.text = _gameStateMessages[1]; // "���� ����" ���
            _gameStateUICanvas.SetActive(true);
            Time.timeScale = 0f;
        }
        //else if (stageClear) // �������� Ŭ����
        //{
        //_gameStateText.text = _gameStateMessages[0]; // "�������� Ŭ����" ���
        //_gameStateUICanvas.SetActive(true);
        //}
    }

    private void UpdateHealthUI() // Hp UI ������Ʈ
    {
        // �׽�Ʈ�� ���߿� �������� �� ���� ü�� �κ� ����
        if (HpUp)
        {
            if (Hp < character.maxHp)
            {
                hearts[Hp].sprite = heart_full;
                Hp += 1;
            }
            HpUp = false;
        }
        else if (HpDown)
        {
            hearts[Hp - 1].sprite = heart_empty;
            Hp -= 1;
        }
        HpDown = false;
    }

    #region ��ư

    private void OnClickJumpButton() // ���� ��ư
    {
        character.Jump();
    }

    private void OnClickSlidingButton() // �����̵� ��ư
    {
        if (character.isSliding == false)
        {
            character.Slide();
            character.isSliding = true;
        }
        else
        {
            character.EndSlide();
            character.isSliding = false;
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