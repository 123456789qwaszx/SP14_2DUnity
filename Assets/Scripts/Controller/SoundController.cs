using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public static SoundController instance; // �̱��� �ν��Ͻ�

    [SerializeField] private AudioMixer _audioMixer; // ����� �ͼ�
    [SerializeField] private Slider _masterSlider; // �����̴�
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

    public void SetMasterVolume() // ���� ���� ����
    {
        float volume = _masterSlider.value;
        _audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }

    public void SetBgmVolume() // ������� ���� ����
    {
        float volume = _bgmSlider.value;
        _audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
    }

    public void SetSfxVolume() // ȿ���� ���� ����
    {
        float volume = _sfxSlider.value;
        _audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }
}