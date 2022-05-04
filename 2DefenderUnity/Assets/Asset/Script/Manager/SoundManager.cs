using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance = null;

    [SerializeField] List<AudioSource> bgmList = null;
    [SerializeField] AudioSource _clickSE = null;
    [SerializeField] AudioSource _bossBgm = null;
    [SerializeField] AudioSource _clearBgm = null;
    [SerializeField] AudioSource _defeatBgm = null;

    [SerializeField] AudioMixer _mixer = null;
    [SerializeField] Slider _slider = null;

    bool _isPlayBGM = true;
    [SerializeField] int BGM_ID = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        if (bgmList.Count != 0 && BGM_ID < bgmList.Count && !bgmList[BGM_ID].isPlaying)
            bgmList[BGM_ID].Play();

        float volume = PlayerPrefs.GetFloat("MasterVolumeOption", 0.75f);
        _mixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }

    void Update()
    {
        playBGM();
    }
    
    void playBGM()
    {
        if (!_isPlayBGM) return;

        if(bgmList.Count > 1)
        {
            if (BGM_ID + 1 < bgmList.Count && !bgmList[BGM_ID].isPlaying && !bgmList[BGM_ID + 1].isPlaying)
                bgmList[++BGM_ID].Play();
            else if (BGM_ID + 1 == bgmList.Count && !bgmList[BGM_ID].isPlaying && !bgmList[0].isPlaying)
            {
                bgmList[0].Play();
                BGM_ID = 0;
            }
        }
        else if(bgmList.Count == 1)
        {
            if (!bgmList[BGM_ID].isPlaying)
                bgmList[BGM_ID].Play();
        }
    }

    public void switchBGM(bool value)
    {
        _isPlayBGM = value;
        if(!value)
        {
            int i = 0;
            foreach(AudioSource ac in bgmList)
            {
                if (ac.isPlaying)
                {
                    BGM_ID = i;
                    ac.Stop();
                }
                ++i;
            }
        }
    }

    public void playBossBGM()
    {
        _bossBgm.Play();
        switchBGM(false);
    }

    public void clearBGM()
    {
        _bossBgm.Stop();
        switchBGM(false);
        _clearBgm.Play();
    }

    public void defeatBGM()
    {
        if (_bossBgm.isPlaying) _bossBgm.Stop();
        switchBGM(false);
        _defeatBgm.Play();
    }

    public void clickSE()
    {
        _clickSE.Play();
    }

    public void initMasterVolumeSlider()
    {
        _slider.value = PlayerPrefs.GetFloat("MasterVolumeOption", 0.75f);
    }

    public void setMasterVolume()
    {
        _mixer.SetFloat("Master", Mathf.Log10(_slider.value) * 20);
        PlayerPrefs.SetFloat("MasterVolumeOption", _slider.value);
    }
}
