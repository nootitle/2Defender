using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance = null;

    [SerializeField] List<AudioSource> bgmList = null;
    [SerializeField] AudioSource _clickSE = null;
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

    public void clickSE()
    {
        _clickSE.Play();
    }
}
