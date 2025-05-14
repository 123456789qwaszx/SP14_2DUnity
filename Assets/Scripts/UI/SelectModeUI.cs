using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// -�������� ����-
// �������� ����
// �ڷΰ���
// ����

public class SelectModeUI : MonoBehaviour
{
    public void OnClickStageButton()
    {
        Debug.Log("�������� ���÷� �̵�");
        SceneManager.LoadScene("Game");
    }

    public void OnClickEndlessButton() // ���Ѹ��� �̵�
    {
        Debug.Log("���Ѹ��� �̵�");
    }

    public void OnClickBackButton() // �ڷΰ���
    {
        Debug.Log("�ڷ��̵�");
        SceneManager.LoadScene("Main");
    }
}
