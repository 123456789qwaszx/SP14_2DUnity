using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainUI : MonoBehaviour
{
    // -���� ȭ��-
    // ����, ������
    // ����

    [SerializeField] private GameObject _soundUICanvas;

    [SerializeField]private Button startButton;
    [SerializeField]private Button exitButton;
    [SerializeField]private Button soundButton;
    [SerializeField]private Button closeButton;

    private void Start()
    {
        Init();
    }

    public void Init() // �ʱ�ȭ
    {
        // ������Ʈ�� ����
        Transform soundCanvas = _soundUICanvas.transform;

        closeButton = soundCanvas.Find("Button - Close").GetComponent<Button>();

        closeButton.onClick.AddListener(OnClickSoundUICloseButton);
    }

    public void OnClickStartButton() // ���۹�ư
    {
        SceneManager.LoadScene("SelectCharacter");
        //SceneManager.LoadScene("SelectMode");
    }

    public void OnClickExitButton() // ������ ��ư
    {
        Debug.Log("������");
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