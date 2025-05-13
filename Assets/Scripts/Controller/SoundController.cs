using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public static SoundController instance; // 싱글톤 인스턴스

    // 오디오 믹서
    public AudioMixer _audioMixer;

    // 슬라이더
    public Slider _masterSlider;
    public Slider _bgmSlider;
    public Slider _sfxSlider;

    private const string MasterVolumeKey = "MasterVolume";
    private const string BgmVolumeKey = "BgmVolume";
    private const string SfxVolumeKey = "SfxVolume";

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

        LoadVolume();
    }

    public void SetMasterVolume() // 메인 볼륨 조절
    {
        float volume = _masterSlider.value;
        _audioMixer.SetFloat("Master", Mathf.Log10(_masterSlider.value) * 20);
        PlayerPrefs.SetFloat(MasterVolumeKey, volume); // 볼륨 값 저장
    }

    public void SetBgmVolume() // 배경음악 볼륨 조절
    {
        float volume = _bgmSlider.value;
        _audioMixer.SetFloat("BGM", Mathf.Log10(_bgmSlider.value) * 20);
        PlayerPrefs.SetFloat(BgmVolumeKey, volume);
    }

    public void SetSfxVolume() // 효과음 볼륨 조절
    {
        float volume = _sfxSlider.value;
        _audioMixer.SetFloat("SFX", Mathf.Log10(_sfxSlider.value) * 20);
        PlayerPrefs.SetFloat(SfxVolumeKey, volume);
    }

    private void LoadVolume()
    {
        // 저장된 마스터 볼륨 값 로드 및 슬라이더 설정
        if (PlayerPrefs.HasKey(MasterVolumeKey))
        {
            float volume = PlayerPrefs.GetFloat(MasterVolumeKey);
            _masterSlider.value = volume;
            _audioMixer.SetFloat("Master", Mathf.Log10(_masterSlider.value) * 20);
        }
        else
        {
            SetMasterVolume();
        }

        if (PlayerPrefs.HasKey(BgmVolumeKey))
        {
            float volume = PlayerPrefs.GetFloat(BgmVolumeKey);
            _bgmSlider.value = volume;
            _audioMixer.SetFloat("BGM", Mathf.Log10(_bgmSlider.value) * 20);
        }
        else
        {
            SetBgmVolume();
        }

        if (PlayerPrefs.HasKey(SfxVolumeKey))
        {
            float volume = PlayerPrefs.GetFloat(SfxVolumeKey);
            _bgmSlider.value = volume;
            _audioMixer.SetFloat("SFX", Mathf.Log10(_sfxSlider.value) * 20);
        }
        else
        {
            SetSfxVolume();
        }
    }
}