using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingBullet : MonoBehaviour
{
    [SerializeField] float Normal_damage = 2.0f;
    [SerializeField] float _attackDelay = 1.0f;
    [SerializeField] float Normal_duration = 5.0f;
    float _damage = 2.0f;
    float _duration = 5.0f;
    [SerializeField] AudioSource _se = null;
    [SerializeField] GameObject _fx = null;
    float _delay = 0.0f;
    GameObject _target = null;
    bool _trigger = false;
    Coroutine _co = null;

    void Update()
    {
        tracking();
    }

    public void startTracking(GameObject target)
    {
        _se.Play();
        _target = target;
        _trigger = true;
        if (_co != null) StopCoroutine(_co);
        _co = StartCoroutine(stopTracking());
    }

    void tracking()
    {
        if (!_trigger) return;

        this.transform.position = Vector3.Lerp(this.transform.position, _target.transform.position + Vector3.up, 5.0f * Time.deltaTime);
        
        if(_delay >= _attackDelay)
        {
            Enemy_Hit EH = _target.GetComponent<Enemy_Hit>();
            if(EH != null)
            {
                EH.Hit(_damage);
                GameObject gm = Instantiate(_fx);
                gm.transform.position = this.transform.position;
            }
            _delay = 0.0f;
        }
        _delay += Time.deltaTime;
    }

    IEnumerator stopTracking()
    {
        yield return new WaitForSeconds(_duration);

        _delay = 0.0f;
        _se.Stop();
        _trigger = false;
        this.gameObject.SetActive(false);
    }

    public void Upgrade(int level)
    {
        switch(level)
        {
            case 2: _damage = Normal_damage * 1.5f; _duration = Normal_duration * 1.5f; break;
            case 3: _damage = Normal_damage * 2.0f; _duration = Normal_duration * 2.0f; break;
            case 4: _damage = Normal_damage * 2.5f; _duration = Normal_duration * 2.5f; break;
            default: break;
        }
    }
}
