using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : MonoBehaviour
{
    [SerializeField] bool _isAiles = true;
    [SerializeField] float _radius = 2.0f;
    [SerializeField] float _damage = 5.0f;
    [SerializeField] float Normal_duration = 5.0f;
    float _duration = 5.0f;
    [SerializeField] GameObject _hitFx = null;
    bool _capture = false;
    Coroutine _co = null;
    [SerializeField] Animator _ani = null;

    private void OnEnable()
    {
        _capture = false;
    }

    void Update()
    {
        OutofMap();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_capture) return;

        if(_isAiles)
        {
            Collider2D[] col = Physics2D.OverlapCircleAll(this.transform.position, _radius);
            foreach (Collider2D c in col)
            {
                Enemy_Hit EH = c.transform.gameObject.GetComponent<Enemy_Hit>();
                if (EH != null)
                {
                    EH.Hit(_damage);
                    EH.Stun(_duration);
                    GameObject gm = Instantiate(_hitFx);
                    gm.transform.position = c.transform.position;
                    _capture = true;

                    if (_co != null) StopCoroutine(_co);
                    _co = StartCoroutine(trapBroken());
                    _ani.SetTrigger("Active");
                    break;
                }
            }
        }
        else
        {

        }
    }

    IEnumerator trapBroken()
    {
        yield return new WaitForSeconds(_duration);

        _capture = false;
        _ani.SetTrigger("Disable");
        this.gameObject.SetActive(false);
    }
    
    void OutofMap()
    {
        if(this.transform.position.y < -10.0f)
        {
            _capture = false;
            _ani.SetTrigger("Disable");
            this.gameObject.SetActive(false);
        }
    }

    public void UpgradeDuration(int level)
    {
        switch(level)
        {
            case 2: _duration = Normal_duration * 1.5f; break;
            case 3: _duration = Normal_duration * 2.0f; break;
            case 4: _duration = Normal_duration * 2.5f; break;
            default: _duration = Normal_duration; break;
        }
    }
}
