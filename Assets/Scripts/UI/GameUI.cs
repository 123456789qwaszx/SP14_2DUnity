using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// -인게임-

// 체력바 표시
// 점수 표시
// 일시정지 표시
// 슬라이딩, 점프 표시


public class GameUI : MonoBehaviour
{
    public GameObject gameUICanvas;
    TextMeshProUGUI currentScoreTxt; // 현재 점수 
    TextMeshProUGUI bestScoreTxt; // 최고 점수
    Button jumpButton; // 점프 버튼
    Button slidingButton; // 슬라이딩 버튼
    //SpriteRenderer hpBar; // 체력
    //Button stopButton; // 일시 정지

    private void Start()
    {
        Init();
    }

    public void Init() // 초기화
    {
        if (gameUICanvas == null) return;

        // 컴포넌트와 연결 - GameUI에 UI스크립트가 있음
        Transform gamecanvasTrans = gameUICanvas.transform;

        currentScoreTxt = gamecanvasTrans.Find("CurrentScoreText").GetComponent<TextMeshProUGUI>();
        bestScoreTxt = gamecanvasTrans.Find("BestScoreText").GetComponent<TextMeshProUGUI>();

        jumpButton = gamecanvasTrans.Find("JumpButton").GetComponent<Button>();
        slidingButton = gamecanvasTrans.Find("SlidingButton").GetComponent<Button>();

        jumpButton.onClick.AddListener(OnClickJumpButton);
        slidingButton.onClick.AddListener(OnClickSlidingButton);
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
}
