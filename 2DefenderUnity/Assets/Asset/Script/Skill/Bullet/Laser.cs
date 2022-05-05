using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] float _destroyTime = 3.0f;
    [SerializeField] AudioSource _SE = null;
    [SerializeField] float _damage = 50.0f;
    bool _hit = false;

    private void OnEnable()
    {
        _hit = true;
        if (_SE != null) _SE.Play();
        StartCoroutine(selfDestroy());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(_hit)
        {
            Player PL = collision.gameObject.GetComponent<Player>();
            if(PL != null)
            {
                PL.Damaged(_damage);
                _hit = false;
            }

            Alies AL = collision.gameObject.GetComponent<Alies>();
            if(AL != null)
            {
                AL.Hit(_damage);
                _hit = false;
            }
        }
    }

    IEnumerator selfDestroy()
    {
        yield return new WaitForSeconds(_destroyTime);

        this.gameObject.SetActive(false);
    }
}
