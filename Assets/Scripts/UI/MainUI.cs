using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainUI : MonoBehaviour
{
    // -메인 화면-
    // 시작, 나가기
    // 설정
    [SerializeField] private GameObject _soundUICanvas;

    private Button closeButton;

    private void Start()
    {
        Init();
    }

    public void Init() // �ʱ�ȭ
    {
        // 컴포넌트와 연결
        Transform soundCanvas = _soundUICanvas.transform;

        closeButton = soundCanvas.Find("Button - Close").GetComponent<Button>();
        closeButton.onClick.AddListener(OnClickSoundUICloseButton);
    }

    public void OnClickStartButton() // 시작버튼
    {
        SceneManager.LoadScene("SelectCharacter");
    }

    public void OnClickExitButton() // 나가기 버튼
    {
        Debug.Log("나가기");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void OnClickSoundButton()
    {
        _soundUICanvas.SetActive(true);
    }

    public void OnClickSoundUICloseButton()
    {
        _soundUICanvas.SetActive(false);
    }
}