using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSE : MonoBehaviour
{
    [SerializeField] List<AudioSource> blade = null;
    int rnd_blade;

    public void bladeSE()
    {
        if(blade.Count > 0)
        {
            int temp = Random.Range(0, blade.Count - 1);
            if (temp == rnd_blade)
            {
                if(temp == 0)
                    blade[++temp].Play();
                else
                    blade[--temp].Play();
            }
            else
            {
                blade[temp].Play();
            }
            rnd_blade = temp;
        }
    }
}
