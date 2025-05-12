using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Xml.Serialization;
using JetBrains.Annotations; // UnityAction을 사용하기 위해 추가

// -인게임-

// 체력바 표시
// 점수 표시
// 일시정지 표시
// 슬라이딩, 점프 표시


public class GameUI : MonoBehaviour
{
    // 스크립트 컴포넌트에 오브젝트 할당
    public GameObject _gameUICanvas;
    public GameObject _gameStateUICanvas;
    public TextMeshProUGUI _gameStateText; // 게임 상태 문구
    public string[] _gameStateMessages; // 게임 상태 문구 배열

    TextMeshProUGUI currentScoreTxt; // 현재 점수 
    TextMeshProUGUI bestScoreTxt; // 최고 점수
    Button jumpButton; // 점프 버튼
    Button slidingButton; // 슬라이딩 버튼
    Button restartButton; // 재시작 버튼
    Button backButton; // 뒤로가기 버튼
    Button homeButton; // 홈으로 가기 버튼
    Button pauseButton; // 일시정지 버튼

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        ShowGameStateUI();
    }

    public void Init() // 초기화
    {
        // 인스펙터에 오브젝트 연결
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

        _gameUICanvas.SetActive(true);
    }

    public void SetUI(int currentscore, int bestscore) // 점수를 받아옴
    {
        currentScoreTxt.text = currentscore.ToString();
        bestScoreTxt.text = bestscore.ToString();
    }

    private void ShowGameStateUI() // 게임상태 UI
    {
        //if (Die) // 죽은 상태일 때 - 게임 오버
        //{
        //_gameStateText.text = _gameStateMessages[1]; // "게임 오버" 출력
        //_gameStateUICanvas.SetActive(true);
        //}
        //else if (stageClear) // 스테이지 클리어
        //{
        //_gameStateText.text = _gameStateMessages[0]; // "스테이지 클리어" 출력
        //_gameStateUICanvas.SetActive(true);
        //}
    }

    #region 버튼

    private void OnClickJumpButton() // 점프 버튼
    {
        Debug.Log("점프");
    }

    private void OnClickSlidingButton() // 슬라이딩 버튼
    {
        Debug.Log("슬라이딩");
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
        Debug.Log("홈으로 이동");
    }

    private void OnClickPauseButton() // 일시정지 버튼
    {
        Time.timeScale = 0f; // 게임 정지
        _gameStateText.text = _gameStateMessages[2]; // "일시정지" 출력
        _gameStateUICanvas.SetActive(true);
        Debug.Log("일시정지");
    }

    #endregion
}