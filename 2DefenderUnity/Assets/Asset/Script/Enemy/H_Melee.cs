using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_Melee : MonoBehaviour
{
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
    float _delayCount = 0.0f;
    bool _jumpTrigger = false;
    bool _sprintTrigger = false;
    bool _isStun = false;
    bool _isDie = false;
    Coroutine _sprintCo;
    Coroutine _stunCo;

    private Rigidbody2D _rb = null;
    [SerializeField] H_Melee_Anim _pc = null;
    Vector3 _originalPosition;

    [SerializeField] GameObject _target = null;
    Player _player = null;

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
        if (StageManager.Instance.pause) return;

        if (_target != null && Vector2.Distance(_target.transform.position, this.transform.position) <= _attackDistance)
        {
            if (_target.transform.position.y > this.transform.position.y)
                stomping();
            else
                Attack();
        }
        else if (_target != null && Vector2.Distance(_target.transform.position, this.transform.position) <= _chaseRange)
        {
            _sprintTrigger = true;
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
        if(!_jumpTrigger && rnd < 5)
            jump();
    }

    private void chasing()
    {
        float dir = _target.transform.position.x - this.transform.position.x;
        if (dir > 0)
            direction = 1;
        else
            direction = -1;
        _pc.MoveAnim(_sprintTrigger, false, direction * _sprintSpeed);
        transform.Translate(direction * _sprintSpeed * Time.deltaTime, 0.0f, 0.0f);
    }

    private void Patrol()
    {
        if (this.transform.position.x < _originalPosition.x - _patrolRange)
        {
            direction = 1;
            _pc.MoveAnim(_sprintTrigger, false, direction * _walkSpeed);
            Moving();
        }
        else if (this.transform.position.x > _originalPosition.x + _patrolRange)
        {
            direction = -1;
            _pc.MoveAnim(_sprintTrigger, false, direction * _walkSpeed);
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

        //if (collision.gameObject.tag.Contains("Enemy"))
            //jump();
    }

    private void jump()
    {
        if (!_jumpTrigger)
        {
            _jumpTrigger = true;
            _pc.jumpAnim();
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
            if(_sprintCo != null)
                StopCoroutine(_sprintCo);
            _sprintTrigger = false;
        }
    }

    void jumpCoolDown()
    {
        if(_jumpTrigger)
        {
            _jumpTrigger = false;
            _pc.jumpCoolDown();
        }
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
            if (_target.transform.position.x - this.transform.position.x > 0)
                _pc.setFlip(false);
            else
                _pc.setFlip(true);
            _player.Damaged(_attackDamage);
            _delayCount = 0.0f;
        }
        else
            _pc.MoveAnim(false, false, 0);
    }

    void stomping()
    {
        if (_delayCount >= _attackDelay)
        {
            _pc.Stomp();
            if (_target.transform.position.x - this.transform.position.x > 0)
                _pc.setFlip(false);
            else
                _pc.setFlip(true);
            _player.Damaged(_attackDamage);
            _delayCount = 0.0f;
        }
    }

    public void Damaged(float value)
    {
        if (_isDie) return;

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
        _hp = _maxHp;
    }

    public bool GetDead()
    {
        return _isDie;
    }

    IEnumerator Stun()
    {
        yield return new WaitForSeconds(_stun);

        if(!_isDie)
        {
            _isStun = false;
            _pc.MoveAnim(_sprintTrigger, false, _rb.velocity.x);
        }
    }
}
