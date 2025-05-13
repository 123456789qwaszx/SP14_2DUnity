using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance; // �̱��� �������� ��𼭵� ���� �����ϵ���
    public AudioSource _effectSoundSource; // ȿ���� ����� ���� AudioSource ������Ʈ
    public AudioClip _buttonClickSound; // ��ư Ŭ�� ȿ���� (�ٸ� ȿ�����鵵 �ʿ信 ���� �߰�)

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    public void PlayButtonClickSound()
    {
        _effectSoundSource.PlayOneShot(_buttonClickSound);
    }
}