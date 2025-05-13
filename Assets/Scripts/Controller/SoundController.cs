using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public static SoundController instance; // �̱��� �ν��Ͻ�

    // ����� �ͼ�
    public AudioMixer _audioMixer;

    // �����̴�
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

    public void SetMasterVolume() // ���� ���� ����
    {
        float volume = _masterSlider.value;
        _audioMixer.SetFloat("Master", Mathf.Log10(_masterSlider.value) * 20);
        PlayerPrefs.SetFloat(MasterVolumeKey, volume); // ���� �� ����
    }

    public void SetBgmVolume() // ������� ���� ����
    {
        float volume = _bgmSlider.value;
        _audioMixer.SetFloat("BGM", Mathf.Log10(_bgmSlider.value) * 20);
        PlayerPrefs.SetFloat(BgmVolumeKey, volume);
    }

    public void SetSfxVolume() // ȿ���� ���� ����
    {
        float volume = _sfxSlider.value;
        _audioMixer.SetFloat("SFX", Mathf.Log10(_sfxSlider.value) * 20);
        PlayerPrefs.SetFloat(SfxVolumeKey, volume);
    }

    private void LoadVolume()
    {
        // ����� ������ ���� �� �ε� �� �����̴� ����
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