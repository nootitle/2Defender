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

    void Start()
    {
        if (_SE != null)
            _SE.Play();
    }

    void Update()
    {
        Moving();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject gm = collision.collider.transform.gameObject;
        if(_isAlies)
        {
            if(gm.tag.Contains("Enemy"))
            {
                gm.GetComponent<Enemy_Hit>().Hit(_attackPower);
                if(_hitEffect != null)
                {
                    GameObject fx = Instantiate(_hitEffect);
                    fx.transform.position = this.transform.position;
                }    
                Destroy(this.gameObject);
            }
        }
        else
        {

        }
    }
    
    private void Moving()
    {
        this.transform.position += Direction * _speed * Time.deltaTime;
    }

    public void setDirection(Vector3 dir)
    {
        Direction = dir;
        if (dir.x < 0)
            this.transform.eulerAngles = new Vector3(0.0f, 0.0f, -90.0f);
    }

    public void setAlies(bool value)
    {
        _isAlies = value;
    }
}
