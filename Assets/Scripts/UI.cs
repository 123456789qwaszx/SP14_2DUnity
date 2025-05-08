using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    // -메인 화면-
    // 시작, 나가기
    // 설정

    // -스테이지 선택-
    // 스테이지 선택
    // 뒤로가기
    // 설정

    // -인게임-
    // 체력바 표시
    // 점수 표시
    // 일시정지 표시
    // 슬라이딩, 점프 표시

    // -게임 종료-
    // 다시하기, 나가기
    // 점수



    // 인게임UI
    #region 

    TextMeshProUGUI currentScoreText; // 현재 점수 
    TextMeshProUGUI bestScoreText; // 최고 점수
    SpriteRenderer hpBar; // 체력
    //Button stopButton; // 일시 정지
    Button jumpButton; // 점프 버튼
    Button slidingButton; // 슬라이딩 버튼

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        // 컴포넌트와 연결 - GameUI에 UI스크립트가 있음
        Transform canvasTransform = transform.Find("Canvas");

        currentScoreText = canvasTransform.transform.Find("CurrentScoreText").GetComponent<TextMeshProUGUI>();
        bestScoreText = canvasTransform.transform.Find("BestScoreText").GetComponent<TextMeshProUGUI>();

        //hpBar = transform.Find("HpBar").GetComponent<SpriteRenderer>();

        //stopButton = transform.Find("StopButton").GetComponent<Button>();
        jumpButton = canvasTransform.transform.Find("JumpButton").GetComponent<Button>();
        slidingButton = canvasTransform.transform.Find("SlidingButton").GetComponent<Button>();

        jumpButton.onClick.AddListener(OnClickJumpButton);
        slidingButton.onClick.AddListener(OnClickSlidingButton);
    }

    public void SetUI(int currentscore, int bestscore)
    {
        currentScoreText.text = currentscore.ToString();
        bestScoreText.text = bestscore.ToString();
    }

    private void OnClickJumpButton()
    {
        Debug.Log("점프");
    }

    private void OnClickSlidingButton()
    {
        Debug.Log("슬라이딩");
    }
    #endregion
}
