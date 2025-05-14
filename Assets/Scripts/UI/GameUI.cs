using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;

// -?�싸곤옙?�쏙??

// 체占?�뱄???�占?�옙
// ?�쏙?�占?�옙 ?�占?�옙
// ?�싹?�옙?�쏙?�占?�옙 ?�占?�옙
// ?�쏙?�占?�옙?�싱?�옙, ?�쏙?�占?�옙 ?�占?�옙


public class GameUI : MonoBehaviour
{
    // ?�쏙?�크?�쏙?�트 ?�쏙?�占?�옙?�쏙?�트?�쏙???�쏙?�占?�옙?�쏙?�트 ?��??�옙
    [Header("캔占?�옙?�쏙???�쏙?�占?�옙?�쏙?�트")]
    [SerializeField] private GameObject _gameUICanvas;
    [SerializeField] private GameObject _gameStateUICanvas;

    [Header("?�쏙?�트 ?�싱뱄옙?�쏙??)]
    [SerializeField] private Sprite heart_full;
    [SerializeField] private Sprite heart_empty;
    [SerializeField] private List<Image> hearts;

    [Header("?�쏙?�占?�옙 ?�쏙?�占?�옙 ?�쏙?�占?�옙")]
    [SerializeField] private TextMeshProUGUI _gameStateText; // ?�쏙?�占?�옙 ?�쏙?�占?�옙 ?�쏙?�占?�옙
    [SerializeField] private string[] _gameStateMessages; // ?�쏙?�占?�옙 ?�쏙?�占?�옙 ?�쏙?�占?�옙 ?�썼??

    private CharacterController character;

    private TextMeshProUGUI currentScoreTxt; // ���� ���� 
    private TextMeshProUGUI bestScoreTxt; // �ְ� ����
    private TextMeshProUGUI stateCurrentScoreTxt; // ���� ���� ���� ���� 
    private TextMeshProUGUI stateBestScoreTxt; // ���� ���� �ְ� ����

    private Button jumpButton; // ?�쏙?�占?�옙 ?�쏙?�튼
    private Button restartButton; // ?�쏙?�占?�옙?? ?�쏙?�튼
    private Button backButton; // ?�쌘로곤?�占?�옙 ?�쏙?�튼
    private Button homeButton; // ?�占?�옙?�쏙???�쏙?�占?�옙 ?�쏙?�튼
    private Button pauseButton; // ?�싹?�옙?�쏙?�占?�옙 ?�쏙?�튼
    private Button slidingButton; // ?�쏙?�占?�옙?�싱?�옙 ?�쏙?�튼

    public GameObject[] charPrefabs;
    public GameObject player;

    public bool HpUp = false; // ȸ���ߴ��� ����

    private void Start()
    {
        player = Instantiate(charPrefabs[(int)Managers.data.currentCharacter]);
        Init();
    }

    private void Update()
    {
        UpdateHealthUI();
        UpdateScoreUI();
    }

    public void Init() // ?�십깍옙??
    {
        // ?�싸?�옙?�쏙?�占?�울???�쏙?�占?�옙?�쏙?�트 ?�쏙?�占?�옙
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
        LoadBestScore(); // �ҷ��� �ְ� ���� ǥ��

        Time.timeScale = 1.0f; // ?�쏙?�占?�쏙?�占?�옙
    }

    public void SetUI(int currentscore, int bestscore) // ������ �޾ƿ�
    {
        currentScoreTxt.text = currentscore.ToString();
        bestScoreTxt.text = bestscore.ToString();
        stateCurrentScoreTxt.text = currentscore.ToString();
        stateBestScoreTxt.text = bestscore.ToString();
    }

    public void SaveBestScore()
    {
        PlayerPrefs.SetInt("BestScore", character.BestScore);
        PlayerPrefs.Save(); // ���� ������ ��ũ�� ����
    }

    public void LoadBestScore() // �ְ� ���� ����
    {
        if (PlayerPrefs.HasKey("BestScore")) // �ش� Ű�� �����ϴ��� Ȯ��
        {
            character.BestScore = PlayerPrefs.GetInt("BestScore");
            bestScoreTxt.text = character.BestScore.ToString();
        }
        else
        {
            character.BestScore = 0; // �����? �ְ� ������ ������ 0���� �ʱ�ȭ
            bestScoreTxt.text = "0";
        }
    }
    
    private void ShowGameStateUI() // ���ӻ��� UI
    {
        if (character.CurrentHp <= 0) // ?�쏙?�占?�옙 ?�쏙?�占?�옙?�쏙???�쏙??- ?�쏙?�占?�옙 ?�쏙?�占?�옙
        {
            _gameStateText.text = _gameStateMessages[1]; // "?�쏙?�占?�옙 ?�쏙?�占?�옙" ?�쏙?�占?
            _gameStateUICanvas.SetActive(true);
            Time.timeScale = 0f;
        }
        //else if (stageClear) // �������� Ŭ����
        //{
        //_gameStateText.text = _gameStateMessages[0]; // "�������� Ŭ����" ���?
        //_gameStateUICanvas.SetActive(true);
        //}
    }

    public void UpdateHealthUI() // Hp UI ?�쏙?�占?�옙?�쏙?�트
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

    public void UpdateScoreUI()
    {
        SetUI(character.Score, character.BestScore);
    }

    public void CheckGameOver() // ?�쏙?�占?�옙 ?�쏙?�占?�옙?�쏙???�쏙?�占? ?�쏙?�占?�옙
    {
        StartCoroutine(DelayedGameOverUI());
    }

    IEnumerator DelayedGameOverUI()
    {
        yield return new WaitForSeconds(0.5f); // 0.5?�쏙???�쏙?�占? ?�쏙???�쌕?�옙 ?�쌜?�옙
        Debug.Log("0.5?�쏙???�식?�옙 UI?�쌜?�옙");
        ShowGameStateUI();
    }

    #region ?�쏙?�튼

    private void OnClickJumpButton() // ���� ��ư
    {
        if (character.jumpCount < character.maxJumpCount && !character.isSliding)
        {
            character.Jump();
        }
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

    private void OnClickRestartButton() // �����? ��ư
    {
        Time.timeScale = 1f; // ?�쏙?�占?�옙 ?�쏙?�占?�옙
        SceneManager.LoadScene("Game");
        Debug.Log("?�쏙?�占?�옙??");
    }

    private void OnClickNextButton() // ���� �������� ��ư
    {
        Debug.Log("?�쏙?�占?�옙 ?�쏙?�占?�옙?�쏙?�占?�옙");
    }

    private void OnClickHomeButton() // Ȩ ��ư
    {
        SceneManager.LoadScene("Main");
    }

    private void OnClickPauseButton() // �Ͻ����� ��ư
    {
        _gameStateText.text = _gameStateMessages[2]; // "?�싹?�옙?�쏙?�占?�옙" ?�쏙?�占?
        _gameStateUICanvas.SetActive(true);
        Time.timeScale = 0f; // ?�쏙?�占?�옙 ?�쏙?�占?�옙
    }

    #endregion
}