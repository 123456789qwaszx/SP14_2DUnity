using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
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




    // 메인 화면
    //public GameObject mainUICanvas;
    Button startButton;
    Button exitButton;

    //// 모드 선택 화면
    //public GameObject selectmodeUICanvas;
    //Button stageButton;
    //Button endlessButton;

    //// 인게임UI
    //public GameObject gameUICanvas;
    //TextMeshProUGUI currentScoreTxt; // 현재 점수 
    //TextMeshProUGUI bestScoreTxt; // 최고 점수
    ////SpriteRenderer hpBar; // 체력
    ////Button stopButton; // 일시 정지
    //Button jumpButton; // 점프 버튼
    //Button slidingButton; // 슬라이딩 버튼

    private void Start()
    {
        MainUIInit();
        //SelectModeUIInit();
        //GameUIInit();
    }

    #region 메인 UI

    public void MainUIInit() // 초기화
    {
        //if (startUICanvas == null) return;

        // 컴포넌트와 연결 - MainUI에 UI스크립트가 있음
        //Transform startcanvasTrans = startUICanvas.transform;
        Transform mainCanvas = transform.Find("Canvas");

        if (mainCanvas != null)
        {
            startButton = mainCanvas.Find("StartButton").GetComponent<Button>();
            exitButton = mainCanvas.Find("ExitButton").GetComponent<Button>();

            //startButton.onClick.AddListener(OnClickStartButton);
            //exitButton.onClick.AddListener(OnClickExitButton);
            if (startButton != null) startButton.onClick.AddListener(OnClickStartButton);
            else Debug.LogError("StartButton을 찾을 수 없습니다!");

            if (exitButton != null) exitButton.onClick.AddListener(OnClickExitButton);
            else Debug.LogError("ExitButton을 찾을 수 없습니다!");
        }
        {
            Debug.LogError("Canvas 오브젝트를 찾을 수 없습니다!");
        }
    }

    private void OnClickStartButton()
    {
        if (startButton != null)
        {
            Debug.Log("시작");
            SceneManager.LoadScene("SelectMode");
        }
    }

    private void OnClickExitButton()
    {
        if (exitButton != null)
        {
            Debug.Log("나가기");
            Application.Quit();
        }
    }

    #endregion

    //#region 인게임 UI

    //public void GameUIInit() // 초기화
    //{
    //    if (gameUICanvas == null) return;

    //    // 컴포넌트와 연결 - GameUI에 UI스크립트가 있음
    //    Transform gamecanvasTrans = gameUICanvas.transform;

    //    currentScoreTxt = gamecanvasTrans.Find("CurrentScoreText").GetComponent<TextMeshProUGUI>();
    //    bestScoreTxt = gamecanvasTrans.Find("BestScoreText").GetComponent<TextMeshProUGUI>();

    //    jumpButton = gamecanvasTrans.Find("JumpButton").GetComponent<Button>();
    //    slidingButton = gamecanvasTrans.Find("SlidingButton").GetComponent<Button>();

    //    jumpButton.onClick.AddListener(OnClickJumpButton);
    //    slidingButton.onClick.AddListener(OnClickSlidingButton);
    //}

    //public void SetUI(int currentscore, int bestscore)
    //{
    //    currentScoreTxt.text = currentscore.ToString();
    //    bestScoreTxt.text = bestscore.ToString();
    //}

    //private void OnClickJumpButton() // 점프 버튼 클릭
    //{
    //    Debug.Log("점프");
    //}

    //private void OnClickSlidingButton() // 슬라이딩 버튼 클릭
    //{
    //    Debug.Log("슬라이딩");
    //}

    //#endregion

    //#region 모드 선택 UI

    //private void SelectModeUIInit()
    //{
    //    Transform selectCanvasTrans = selectmodeUICanvas.transform;

    //    stageButton = selectCanvasTrans.Find("StageButton").GetComponent<Button>();
    //    endlessButton = selectCanvasTrans.Find("EndlessButton").GetComponent<Button>();

    //    stageButton.onClick.AddListener(OnClickStageButton);
    //    endlessButton.onClick.AddListener(OnClickEndlessButton);
    //}

    //private void OnClickStageButton()
    //{
    //    Debug.Log("스테이지로 이동");
    //}

    //private void OnClickEndlessButton()
    //{
    //    Debug.Log("무한모드로 이동");
    //}

    //#endregion
}
