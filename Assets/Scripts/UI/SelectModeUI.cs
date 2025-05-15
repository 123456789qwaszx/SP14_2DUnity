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
    public void OnClickStageButton() // 게임으로 이동
    {
        GameUI.isEndless = false;
        Debug.Log("게임으로 이동");
        SceneManager.LoadScene("Game");
    }

    public void OnClickEndlessButton() // 무한모드로 이동
    {
        GameUI.isEndless = true;
        Debug.Log("무한모드로 이동");
        SceneManager.LoadScene("Game");
    }

    public void OnClickBackButton() // 뒤로가기
    {
        Debug.Log("뒤로이동");
        SceneManager.LoadScene("SelectCharacter");
    }
}
