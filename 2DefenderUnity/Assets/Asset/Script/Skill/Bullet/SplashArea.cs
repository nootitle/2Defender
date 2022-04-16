using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashArea : MonoBehaviour
{
    [SerializeField] float _splashDamage = 10.0f;
    [SerializeField] float _radius = 10.0f;
    [SerializeField] float _explosionDelay = 1.0f;
    [SerializeField] GameObject _hitFx = null;
    [SerializeField] bool _hitFxExist = false;
    [SerializeField] bool _isAlies = true;
    [SerializeField] float _attackDelay = 1.0f;
    float _delayCount = 1.0f;

    private void OnEnable()
    {
        _delayCount = _attackDelay;
    }

    private void Update()
    {
        explosion();
        _delayCount += Time.deltaTime;
    }

    void explosion()
    {
        if (_delayCount < _attackDelay) return;

        Collider2D[] col = Physics2D.OverlapCircleAll(this.transform.position, _radius);
        foreach (Collider2D c in col)
        {
            if(_isAlies)
            {
                Enemy_Hit EH = c.transform.gameObject.GetComponent<Enemy_Hit>();
                if (EH != null)
                {
                    EH.Hit(_splashDamage);
                    if (_hitFxExist)
                    {
                        GameObject gm = Instantiate(_hitFx);
                        gm.transform.position = c.transform.position;
                    }
                }
                _delayCount = 0.0f;
            }
            else
            {
                Player PL = c.transform.gameObject.GetComponent<Player>();
                if(PL != null)
                {
                    PL.Damaged(_splashDamage);
                    if(_hitFxExist)
                    {
                        GameObject gm = Instantiate(_hitFx);
                        gm.transform.position = c.transform.position;
                    }
                }
                _delayCount = 0.0f;
            }
        }
    }
}
