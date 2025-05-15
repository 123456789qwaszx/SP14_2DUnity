using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;
using UnityEditor;

public class GameUI : MonoBehaviour
{
    [Header("캔버스")]
    [SerializeField] private GameObject _gameUICanvas;
    [SerializeField] private GameObject _gameStateUICanvas;

    [Header("점수 문구 연결")]
    [SerializeField] private TextMeshProUGUI currentScoreTxt; // 인게임 UI 점수
    [SerializeField] private TextMeshProUGUI bestScoreTxt; // 인게임 UI 최고 점수
    [SerializeField] private TextMeshProUGUI stateCurrentScoreTxt; // 게임 종료 UI에서 사용하는 점수
    [SerializeField] private TextMeshProUGUI stateBestScoreTxt; // 게임 종료 UI에서 사용하는 최고 점수

    [Header("HP 이미지")]
    [SerializeField] private Sprite heart_full;
    [SerializeField] private Sprite heart_empty;
    [SerializeField] private List<Image> hearts;

    [Header("게임 상태 UI 문구")]
    [SerializeField] private TextMeshProUGUI _gameStateText; // 게임 상태 문구
    [SerializeField] private string[] _gameStateMessages; // 게임 상태 문구 배열

    private CharacterController character;
    public SelectModeUI selectModeUI;

    public GameObject[] charPrefabs;
    public GameObject player;

    public bool HpUp = false; // 체력을 회복한 상태인지 확인
    public bool isClear = false; // 클리어 했는지 
    public static bool isEndless = false;

    private void Start()
    {
        player = Instantiate(charPrefabs[(int)Managers.data.currentCharacter]);
        Init();
    }

    private void Update()
    {
        UpdateHealthUI();
        UpdateScoreUI();
        ShowGameStateUI();
    }

    public void Init() // 초기화
    {
        GameObject playerObject = player;

        character = playerObject.GetComponent<CharacterController>();
        _gameUICanvas.SetActive(true);
        character.SetCharacterState(); // 캐릭터 상태 초기화
        LoadBestScore(); // 최고 점수 불러오기

        Time.timeScale = 1.0f; // 게임 시작
    }

    public void SetUI(int currentscore, int bestscore) // 받아 온 점수를 UI에 연결
    {
        currentScoreTxt.text = currentscore.ToString();
        bestScoreTxt.text = bestscore.ToString();
        stateCurrentScoreTxt.text = currentscore.ToString();
        stateBestScoreTxt.text = bestscore.ToString();
    }

    public void SaveBestScore() // 최고 점수 저장
    {
        PlayerPrefs.SetInt("BestScore", character.BestScore);
        PlayerPrefs.Save();
    }

    public void LoadBestScore() // 게임 시작 시 최고 점수 불러오기
    {
        if (PlayerPrefs.HasKey("BestScore"))
        {
            character.BestScore = PlayerPrefs.GetInt("BestScore");
            bestScoreTxt.text = character.BestScore.ToString();
        }
        else
        {
            character.BestScore = 0; // 최고 점수가 없을 경우 초기화
            bestScoreTxt.text = "0";
        }
    }

    private void ShowGameStateUI() // 게임 상태 UI 보여주기
    {
        if (character.CurrentHp <= 0) // 현재 체력이 0일 경우
        {
            _gameStateText.text = _gameStateMessages[1]; // GameOver 출력
            _gameStateUICanvas.SetActive(true);
            Time.timeScale = 0f;
        }

        // 무한 모드이면 클리어 문구 출력 X
        if((!isEndless))
        {
            if (character.Score >= 5000 && !isClear) // 스코어 5000점 이상이면 클리어 UI출력
            {
                _gameStateText.text = _gameStateMessages[0]; // 클리어 문구 출력
                _gameStateUICanvas.SetActive(true);
                Time.timeScale = 0f;
                isClear = true;
            }
        }
    }

    public void UpdateHealthUI() // Hp UI 업데이트
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

    public void CheckGameOver() // 게임 오버 시 
    {
        StartCoroutine(DelayedGameOverUI());
    }

    IEnumerator DelayedGameOverUI()
    {
        yield return new WaitForSeconds(0.5f); // 0.5초 후 게임 종료 및 UI 출력
        Debug.Log("0.5초 후 UI 출력");
        ShowGameStateUI();
    }

    #region ��ư

    public void OnClickJumpButton() // 점프 버튼
    {
        if (character.jumpCount < character.maxJumpCount && !character.isSliding)
        {
            character.Jump();
        }
    }

    public void OnClickSlidingButton() // 슬라이딩 버튼
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

    public void OnClickRestartButton() // 재시작 버튼
    {
        Time.timeScale = 1f; // 게임 시작
        SceneManager.LoadScene("Game");
        Debug.Log("재시작");
    }

    public void OnClickNextButton() // 이어하기 버튼
    {
        if ((int)character.CurrentHp == 0) // 게임 오버면 이어하기가 아닌 뒤로가기로 변경
        {
            SceneManager.LoadScene("SelecScene");
        }
        else if (character.Score >= 5000 || (int)character.CurrentHp > 0)
        {
            _gameStateUICanvas.SetActive(false);
            Time.timeScale = 1f; // 게임 이어서 시작
            Debug.Log("이어하기");
        }
    }

    public void OnClickHomeButton() // 메인으로 이동
    {
        SceneManager.LoadScene("Main");
    }

    public void OnClickPauseButton() // 일시 정지 버튼
    {
        _gameStateText.text = _gameStateMessages[2]; // 일시 정지 문구 출력
        _gameStateUICanvas.SetActive(true);
        Time.timeScale = 0f; // 게임 정지
    }

    #endregion
}