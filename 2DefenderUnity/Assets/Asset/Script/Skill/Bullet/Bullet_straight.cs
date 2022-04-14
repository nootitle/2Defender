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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject gm = collision.transform.gameObject;
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

    IEnumerator selfDestroy()
    {
        yield return new WaitForSeconds(_lifeTime);

        Destroy(this.gameObject);
    }
}
