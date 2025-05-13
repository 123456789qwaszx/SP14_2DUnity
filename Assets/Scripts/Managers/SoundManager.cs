using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    // 오디오 믹서
    public AudioMixer _audioMixer;

    // 슬라이더
    public Slider _bgmSlider;

    public void SetBgmVolume() // 볼륨 조절
    {
        _audioMixer.SetFloat("BGM", Mathf.Log10(_bgmSlider.value) * 20);
    }
}
