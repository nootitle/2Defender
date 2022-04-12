using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepSE : MonoBehaviour
{
    [SerializeField] List<AudioSource> FootStep = null;
    [SerializeField] List<AudioSource> FootStepRun = null;
    [SerializeField] List<AudioSource> Jump = null;
    [SerializeField] List<AudioSource> Land = null;
    int rnd, rnd_Run, rnd_Jump, rnd_Land;

    public void PlayWalk()
    {
        if (FootStep.Count == 0) return;
        if (FootStep[rnd].isPlaying) return;
        rnd = Random.Range(0, FootStep.Count - 1);
        FootStep[rnd].Play();
    }

    public void PlayRun()
    {
        if (FootStepRun.Count == 0) return;
        if (FootStepRun[rnd_Run].isPlaying) return;
        rnd_Run = Random.Range(0, FootStepRun.Count - 1);
        FootStepRun[rnd_Run].Play();
    }

    public void PlayJump()
    {
        if (Jump.Count == 0) return;
        if (Jump[rnd_Jump].isPlaying) return;
        rnd_Jump = Random.Range(0, Jump.Count - 1);
        Jump[rnd_Jump].Play();
    }

    public void PlayLand()
    {
        if (Land.Count == 0) return;
        if (Land[rnd_Land].isPlaying) return;
        rnd_Land = Random.Range(0, Land.Count - 1);
        Land[rnd_Land].Play();
    }
}
