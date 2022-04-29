using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_AirBomb : MonoBehaviour
{
    [SerializeField] GameObject _bulletBody = null;
    [SerializeField] GameObject _explosion = null;
    [SerializeField] AudioSource _SE = null;
    bool _start = false;

    private void OnEnable()
    {
        _explosion.SetActive(false);
        _bulletBody.SetActive(true);
        _start = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(_start)
        {
            _SE.Play();
            _bulletBody.SetActive(false);
            _explosion.SetActive(true);
            _start = false;
        }
    }
}
