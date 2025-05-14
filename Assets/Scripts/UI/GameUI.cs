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

    private CharacterController character;

    private TextMeshProUGUI currentScoreTxt; // ���� ���� 
    private TextMeshProUGUI bestScoreTxt; // �ְ� ����
    private TextMeshProUGUI stateCurrentScoreTxt; // ���� ���� 
    private TextMeshProUGUI stateBestScoreTxt; // �ְ� ����

    private Button jumpButton; // ���� ��ư
    private Button restartButton; // ����� ��ư
    private Button backButton; // �ڷΰ��� ��ư
    private Button homeButton; // Ȩ���� ���� ��ư
    private Button pauseButton; // �Ͻ����� ��ư
    private Button slidingButton; // �����̵� ��ư

    public GameObject[] charPrefabs;
    public GameObject player;

    public bool HpUp = false;

    private void Start()
    {
        player = Instantiate(charPrefabs[(int)Managers.data.currentCharacter]);
        Init();
    }

    private void Update()
    {
        UpdateHealthUI();
    }

    public void Init() // �ʱ�ȭ
    {
        // �ν����Ϳ� ������Ʈ ����
        Transform gameCanvas = _gameUICanvas.transform;
        Transform gameStateCanvas = _gameStateUICanvas.transform;

        currentScoreTxt = gameCanvas.Find("CurrentScoreText").GetComponent<TextMeshProUGUI>();
        bestScoreTxt = gameCanvas.Find("BestScoreText").GetComponent<TextMeshProUGUI>();
        stateCurrentScoreTxt = gameStateCanvas.Find("CurrentScoreText").GetComponent<TextMeshProUGUI>();
        stateBestScoreTxt = gameStateCanvas.Find("BestScoreText").GetComponent<TextMeshProUGUI>();

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

        GameObject playerObject = player;

        character = playerObject.GetComponent<CharacterController>();
        _gameUICanvas.SetActive(true);
        character.SetCharacterState(); // ĳ���� ���� �ʱ�ȭ

        Time.timeScale = 1.0f; // ���ӽ���
    }

    public void SetUI(int currentscore, int bestscore) // 점수를 받아옴
    {
        currentScoreTxt.text = currentscore.ToString();
        bestScoreTxt.text = bestscore.ToString();
        stateCurrentScoreTxt.text = currentscore.ToString();
        stateBestScoreTxt.text = bestscore.ToString();
    }

    public void SaveBestScore()
    {
        PlayerPrefs.SetInt("BestScore", character.BestScore);
        PlayerPrefs.Save(); // 변경 사항을 디스크에 저장
    }

    public void LoadBestScore() // 최고 점수 저장
    {
        if (PlayerPrefs.HasKey("BestScore")) // 해당 키가 존재하는지 확인
        {
            character.BestScore = PlayerPrefs.GetInt("BestScore");
            bestScoreTxt.text = character.BestScore.ToString();
        }
        else
        {
            character.BestScore = 0; // 저장된 최고 점수가 없으면 0으로 초기화
            bestScoreTxt.text = "0";
        }
    }

    public void ShowGameStateUI() // ���ӻ��� UI
    {
        if (character.CurrentHp <= 0) // ���� ������ �� - ���� ����
        {
            _gameStateText.text = _gameStateMessages[1]; // "���� ����" ���
            _gameStateUICanvas.SetActive(true);
            Time.timeScale = 0f;
        }
        else if (character.Score >= 5000) // �������� Ŭ����
        {
            _gameStateText.text = _gameStateMessages[0]; // "�������� Ŭ����" ���
            _gameStateUICanvas.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void UpdateHealthUI() // Hp UI ������Ʈ
    {
        if (HpUp)
        {
            if (character.CurrentHp > 0 && character.CurrentHp <= character.maxHp)
            {
                if ((int)character.CurrentHp == 2)
                {
                    hearts[(int)character.CurrentHp - 1].sprite = heart_full;
                }
                else if ((int)character.CurrentHp == 3)
                {
                    hearts[(int)character.CurrentHp - 1].sprite = heart_full;
                }
            }

            HpUp = false;
        }
        else if (character.isInvincible)
        {
            if (character.CurrentHp >= 0)
            {
                if ((int)character.CurrentHp == 2)
                {
                    hearts[(int)character.CurrentHp].sprite = heart_empty;
                }
                else if ((int)character.CurrentHp == 1)
                {
                    hearts[(int)character.CurrentHp].sprite = heart_empty;
                }
                else if ((int)character.CurrentHp == 0)
                {
                    hearts[(int)character.CurrentHp].sprite = heart_empty;
                }
            }
        }
    }

    public void CheckGameOver() // ���� ������ ��� ����
    {
        StartCoroutine(DelayedGameOverUI());
    }

    IEnumerator DelayedGameOverUI()
    {
        yield return new WaitForSeconds(0.5f); // 0.5�� ��� �� �ٽ� �۵�
        Debug.Log("0.5�� �Ŀ� UI�۵�");
        ShowGameStateUI();
    }

    #region ��ư

    public void OnClickJumpButton() // ���� ��ư
    {
        if (character.jumpCount < character.maxJumpCount && !character.isSliding)
        {
            character.Jump();
        }
    }

    public void OnClickSlidingButton() // �����̵� ��ư
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

    public void OnClickRestartButton() // ����� ��ư
    {
        Time.timeScale = 1f; // ���� ����
        SceneManager.LoadScene("Game");
        Debug.Log("�����");
    }

    public void OnClickNextButton() // ���� �������� ��ư
    {
        Debug.Log("���� ��������");
    }

    public void OnClickHomeButton() // Ȩ ��ư
    {
        SceneManager.LoadScene("Main");
    }

    public void OnClickPauseButton() // �Ͻ����� ��ư
    {
        _gameStateText.text = _gameStateMessages[2]; // "�Ͻ�����" ���
        _gameStateUICanvas.SetActive(true);
        Time.timeScale = 0f; // ���� ����
    }

    #endregion
}