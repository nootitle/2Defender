using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_straight : MonoBehaviour
{
    [SerializeField] float _speed = 5.0f;
    [SerializeField] float _attackPower = 10.0f;
    Vector3 Direction = Vector3.left;
    [SerializeField] bool _isAlies = true;
    [SerializeField] GameObject _hitEffect = null;
    [SerializeField] AudioSource _SE = null;
    [SerializeField] float _lifeTime = 5.0f;
    [SerializeField] bool _manualRotate = false;
    float _extraDamage = 0.0f;

    void Start()
    {
        if (_SE != null)
            _SE.Play();

        StartCoroutine(selfDestroy());
    }

    void Update()
    {
        Moving();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject gm = collision.transform.gameObject;
        if (_isAlies)
        {
            if (gm.tag.Contains("Enemy"))
            {
                gm.GetComponent<Enemy_Hit>().Hit(_attackPower + _extraDamage);
                if (_hitEffect != null)
                {
                    GameObject fx = Instantiate(_hitEffect);
                    fx.transform.position = this.transform.position;
                    Destroy(this.gameObject);
                }       
            }
            else if (gm.layer == 11)
            {
                GameObject fx = Instantiate(_hitEffect);
                fx.transform.position = this.transform.position;
                Destroy(this.gameObject);
            }
        }
        else
        {
            if (gm.tag.Contains("Player"))
            {
                gm.GetComponent<Player>().Damaged(_attackPower + _extraDamage);
                if (_hitEffect != null)
                {
                    GameObject fx = Instantiate(_hitEffect);
                    fx.transform.position = this.transform.position;
                    Destroy(this.gameObject);
                }
            }
            else if (gm.layer == 11)
            {
                GameObject fx = Instantiate(_hitEffect);
                fx.transform.position = this.transform.position;
                Destroy(this.gameObject);
            }
        }

    }

    public void setExtraDamage(int _level)
    {
        switch(_level)
        {
            case 1: _extraDamage = 0.0f; break;
            case 2: _extraDamage = 5.0f; break;
            case 3: _extraDamage = 10.0f; break;
        }
    }
    
    private void Moving()
    {
        this.transform.position += Direction * _speed * Time.deltaTime;
    }

    public void setDirection(Vector3 dir)
    {
        Direction = dir.normalized;
        
        if (dir.x < 0 && !_manualRotate)
            this.transform.eulerAngles = this.transform.eulerAngles * -1;  
    }

    public void setDirection(Vector3 dir, Vector3 angle)
    {
        Direction = dir;
        Vector3 _angle = angle;
        if (dir.x < 0)
            _angle *= -1.0f;
        this.transform.eulerAngles = _angle;
    }

    public void setAlies(bool value)
    {
        _isAlies = value;
    }

    public void addExtraDamage(float value)
    {
        _attackPower += value;
    }

    IEnumerator selfDestroy()
    {
        yield return new WaitForSeconds(_lifeTime);

        Destroy(this.gameObject);
    }
}
