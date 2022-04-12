using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitAndDestroy : MonoBehaviour
{
    [SerializeField] float _destroyTime = 3.0f;
    [SerializeField] AudioSource _SE = null;

    void Start()
    {
        if (_SE != null) _SE.Play();
        StartCoroutine(selfDestroy());
    }

    IEnumerator selfDestroy()
    {
        yield return new WaitForSeconds(_destroyTime);

        Destroy(this.gameObject);
    }
}
