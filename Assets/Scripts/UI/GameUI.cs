using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;

// -? ì‹¸ê³¤ì˜™? ì™??

// ì²´å ?¹ë±„???œå ?™ì˜™
// ? ì™?™å ?™ì˜™ ?œå ?™ì˜™
// ? ì‹¹?™ì˜™? ì™?™å ?™ì˜™ ?œå ?™ì˜™
// ? ì™?™å ?™ì˜™? ì‹±?¸ì˜™, ? ì™?™å ?™ì˜™ ?œå ?™ì˜™


public class GameUI : MonoBehaviour
{
    // ? ì™?™í¬? ì™?™íŠ¸ ? ì™?™å ?™ì˜™? ì™?™íŠ¸? ì™??? ì™?™å ?™ì˜™? ì™?™íŠ¸ ? ì??ì˜™
    [Header("ìº”å ?™ì˜™? ì™??? ì™?™å ?™ì˜™? ì™?™íŠ¸")]
    [SerializeField] private GameObject _gameUICanvas;
    [SerializeField] private GameObject _gameStateUICanvas;

    [Header("? ì™?™íŠ¸ ? ì‹±ë±„ì˜™? ì™??)]
    [SerializeField] private Sprite heart_full;
    [SerializeField] private Sprite heart_empty;
    [SerializeField] private List<Image> hearts;

    [Header("? ì™?™å ?™ì˜™ ? ì™?™å ?™ì˜™ ? ì™?™å ?™ì˜™")]
    [SerializeField] private TextMeshProUGUI _gameStateText; // ? ì™?™å ?™ì˜™ ? ì™?™å ?™ì˜™ ? ì™?™å ?™ì˜™
    [SerializeField] private string[] _gameStateMessages; // ? ì™?™å ?™ì˜™ ? ì™?™å ?™ì˜™ ? ì™?™å ?™ì˜™ ? ì¼??

    private CharacterController character;

    private TextMeshProUGUI currentScoreTxt; // ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ 
    private TextMeshProUGUI bestScoreTxt; // ï¿½Ö°ï¿½ ï¿½ï¿½ï¿½ï¿½
    private TextMeshProUGUI stateCurrentScoreTxt; // ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ 
    private TextMeshProUGUI stateBestScoreTxt; // ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½Ö°ï¿½ ï¿½ï¿½ï¿½ï¿½

    private Button jumpButton; // ? ì™?™å ?™ì˜™ ? ì™?™íŠ¼
    private Button restartButton; // ? ì™?™å ?™ì˜™?? ? ì™?™íŠ¼
    private Button backButton; // ? ìŒ˜ë¡œê³¤?™å ?™ì˜™ ? ì™?™íŠ¼
    private Button homeButton; // ?ˆå ?™ì˜™? ì™??? ì™?™å ?™ì˜™ ? ì™?™íŠ¼
    private Button pauseButton; // ? ì‹¹?™ì˜™? ì™?™å ?™ì˜™ ? ì™?™íŠ¼
    private Button slidingButton; // ? ì™?™å ?™ì˜™? ì‹±?¸ì˜™ ? ì™?™íŠ¼

    public GameObject[] charPrefabs;
    public GameObject player;

    public bool HpUp = false; // È¸ï¿½ï¿½ï¿½ß´ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½

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

    public void Init() // ? ì‹­ê¹ì˜™??
    {
        // ? ì‹¸?™ì˜™? ì™?™å ?¶ìš¸??? ì™?™å ?™ì˜™? ì™?™íŠ¸ ? ì™?™å ?™ì˜™
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
        character.SetCharacterState(); // Ä³ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½Ê±ï¿½È­
        LoadBestScore(); // ï¿½Ò·ï¿½ï¿½ï¿½ ï¿½Ö°ï¿½ ï¿½ï¿½ï¿½ï¿½ Ç¥ï¿½ï¿½

        Time.timeScale = 1.0f; // ? ì™?™å ?ˆì™?™å ?™ì˜™
    }

    public void SetUI(int currentscore, int bestscore) // ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½Ş¾Æ¿ï¿½
    {
        currentScoreTxt.text = currentscore.ToString();
        bestScoreTxt.text = bestscore.ToString();
        stateCurrentScoreTxt.text = currentscore.ToString();
        stateBestScoreTxt.text = bestscore.ToString();
    }

    public void SaveBestScore()
    {
        PlayerPrefs.SetInt("BestScore", character.BestScore);
        PlayerPrefs.Save(); // ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½Å©ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½
    }

    public void LoadBestScore() // ï¿½Ö°ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½
    {
        if (PlayerPrefs.HasKey("BestScore")) // ï¿½Ø´ï¿½ Å°ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½Ï´ï¿½ï¿½ï¿½ È®ï¿½ï¿½
        {
            character.BestScore = PlayerPrefs.GetInt("BestScore");
            bestScoreTxt.text = character.BestScore.ToString();
        }
        else
        {
            character.BestScore = 0; // ï¿½ï¿½ï¿½ï¿½ï¿? ï¿½Ö°ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ 0ï¿½ï¿½ï¿½ï¿½ ï¿½Ê±ï¿½È­
            bestScoreTxt.text = "0";
        }
    }
    
    private void ShowGameStateUI() // ï¿½ï¿½ï¿½Ó»ï¿½ï¿½ï¿½ UI
    {
        if (character.CurrentHp <= 0) // ? ì™?™å ?™ì˜™ ? ì™?™å ?™ì˜™? ì™??? ì™??- ? ì™?™å ?™ì˜™ ? ì™?™å ?™ì˜™
        {
            _gameStateText.text = _gameStateMessages[1]; // "? ì™?™å ?™ì˜™ ? ì™?™å ?™ì˜™" ? ì™?™å ?
            _gameStateUICanvas.SetActive(true);
            Time.timeScale = 0f;
        }
        //else if (stageClear) // ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ Å¬ï¿½ï¿½ï¿½ï¿½
        //{
        //_gameStateText.text = _gameStateMessages[0]; // "ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ Å¬ï¿½ï¿½ï¿½ï¿½" ï¿½ï¿½ï¿?
        //_gameStateUICanvas.SetActive(true);
        //}
    }

    public void UpdateHealthUI() // Hp UI ? ì™?™å ?™ì˜™? ì™?™íŠ¸
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

    public void CheckGameOver() // ? ì™?™å ?™ì˜™ ? ì™?™å ?™ì˜™? ì™??? ì™?™å ? ? ì™?™å ?™ì˜™
    {
        StartCoroutine(DelayedGameOverUI());
    }

    IEnumerator DelayedGameOverUI()
    {
        yield return new WaitForSeconds(0.5f); // 0.5? ì™??? ì™?™å ? ? ì™??? ìŒ•?™ì˜™ ? ìŒœ?¸ì˜™
        Debug.Log("0.5? ì™??? ì‹?¸ì˜™ UI? ìŒœ?¸ì˜™");
        ShowGameStateUI();
    }

    #region ? ì™?™íŠ¼

    private void OnClickJumpButton() // ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½Æ°
    {
        if (character.jumpCount < character.maxJumpCount && !character.isSliding)
        {
            character.Jump();
        }
    }

    private void OnClickSlidingButton() // ï¿½ï¿½ï¿½ï¿½ï¿½Ìµï¿½ ï¿½ï¿½Æ°
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

    private void OnClickRestartButton() // ï¿½ï¿½ï¿½ï¿½ï¿? ï¿½ï¿½Æ°
    {
        Time.timeScale = 1f; // ? ì™?™å ?™ì˜™ ? ì™?™å ?™ì˜™
        SceneManager.LoadScene("Game");
        Debug.Log("? ì™?™å ?™ì˜™??");
    }

    private void OnClickNextButton() // ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½Æ°
    {
        Debug.Log("? ì™?™å ?™ì˜™ ? ì™?™å ?™ì˜™? ì™?™å ?™ì˜™");
    }

    private void OnClickHomeButton() // È¨ ï¿½ï¿½Æ°
    {
        SceneManager.LoadScene("Main");
    }

    private void OnClickPauseButton() // ï¿½Ï½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½Æ°
    {
        _gameStateText.text = _gameStateMessages[2]; // "? ì‹¹?™ì˜™? ì™?™å ?™ì˜™" ? ì™?™å ?
        _gameStateUICanvas.SetActive(true);
        Time.timeScale = 0f; // ? ì™?™å ?™ì˜™ ? ì™?™å ?™ì˜™
    }

    #endregion
}