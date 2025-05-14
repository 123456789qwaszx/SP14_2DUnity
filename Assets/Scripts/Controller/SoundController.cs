using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public static SoundController instance; // 싱글톤 인스턴스

    [SerializeField] private AudioMixer _audioMixer; // 오디오 믹서
    [SerializeField] private Slider _masterSlider; // 슬라이더
    [SerializeField] private Slider _bgmSlider;
    [SerializeField] private Slider _sfxSlider;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetMasterVolume() // 메인 볼륨 조절
    {
        float volume = _masterSlider.value;
        _audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }

    public void SetBgmVolume() // 배경음악 볼륨 조절
    {
        float volume = _bgmSlider.value;
        _audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
    }

    public void SetSfxVolume() // 효과음 볼륨 조절
    {
        float volume = _sfxSlider.value;
        _audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }
}