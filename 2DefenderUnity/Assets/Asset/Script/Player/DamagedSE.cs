using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagedSE : MonoBehaviour
{
    [SerializeField] List<AudioSource> _Damaged = null;
    int rnd;

    public void PlayDamaged()
    {
        if(_Damaged != null)
        {
            rnd = Random.Range(0, _Damaged.Count - 1);
            _Damaged[rnd].Play();
        }
    }
}
