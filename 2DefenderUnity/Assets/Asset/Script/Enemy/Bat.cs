using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    [SerializeField] GameObject _center = null;
    [SerializeField] float _maxHp = 100.0f;
    float _hp = 100.0f;
    [SerializeField] float _walkSpeed = 5.0f;
    float direction = 0.0f;
    [SerializeField] float _patrolRange = 10.0f;
    [SerializeField] float _chaseRange = 4.0f;
    [SerializeField] float _attackDistance = 2.0f;
    [SerializeField] float _attack2Distance = 4.0f;
    [SerializeField] float _attackDelay = 2.0f;
    [SerializeField] float _attackDamage = 2.0f;
    [SerializeField] float _stun = 2.0f;
    [SerializeField] GameObject _fireBall = null;
    [SerializeField] AudioSource _hitSE = null;
    [SerializeField] AudioSource _painSE = null;
    [SerializeField] AudioSource _meleeSE = null;
    [SerializeField] bool _reverseFlip = false;
    [SerializeField] GameObject _hitFx = null;
    float _delayCount = 0.0f;
    bool _isStun = false;
    bool _isDie = false;
    bool _isAttacking = false;
    Coroutine _stunCo = null;
    Coroutine _attack1Co = null;
    Coroutine _attack2Co = null;

    private Rigidbody2D _rb = null;
    [SerializeField] Bat_Anim _pc = null;
    Vector3 _originalPosition;

    [SerializeField] GameObject _target = null;
    Player _player = null;

    public GameObject GetCenter() { return _center; }

    [SerializeField] GameObject _dropItem = null;

    void Start()
    {
        _hp = _maxHp;
        _rb = this.GetComponent<Rigidbody2D>();
        _originalPosition = this.transform.position;
        direction = 1.0f;
        _delayCount = _attackDelay;
        if (_target != null)
            _player = _target.GetComponent<Player>();
    }

    void Update()
    {
        if (_isDie) return;
        if (_isStun) return;
        if (_isAttacking) return;
        if (StageManager.Instance.pause) return;

        if (_target != null && Vector2.Distance(_target.transform.position, _center.transform.position) <= _attackDistance)
        {
             Attack();
        }
        else if(_target != null && Vector2.Distance(_target.transform.position, _center.transform.position) <= _attack2Distance)
        {
            stomping();
        }
        else if (_target != null && Vector2.Distance(_target.transform.position, _center.transform.position) <= _chaseRange)
        {
            chasing();
        }
        else
        {
            Patrol();
        }

        if (_delayCount < _attackDelay)
            _delayCount += Time.deltaTime;
    }

    private void Moving()
    {
        transform.Translate(direction * _walkSpeed * Time.deltaTime, 0.0f, 0.0f);
    }

    private void chasing()
    {
        float dir = _target.transform.position.x - _center.transform.position.x;
        float dir_y = _target.transform.position.y - _center.transform.position.y;

        _pc.MoveAnim(direction * _walkSpeed);
        if (dir > 0)
        {
            if (_reverseFlip) _pc.setFlip(true);
            direction = 1;
        }
        else
        {
            if (_reverseFlip) _pc.setFlip(false);
            direction = -1;
        }

        if(dir_y > 0)
            transform.Translate(direction * _walkSpeed * Time.deltaTime, _walkSpeed * Time.deltaTime, 0.0f);
        else
            transform.Translate(direction * _walkSpeed * Time.deltaTime, -_walkSpeed * Time.deltaTime, 0.0f);
    }

    private void Patrol()
    {
        if (_center.transform.position.x < _originalPosition.x - _patrolRange)
        {
            direction = 1;
            _pc.MoveAnim(direction * _walkSpeed);
            if (_reverseFlip) _pc.setFlip(true);
            Moving();
        }
        else if (_center.transform.position.x > _originalPosition.x + _patrolRange)
        {
            direction = -1;
            _pc.MoveAnim(direction * _walkSpeed);
            if (_reverseFlip) _pc.setFlip(false);
            Moving();
        }
        else
        {
            _pc.MoveAnim(direction * _walkSpeed);
            Moving();
        }
    }

    void Attack()
    {
        if (_delayCount >= _attackDelay)
        {
            _isAttacking = true;
            _pc.Attack();
            _meleeSE.Play();
            if (_attack1Co != null) StopCoroutine(_attack1Co);
            _attack1Co = StartCoroutine(Attack1Process());
        }
        else
            _pc.MoveAnim(0.0f);
    }

    IEnumerator Attack1Process()
    {
        yield return new WaitForSeconds(0.5f);

        if(Vector3.Distance(_center.transform.position, _target.transform.position) < _attackDistance)
        {
            if (_target.transform.position.x - _center.transform.position.x > 0)
            {
                if (_reverseFlip) _pc.setFlip(true);
                else _pc.setFlip(false);

                GameObject gm = Instantiate(_hitFx);
                gm.transform.position = _target.transform.position;
                _player.Damaged(_attackDamage);
            }
            else
            {
                if (_reverseFlip) _pc.setFlip(false);
                else _pc.setFlip(true);

                GameObject gm = Instantiate(_hitFx);
                gm.transform.position = _target.transform.position;
                _player.Damaged(_attackDamage);
            }
        }
        _delayCount = 0.0f;
        _pc.MoveAnim(0.0f);
        _isAttacking = false;
    }

    void stomping()
    {
        if (_delayCount >= _attackDelay)
        {
            _isAttacking = true;
            _pc.Stomp();
            _meleeSE.Play();
            if (_attack2Co != null) StopCoroutine(_attack2Co);
            _attack2Co = StartCoroutine(Attack2Process());
        }
    }

    IEnumerator Attack2Process()
    {
        yield return new WaitForSeconds(0.5f);

        Vector3 _angle = Vector3.zero;
        Vector3 _dir = Vector3.right;
        for (int i = 0; i < 6; ++i)
        {
            switch (i)
            {
                case 0: _angle = new Vector3(0.0f, 0.0f, 75.0f); _dir = new Vector3(0.8f, 0.5f, 0.0f); break;
                case 1: _angle = new Vector3(0.0f, 0.0f, 0.0f); _dir = new Vector3(1.0f, 0.0f, 0.0f); break;
                case 2: _angle = new Vector3(0.0f, 0.0f, -75.0f); _dir = new Vector3(0.8f, -0.50f, 0.0f); break;
                case 3: _angle = new Vector3(0.0f, 0.0f, 75.0f); _dir = new Vector3(-0.8f, 0.5f, 0.0f); break;
                case 4: _angle = new Vector3(0.0f, 0.0f, 0.0f); _dir = new Vector3(-1.0f, 0.0f, 0.0f); break;
                case 5: _angle = new Vector3(0.0f, 0.0f, -75.0f); _dir = new Vector3(-0.8f, -0.5f, 0.0f); break;
            }

            GameObject gm = Instantiate(_fireBall);
            gm.GetComponent<Bullet_straight>().setDirection(_dir, _angle);
            gm.transform.position = _center.transform.position;
        }
        _delayCount = 0.0f;
        _pc.MoveAnim(0.0f);
        _isAttacking = false;
    }

    public void Damaged(float value)
    {
        if (_isDie) return;

        _hitSE.Play();
        _painSE.Play();
        _pc.DamagedAnim();
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

    void Die()
    {
        _pc.DieAnim();
        _isDie = true;
        _rb.constraints = RigidbodyConstraints2D.None;
        _rb.AddForce(Vector2.down * 5.0f * Time.deltaTime);

        int rnd = Random.Range(0, 100);
        if (rnd < 5)
            dropItem();

        StartCoroutine(SelfDestroy());
    }

    IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(1.0f);

        _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        EnemyManager.Instance.deathCount();
        this.gameObject.SetActive(false);
    }

    public void respawn()
    {
        _isDie = false;
        _isStun = false;
        _originalPosition = this.transform.position;
        _hp = _maxHp;
    }

    public bool GetDead()
    {
        return _isDie;
    }

    IEnumerator Stun()
    {
        yield return new WaitForSeconds(_stun);

        if (!_isDie)
        {
            _isStun = false;
            _pc.MoveAnim(_rb.velocity.x);
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
            _pc.MoveAnim(_rb.velocity.x);
        }
    }

    void dropItem()
    {
        if (_dropItem != null)
        {
            GameObject gm = Instantiate(_dropItem);
            gm.transform.position = _center.transform.position;
        }
    }
}
