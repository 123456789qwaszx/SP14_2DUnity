using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// -스테이지 선택-
// 스테이지 선택
// 뒤로가기
// 설정

public class SelectModeUI : MonoBehaviour
{
    public void OnClickStageButton() // 스테이지 선택으로 이동
    {
        Debug.Log("스테이지 선택로 이동");
        SceneManager.LoadScene("Game");
    }

    public void OnClickEndlessButton() // 무한모드로 이동
    {
        Debug.Log("무한모드로 이동");
    }

    public void OnClickBackButton() // 뒤로가기
    {
        Debug.Log("뒤로이동");
        SceneManager.LoadScene("Main");
    }
}
