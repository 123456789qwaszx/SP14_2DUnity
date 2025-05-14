using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;

// -인게임-

// 체력바 표시
// 점수 표시
// 일시정지 표시
// 슬라이딩, 점프 표시


public class GameUI : MonoBehaviour
{
    // 스크립트 컴포넌트에 오브젝트 할당
    [Header("캔버스 오브젝트")]
    [SerializeField] private GameObject _gameUICanvas;
    [SerializeField] private GameObject _gameStateUICanvas;

    [Header("하트 이미지")]
    [SerializeField] private Sprite heart_full;
    [SerializeField] private Sprite heart_empty;
    [SerializeField] private List<Image> hearts;

    [Header("게임 상태 문구")]
    [SerializeField] private TextMeshProUGUI _gameStateText; // 게임 상태 문구
    [SerializeField] private string[] _gameStateMessages; // 게임 상태 문구 배열

    private CharacterController character;
    private ObstacleBaseController obstacle;
    private Items items;

    private TextMeshProUGUI currentScoreTxt; // 현재 점수 
    private TextMeshProUGUI bestScoreTxt; // 최고 점수
    private Button jumpButton; // 점프 버튼
    private Button restartButton; // 재시작 버튼
    private Button backButton; // 뒤로가기 버튼
    private Button homeButton; // 홈으로 가기 버튼
    private Button pauseButton; // 일시정지 버튼
    private Button slidingButton; // 슬라이딩 버튼

    bool HpUp = false;

    private void Start()
    {
        Init();
        Debug.Log(character.currentHp);
        Debug.Log(character.maxHp);
    }

    private void Update()
    {
        ShowGameStateUI();
        UpdateHealthUI();
    }

    public void Init() // 초기화
    {
        // 인스펙터에 오브젝트 연결
        Transform gameCanvas = _gameUICanvas.transform;
        Transform gameStateCanvas = _gameStateUICanvas.transform;

        //Transform currentScoreTransform = gameCanvas.Find("CurrentScoreText");

        //currentScoreTxt = currentScoreTransform.GetComponent<TextMeshProUGUI>();
        currentScoreTxt = gameCanvas.Find("CurrentScoreText").GetComponent<TextMeshProUGUI>();
        //bestScoreTxt = gameCanvas.Find("BestScoreText").GetComponent<TextMeshProUGUI>();

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
        character.SetCharacterState(); // 캐릭터 상태 초기화

        Time.timeScale = 1.0f; // 게임시작
    }

    public void SetUI(int currentscore) //, int bestscore) // 점수를 받아옴
    {
        currentScoreTxt.text = currentscore.ToString();
        //bestScoreTxt.text = bestscore.ToString();
    }

    private void ShowGameStateUI() // 게임상태 UI
    {
        if (character.currentHp <= 0) // 죽은 상태일 때 - 게임 오버
        {
            _gameStateText.text = _gameStateMessages[1]; // "게임 오버" 출력
            _gameStateUICanvas.SetActive(true);
            Time.timeScale = 0f;
        }
        //else if (stageClear) // 스테이지 클리어
        //{
        //_gameStateText.text = _gameStateMessages[0]; // "스테이지 클리어" 출력
        //_gameStateUICanvas.SetActive(true);
        //}
    }

    private void UpdateHealthUI() // Hp UI 업데이트
    {
        // 테스트라 나중에 합쳐졌을 때 현재 체력 부분 수정
        if (HpUp)
        {
            if (character.currentHp < character.maxHp)
            {
                items.HpRecovery(character);
                hearts[(int)character.currentHp].sprite = heart_full;
            }

            HpUp = false;
        }
        else if (character.isInvincible)
        {
            character.Damage(obstacle.damage);
            Debug.Log($"체력 감소 {character.currentHp}");
            hearts[(int)character.currentHp - 1].sprite = heart_empty;
        }

        character.isInvincible = false;
    }

    #region 버튼

    private void OnClickJumpButton() // 점프 버튼
    {
        if (character.jumpCount < character.maxJumpCount && !character.isSliding)
        {
            character.Jump();
        }
    }

    private void OnClickSlidingButton() // 슬라이딩 버튼
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

    private void OnClickRestartButton() // 재시작 버튼
    {
        Time.timeScale = 1f; // 게임 시작
        SceneManager.LoadScene("Game");
        Debug.Log("재시작");
    }

    private void OnClickNextButton() // 다음 스테이지 버튼
    {
        Debug.Log("다음 스테이지");
    }

    private void OnClickHomeButton() // 홈 버튼
    {
        SceneManager.LoadScene("Main");
    }

    private void OnClickPauseButton() // 일시정지 버튼
    {
        Time.timeScale = 0f; // 게임 정지
        _gameStateText.text = _gameStateMessages[2]; // "일시정지" 출력
        _gameStateUICanvas.SetActive(true);
    }

    #endregion
}