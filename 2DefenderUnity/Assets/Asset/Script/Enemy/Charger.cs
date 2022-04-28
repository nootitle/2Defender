using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : MonoBehaviour
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
    [SerializeField] AudioSource _preSE = null;
    [SerializeField] AudioSource _chargeSE = null;
    [SerializeField] AudioSource _normalAttackSE = null;
    [SerializeField] bool _reverseFlip = false;
    float _delayCount = 0.0f;
    bool _jumpTrigger = false;
    bool _sprintTrigger = false;
    bool _isStun = false;
    bool _isDie = false;
    bool _isAttacking = false;
    Coroutine _sprintCo;
    Coroutine _stunCo;
    Coroutine _jumpCo;
    Coroutine _chargerCo;

    private Rigidbody2D _rb = null;
    [SerializeField] Charger_Anim _pc = null;
    Vector3 _originalPosition;

    [SerializeField] GameObject _target = null;
    Player _player = null;

    public GameObject GetCenter() { return _center; }

    void Start()
    {
        _hp = _maxHp;
        _rb = this.GetComponent<Rigidbody2D>();
        _originalPosition = this.transform.position;
        direction = 1.0f;
        _delayCount = _attackDelay;
        if (_target != null)
            _player = _target.GetComponent<Player>();
        jumpCoolDown();
    }

    void Update()
    {
        //jump();
        //sprint();
        if (_isDie) return;
        if (_isStun) return;
        if (_isAttacking) return;
        if (_jumpTrigger)
        {
            transform.Translate(direction * _walkSpeed * Time.deltaTime, 0.0f, 0.0f);
            return;
        }
        if (_isAttacking)
        {
            transform.Translate(direction * _sprintSpeed * Time.deltaTime, 0.0f, 0.0f);
            return;
        }
        if (StageManager.Instance.pause) return;

        if (_target != null && Vector2.Distance(_target.transform.position, _center.transform.position) <= _attackDistance)
        {
            Attack();
        }
        else if(_target != null && Vector2.Distance(_target.transform.position, _center.transform.position) <= _attackDistance * 3.0f)
        {
            stomping();
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
            Patrol();
        }

        if (_delayCount < _attackDelay)
            _delayCount += Time.deltaTime;
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
        if (_isAttacking)
        {
            if (collision.gameObject.tag.Contains("Player"))
            {
                collision.gameObject.GetComponent<Player>().Damaged(_attackDamage * 2.0f);
                _isAttacking = false;
                if (_chargerCo != null) StopCoroutine(_chargerCo);
            }
        }
        else
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
            _normalAttackSE.Play();
            if (_target.transform.position.x - _center.transform.position.x > 0)
            {
                if (_reverseFlip) _pc.setFlip(true);
                else _pc.setFlip(false);
            }
            else
            {
                if (_reverseFlip) _pc.setFlip(false);
                else _pc.setFlip(true);
            }
            _player.Damaged(_attackDamage);
            _delayCount = 0.0f;
        }
        else
            _pc.MoveAnim(false, true, 0);
    }

    void stomping()
    {
        if (_delayCount >= _attackDelay)
        {
            _isAttacking = true;
            _preSE.Play();
            _pc.setReady();
            
            if (_chargerCo != null) StopCoroutine(_chargerCo);
            _chargerCo = StartCoroutine(charge());
        }
        else
            _pc.MoveAnim(false, true, 0.0f);
    }

    IEnumerator charge()
    {
        yield return new WaitForSeconds(2.0f);
        _chargeSE.Play();
        _pc.Stomp();
        if (_target.transform.position.x - _center.transform.position.x > 0)
        {
            if (_reverseFlip) _pc.setFlip(true);
            else _pc.setFlip(false);
            _rb.AddForce(Vector2.right * _sprintSpeed * 20.0f, ForceMode2D.Impulse);
        }
        else
        {
            if (_reverseFlip) _pc.setFlip(false);
            else _pc.setFlip(true);
            _rb.AddForce(Vector2.left * _sprintSpeed * 20.0f, ForceMode2D.Impulse);
        }

        yield return new WaitForSeconds(1.0f);
        _isAttacking = false;
        _delayCount = 0.0f;
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
        _isAttacking = false;
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
        _isAttacking = false;
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
}
