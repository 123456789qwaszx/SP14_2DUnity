using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    // ����� �ͼ�
    public AudioMixer _audioMixer;

    // �����̴�
    public Slider _bgmSlider;

    public void SetBgmVolume() // ���� ����
    {
        _audioMixer.SetFloat("BGM", Mathf.Log10(_bgmSlider.value) * 20);
    }
}
