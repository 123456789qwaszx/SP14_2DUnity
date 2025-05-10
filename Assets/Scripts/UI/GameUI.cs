using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using UnityEngine.Events; // UnityAction을 사용하기 위해 추가

// -인게임-

// 체력바 표시
// 점수 표시
// 일시정지 표시
// 슬라이딩, 점프 표시


public class GameUI : MonoBehaviour
{
    public GameObject gameUICanvas;
    public GameObject gameoverUICanvas;

    TextMeshProUGUI currentScoreTxt; // 현재 점수 
    TextMeshProUGUI bestScoreTxt; // 최고 점수
    Button jumpButton; // 점프 버튼
    Button slidingButton; // 슬라이딩 버튼
    Button restartButton; // 재시작 버튼
    Button backButton; // 뒤로가기 버튼
    Button homeButton; // 홈으로 가기 버튼

    private void Start()
    {
        Init();
    }

    public void Init() // 초기화
    {
        // 인스펙터에 오브젝트 연결
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

    private void OnClickJumpButton() // 점프 버튼 클릭
    {
            Debug.Log("점프");
    }

    private void OnClickSlidingButton() // 슬라이딩 버튼 클릭
    {
        Debug.Log("슬라이딩");
    }

    private void OnClickRestartButton() // 재시작 버튼 클릭
    {
        SceneManager.LoadScene("Game");
        Debug.Log("재시작");
    }

    private void OnClickBackButton() // 뒤로가기 버튼 클릭
    {
        SceneManager.LoadScene("StageSelect");
        Debug.Log("뒤로가기");
    }

    private void OnClickHomeButton() // 홈 버튼 클릭
    {
        SceneManager.LoadScene("Main");
        Debug.Log("홈으로 이동");
    }
}