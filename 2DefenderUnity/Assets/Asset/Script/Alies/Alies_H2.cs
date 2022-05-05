using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alies_H2 : MonoBehaviour
{
    [SerializeField] GameObject _center = null;
    [SerializeField] float _maxHp = 100.0f;
    float _hp = 100.0f;
    [SerializeField] float _jumpPower = 5.0f;
    [SerializeField] float _walkSpeed = 5.0f;
    [SerializeField] float _sprintSpeed = 10.0f;
    float direction = 0.0f;
    [SerializeField] float _patrolRange = 10.0f;
    [SerializeField] float _chaseRange = 4.0f;
    [SerializeField] float _attackDistance = 2.0f;
    [SerializeField] float _attackDelay = 2.0f;
    [SerializeField] float _attackDamage = 2.0f;
    [SerializeField] float _stun = 2.0f;
    [SerializeField] AudioSource _hitSE = null;
    [SerializeField] AudioSource _painSE = null;
    [SerializeField] AudioSource _meleeSE = null;
    [SerializeField] bool _reverseFlip = false;
    [SerializeField] GameObject _hitFx = null;
    float _delayCount = 0.0f;
    bool _jumpTrigger = false;
    bool _sprintTrigger = false;
    bool _isStun = false;
    bool _isDie = false;
    Coroutine _sprintCo;
    Coroutine _stunCo;
    Coroutine _jumpCo;

    private Rigidbody2D _rb = null;
    [SerializeField] Alies_H2_Anim _pc = null;
    Vector3 _originalPosition;

    [SerializeField] GameObject _target = null;
    Enemy_Hit _EH = null;

    public GameObject GetCenter() { return _center; }

    [SerializeField] GameObject _dropItem = null;

    [SerializeField] float _activeTime = 15.0f;
    float _time = 0.0f;

    void Start()
    {
        _hp = _maxHp;
        _rb = this.GetComponent<Rigidbody2D>();
        _originalPosition = this.transform.position;
        direction = 1.0f;
        _delayCount = _attackDelay;
        if (_target != null)
            _EH = _target.GetComponent<Enemy_Hit>();
        jumpCoolDown();
        changingTarget();
    }

    void Update()
    {
        if (_isDie) return;
        if (_isStun) return;
        if (_jumpTrigger)
        {
            transform.Translate(direction * _walkSpeed * Time.deltaTime, 0.0f, 0.0f);
            return;
        }
        if (StageManager.Instance.pause) return;
        if (_target == null || !_target.activeSelf) changingTarget();

        if (_target != null && Vector2.Distance(_target.transform.position, _center.transform.position) <= _attackDistance)
        {
            if (_target.transform.position.y > _center.transform.position.y)
                stomping();
            else
                Attack();
        }
        else if (_target != null && Vector2.Distance(_target.transform.position, _center.transform.position) <= _chaseRange)
        {
            _sprintTrigger = true;
            int rnd = Random.Range(0, 1000);
            if (!_jumpTrigger && rnd < 5)
                jump();
            else
                chasing();
        }
        else
        {
            _sprintTrigger = false;
            changingTarget();
        }

        if (_delayCount < _attackDelay)
            _delayCount += Time.deltaTime;

        if (_time < _activeTime)
            _time += Time.deltaTime;
        else
        {
            _time = 0.0f;
            Die();
        }
    }

    void changingTarget()
    {
        Collider2D[] co = Physics2D.OverlapCircleAll(_center.transform.position, _chaseRange);
        for (int i = 0; i < co.Length; ++i)
        {
            Enemy_Hit temp = co[i].gameObject.GetComponent<Enemy_Hit>();
            if (temp != null && co[i].gameObject.activeSelf)
            {
                _EH = temp;
                _target = co[i].gameObject;
                break;
            }
        }
    }

    private void Moving()
    {
        if (!_sprintTrigger)
            transform.Translate(direction * _walkSpeed * Time.deltaTime, 0.0f, 0.0f);
        else
            transform.Translate(direction * _sprintSpeed * Time.deltaTime, 0.0f, 0.0f);

        int rnd = Random.Range(0, 1000);
        if (!_jumpTrigger && rnd < 5)
            jump();
    }

    private void chasing()
    {
        float dir = _target.transform.position.x - _center.transform.position.x;
        _pc.MoveAnim(_sprintTrigger, false, direction * _sprintSpeed);
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
        transform.Translate(direction * _sprintSpeed * Time.deltaTime, 0.0f, 0.0f);
    }

    private void Patrol()
    {
        if (_center.transform.position.x < _originalPosition.x - _patrolRange)
        {
            direction = 1;
            _pc.MoveAnim(_sprintTrigger, false, direction * _walkSpeed);
            if (_reverseFlip) _pc.setFlip(true);
            Moving();
        }
        else if (_center.transform.position.x > _originalPosition.x + _patrolRange)
        {
            direction = -1;
            _pc.MoveAnim(_sprintTrigger, false, direction * _walkSpeed);
            if (_reverseFlip) _pc.setFlip(false);
            Moving();
        }
        else
        {
            _pc.MoveAnim(_sprintTrigger, false, direction * _walkSpeed);
            Moving();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        jumpCoolDown();
    }

    private void jump()
    {
        if (!_jumpTrigger)
        {
            if (_jumpCo != null) StopCoroutine(_jumpCo);
            _jumpCo = StartCoroutine(jumpCoolDownIE());
            _jumpTrigger = true;
            _pc.jumpAnim();
            if (direction == 1)
                _rb.AddForce(Vector3.up * _jumpPower + Vector3.right * 5.0f, ForceMode2D.Impulse);
            else if (direction == -1)
                _rb.AddForce(Vector3.up * _jumpPower + Vector3.left * 5.0f, ForceMode2D.Impulse);
            else
                _rb.AddForce(Vector3.up * _jumpPower, ForceMode2D.Impulse);
        }
    }

    private void sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (!_sprintTrigger)
                _sprintCo = StartCoroutine(sprintTerm());
        }
        else
        {
            if (_sprintCo != null)
                StopCoroutine(_sprintCo);
            _sprintTrigger = false;
        }
    }

    void jumpCoolDown()
    {
        if (_jumpTrigger)
        {
            _jumpTrigger = false;
            _pc.jumpCoolDown();
        }
    }

    IEnumerator jumpCoolDownIE()
    {
        yield return new WaitForSeconds(5.0f);

        jumpCoolDown();
    }

    IEnumerator sprintTerm()
    {
        yield return new WaitForSeconds(0.5f);

        _sprintTrigger = true;
    }

    void Attack()
    {
        if (_delayCount >= _attackDelay)
        {
            _pc.Attack();
            _meleeSE.Play();
            if (_target.transform.position.x - _center.transform.position.x > 0)
            {
                if (_reverseFlip) _pc.setFlip(true);
                else _pc.setFlip(false);

                GameObject gm = Instantiate(_hitFx);
                gm.transform.position = _target.transform.position;
            }
            else
            {
                if (_reverseFlip) _pc.setFlip(false);
                else _pc.setFlip(true);

                GameObject gm = Instantiate(_hitFx);
                gm.transform.position = _target.transform.position;
            }
            _EH.Hit(_attackDamage);
            _delayCount = 0.0f;
        }
        else
            _pc.MoveAnim(false, false, 0);
    }

    IEnumerator AttackProcess()
    {
        yield return new WaitForSeconds(0.1f);

        _meleeSE.Play();
        if (_target.transform.position.x - _center.transform.position.x > 0)
        {
            if (_reverseFlip) _pc.setFlip(true);
            else _pc.setFlip(false);

            GameObject gm = Instantiate(_hitFx);
            gm.transform.position = _target.transform.position;
        }
        else
        {
            if (_reverseFlip) _pc.setFlip(false);
            else _pc.setFlip(true);

            GameObject gm = Instantiate(_hitFx);
            gm.transform.position = _target.transform.position;
        }
        _EH.Hit(_attackDamage);
        _delayCount = 0.0f;
    }

    void stomping()
    {
        if (_delayCount >= _attackDelay)
        {
            _pc.Stomp();
            _meleeSE.Play();
            if (_target.transform.position.x - _center.transform.position.x > 0)
            {
                if (_reverseFlip) _pc.setFlip(true);
                else _pc.setFlip(false);

                GameObject gm = Instantiate(_hitFx);
                gm.transform.position = _target.transform.position;
            }
            else
            {
                if (_reverseFlip) _pc.setFlip(false);
                else _pc.setFlip(true);

                GameObject gm = Instantiate(_hitFx);
                gm.transform.position = _target.transform.position;
            }
            _EH.Hit(_attackDamage);
            _delayCount = 0.0f;
        }
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
        _jumpTrigger = false;

        int rnd = Random.Range(0, 100);
        if (rnd < 5)
            dropItem();

        StartCoroutine(SelfDestroy());
    }

    IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(1.0f);

        EnemyManager.Instance.deathCount();
        this.gameObject.SetActive(false);
    }

    public void respawn()
    {
        _isDie = false;
        _isStun = false;
        _jumpTrigger = false;
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
            _pc.MoveAnim(_sprintTrigger, false, _rb.velocity.x);
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
            _pc.MoveAnim(_sprintTrigger, false, _rb.velocity.x);
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
