using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance; // 싱글톤 패턴으로 어디서든 접근 가능하도록
    public AudioSource _effectSoundSource; // 효과음 재생을 위한 AudioSource 컴포넌트
    public AudioClip _buttonClickSound; // 버튼 클릭 효과음 (다른 효과음들도 필요에 따라 추가)

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