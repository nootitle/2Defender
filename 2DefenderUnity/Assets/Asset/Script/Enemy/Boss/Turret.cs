using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] float _maxHp = 250.0f;
    float _hp = 250.0f;
    [SerializeField] GameObject _bullet = null;
    [SerializeField] float _attackDelay = 2.0f;
    [SerializeField] float _stun = 0.5f;
    [SerializeField] GameObject _target = null;
    [SerializeField] GameObject _fireFx = null;
    [SerializeField] AudioSource _hitSE = null;
    float _delay = 0.0f;
    bool _isDie = false;
    bool _isStun = false;
    Vector3 eulerCalc = Vector3.zero;
    Coroutine _stunCo = null;
    void Start()
    {
        _hp = _maxHp;
        _delay = _attackDelay;
        if (_target == null)
            _target = GameObject.Find("Player");
    }

    void Update()
    {
        if (_isDie) return;
        if (_isStun) return;
        if (StageManager.Instance.pause) return;

        LookAtTarget();
        Attack();
    }

    void Attack()
    {
        if (_delay >= _attackDelay)
        {
            CalculateEulerForTarget();
            if(_bullet != null)
            {
                Vector3 dir = _target.transform.position - this.transform.position;
                GameObject gm = Instantiate(_bullet);
                gm.transform.position = this.transform.position;
                gm.GetComponent<Bullet_straight>().setDirection(dir);
                gm.transform.up = eulerCalc;
            }
            if (_fireFx != null)
                _fireFx.SetActive(true);

            _delay = 0.0f;
        }
        else
            _delay += Time.deltaTime;
        
    }

    void CalculateEulerForTarget()
    {
        if(_target != null)
        {
            Vector3 dir = new Vector3(_target.transform.position.x - this.transform.position.x, _target.transform.position.y - this.transform.position.y, 0.0f);
            dir.Normalize();
            /*
            float angle;
            if (_target.transform.position.y > this.transform.position.y)
                angle = Vector3.Angle(Vector3.left, dir);
            else
                angle = Mathf.Abs(Vector3.Angle(Vector3.left, dir) + 90.0f);
            eulerCalc = new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y, angle);
            */
            eulerCalc = dir;
        }
    }

    void LookAtTarget()
    {
        this.transform.up = Vector3.Lerp(this.transform.up, eulerCalc, 5.0f * Time.deltaTime);
    }

    public void Damaged(float value)
    {
        if (_isDie) return;

        _hitSE.Play();
        _hp -= value;

        if (_hp <= 0)
            Die();
        else
        {
            _isStun = true;
            if (_stunCo != null) StopCoroutine(_stunCo);
            _stunCo = StartCoroutine("Stun");
        }
    }

    IEnumerator Stun()
    {
        yield return new WaitForSeconds(_stun);

        if (!_isDie)
        {
            _isStun = false;
        }
    }
    public void CallStun(float duration)
    {
        _isStun = true;
        if (_stunCo != null) StopCoroutine(_stunCo);
        _stunCo = StartCoroutine(exitCallStunFunction(duration));
    }

    IEnumerator exitCallStunFunction(float duration)
    {
        yield return new WaitForSeconds(duration);

        if (!_isDie)
        {
            _isStun = false;
        }
    }

    void Die()
    {
        _isDie = true;
        StartCoroutine(SelfDestroy());
    }

    public bool GetDead()
    {
        return _isDie;
    }

    public void respawn()
    {
        _isDie = false;
        _isStun = false;
        _hp = _maxHp;
    }

    IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(1.0f);

        EnemyManager.Instance.deathCount();
        this.gameObject.SetActive(false);
    }
}
