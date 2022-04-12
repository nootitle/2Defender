using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSE : MonoBehaviour
{
    [SerializeField] List<AudioSource> blade = null;
    int rnd_blade;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void bladeSE()
    {
        if(blade.Count > 0 && !blade[rnd_blade].isPlaying)
        {
            rnd_blade = Random.Range(0, blade.Count - 1);
            blade[rnd_blade].Play();
        }
    }
}
