using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss3 : MonoBehaviour
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
    [SerializeField] float _lungeDistance = 6.0f;
    [SerializeField] float _attackDelay = 2.0f;
    [SerializeField] float _attackDamage = 2.0f;
    [SerializeField] float _stun = 2.0f;
    [SerializeField] AudioSource _hitSE = null;
    [SerializeField] AudioSource _painSE = null;
    [SerializeField] AudioSource _slashSE1 = null;
    [SerializeField] AudioSource _slashSE2 = null;
    [SerializeField] AudioSource _slashSE3 = null;
    [SerializeField] GameObject _meleeFx = null;
    float _delayCount = 0.0f;
    bool _jumpTrigger = false;
    bool _sprintTrigger = false;
    bool _isStun = false;
    bool _isDie = false;
    bool _isAttacking = false;
    Coroutine _sprintCo;
    Coroutine _stunCo;
    Coroutine _extraHitCo;
    Coroutine _extraHitCo2;
    Coroutine _jumpCo;

    private Rigidbody2D _rb = null;
    [SerializeField] Boss3_Anim _pc = null;
    Vector3 _originalPosition;

    [SerializeField] GameObject _target = null;
    Player _player = null;

    [SerializeField] GameObject _fireBall = null;
    [SerializeField] AudioSource _fireBallSE = null;
    [SerializeField] GameObject _spawner = null;

    public GameObject GetCenter() { return _center; }

    [SerializeField] float _MaxSuperArmor = 100.0f;
    float _superArmor = 100.0f;

    [SerializeField] GameObject _bossCanvas = null;
    [SerializeField] GameObject _hpSlider = null;
    [SerializeField] GameObject _superArmorSlider = null;
    Slider hpSlider = null;
    Slider superArmorSlider = null;
    [SerializeField] GameObject _superArmorFx = null;
    public float _superArmorMaden = 0.0f;
    Coroutine _superArmorCo = null;
    bool _superArmorBroken = false;
    [SerializeField] AudioSource _superArmorSE = null;

    [SerializeField] GameObject _clearCanvas = null;

    [SerializeField] Camera _camera = null;
    Coroutine _cutSceneCo = null;

    [SerializeField] int _bossID = 3;
    [SerializeField] GameObject _banner = null;

    void Start()
    {
        _hp = _maxHp;
        _superArmor = _MaxSuperArmor;
        _rb = this.GetComponent<Rigidbody2D>();
        _originalPosition = this.transform.position;
        direction = 1.0f;
        _delayCount = _attackDelay;
        if (_target != null)
            _player = _target.GetComponent<Player>();

        hpSlider = _hpSlider.GetComponent<Slider>();
        superArmorSlider = _superArmorSlider.GetComponent<Slider>();
        jumpCoolDown();
    }

    private void OnEnable()
    {
        _bossCanvas.SetActive(true);
        SoundManager.Instance.playBossBGM();

        if (_cutSceneCo != null) StopCoroutine(_cutSceneCo);
        _cutSceneCo = StartCoroutine(CutScene());
    }

    void Update()
    {
        if (_isDie) return;
        if (_isStun) return;
        if (_isAttacking) return;
        if (_jumpTrigger)
        {
            transform.Translate(direction * _walkSpeed * Time.deltaTime, 0.0f, 0.0f);
            return;
        }
        if (StageManager.Instance.pause) return;

        if (_target != null && Vector2.Distance(_target.transform.position, _center.transform.position) <= _lungeDistance)
        {
            _pc.Slide();
            Attack();
        }
        else if (_target != null && Vector2.Distance(_target.transform.position, _center.transform.position) <= _lungeDistance * 2.0f)
        {
            int rnd = Random.Range(0, 100);
            if (rnd < 50)
                stomping();
            else
                casting();
        }
        else if (_target != null && Vector2.Distance(_target.transform.position, _center.transform.position) <= _chaseRange)
        {
            _sprintTrigger = true;
            int rnd = Random.Range(0, 1000);
            if (rnd < 998 || _jumpTrigger)
                chasing();
            else
                jump();
        }
        else
        {
            _sprintTrigger = false;
            int rnd = Random.Range(0, 1000);
            if (rnd < 998 || _jumpTrigger)
                Patrol();
            else
                jump();
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
        if (dir > 0)
            direction = 1;
        else
            direction = -1;
        _pc.MoveAnim(_sprintTrigger, false, direction * _sprintSpeed);
        transform.Translate(direction * _sprintSpeed * Time.deltaTime, 0.0f, 0.0f);
    }

    private void Patrol()
    {
        if (_center.transform.position.x < _originalPosition.x - _patrolRange)
        {
            direction = 1;
            _pc.MoveAnim(_sprintTrigger, false, direction * _walkSpeed);
            Moving();
        }
        else if (_center.transform.position.x > _originalPosition.x + _patrolRange)
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

    IEnumerator jumpCoolDownIE()
    {
        yield return new WaitForSeconds(5.0f);

        jumpCoolDown();
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

    IEnumerator sprintTerm()
    {
        yield return new WaitForSeconds(0.5f);

        _sprintTrigger = true;
    }

    void Attack()
    {
        if (_delayCount >= _attackDelay)
        {
            _isAttacking = true;
            if (_target.transform.position.x - _center.transform.position.x > 0)
                _pc.setFlip(false);
            else
                _pc.setFlip(true);

            if (_extraHitCo != null) StopCoroutine(_extraHitCo);
            _extraHitCo = StartCoroutine(ExtraHit());
        }
        else
            _pc.Slide();
    }

    IEnumerator ExtraHit()
    {
        if (_target.transform.position.x - _center.transform.position.x > 0)
            _rb.AddForce(Vector3.right * _walkSpeed * 15.0f, ForceMode2D.Impulse);
        else
            _rb.AddForce(Vector3.left * _walkSpeed * 15.0f, ForceMode2D.Impulse);
        _pc.Attack();
        for (int j = 0; j < 3; ++j)
        {
            if (_isStun) break;

            yield return new WaitForSeconds(0.2f);

            if (j == 0) _slashSE1.Play();
            else if (j == 1) _slashSE2.Play();
            else _slashSE3.Play();
            if (Vector2.Distance(_target.transform.position, _center.transform.position) <= _attackDistance)
            {
                _player.Damaged(_attackDamage);
                GameObject gm = Instantiate(_meleeFx);
                gm.transform.position = _target.transform.position;
            }

        }


        yield return new WaitForSeconds(0.5f);
        _isAttacking = false;
        _delayCount = 0.0f;
    }

    void stomping()
    {
        if (_delayCount >= _attackDelay)
        {
            _isAttacking = true;

            _pc.Stomp();
            _slashSE2.Play();
            if (_target.transform.position.x - _center.transform.position.x > 0)
            {
                _rb.AddForce(Vector3.up * _jumpPower + Vector3.right * 30.0f, ForceMode2D.Impulse);
                _pc.setFlip(false);
            }
            else
            {
                _rb.AddForce(Vector3.up * _jumpPower + Vector3.left * 30.0f, ForceMode2D.Impulse);
                _pc.setFlip(true);
            }

            if (_extraHitCo2 != null) StopCoroutine(_extraHitCo2);
            _extraHitCo2 = StartCoroutine(ExtraHit2());
        }
    }

    IEnumerator ExtraHit2()
    {
        yield return new WaitForSeconds(0.2f);
        MakeSpreadBullets();
        _isAttacking = false;
        _delayCount = 0.0f;
    }

    void MakeSpreadBullets()
    {
        Vector3 _angle = Vector3.zero;
        Vector3 _dir = Vector3.right;
        for (int i = 0; i < 10; ++i)
        {
            switch (i)
            {
                case 0: _angle = new Vector3(0.0f, 0.0f, 75.0f); _dir = new Vector3(0.8f, 0.5f, 0.0f); break;
                case 1: _angle = new Vector3(0.0f, 0.0f, 35.0f); _dir = new Vector3(0.9f, 0.25f, 0.0f); break;
                case 2: _angle = new Vector3(0.0f, 0.0f, 0.0f); _dir = new Vector3(1.0f, 0.0f, 0.0f); break;
                case 3: _angle = new Vector3(0.0f, 0.0f, -35.0f); _dir = new Vector3(0.9f, -0.25f, 0.0f); break;
                case 4: _angle = new Vector3(0.0f, 0.0f, -75.0f); _dir = new Vector3(0.8f, -0.50f, 0.0f); break;
                case 5: _angle = new Vector3(0.0f, 0.0f, 75.0f); _dir = new Vector3(-0.8f, 0.5f, 0.0f); break;
                case 6: _angle = new Vector3(0.0f, 0.0f, 35.0f); _dir = new Vector3(-0.9f, 0.25f, 0.0f); break;
                case 7: _angle = new Vector3(0.0f, 0.0f, 0.0f); _dir = new Vector3(-1.0f, 0.0f, 0.0f); break;
                case 8: _angle = new Vector3(0.0f, 0.0f, -35.0f); _dir = new Vector3(-0.9f, -0.25f, 0.0f); break;
                case 9: _angle = new Vector3(0.0f, 0.0f, -75.0f); _dir = new Vector3(-0.8f, -0.5f, 0.0f); break;
            }

            GameObject gm = Instantiate(_fireBall);
            gm.GetComponent<Bullet_straight>().setDirection(_dir, _angle);
            gm.transform.position = _center.transform.position;
        }
    }

    void casting()
    {
        if (_delayCount >= _attackDelay)
        {
            _pc.Cast();
            _fireBallSE.Play();
            GameObject gm = Instantiate(_spawner);
            gm.GetComponent<Plunder>().SetTarget(_target);
            gm.transform.position = _center.transform.position;
            _delayCount = 0.0f;
        }
    }  

    public void Damaged(float value)
    {
        if (_isDie) return;

        _hitSE.Play();
        _painSE.Play();
        if (value < _superArmor)
        {
            _superArmor -= value;
            superArmorSlider.value = _superArmor;
        }
        else
        {
            if (_superArmor > 0)
            {
                value -= _superArmor;
                if (!_superArmorBroken)
                {
                    _superArmorCo = StartCoroutine(_retreatSuperArmor());
                    _superArmorBroken = true;
                }
            }

            _superArmor = 0.0f;
            _hp -= value;
            superArmorSlider.value = _superArmor;
            hpSlider.value = _hp;
        }

        if (_hp <= 0)
            Die();
        else
        {
            if (_superArmor <= 0)
            {
                _isStun = true;
                _pc.DamagedAnim();
                if (_stunCo != null) StopCoroutine(_stunCo);
                _stunCo = StartCoroutine("Stun");
            }
        }
    }

    IEnumerator _retreatSuperArmor()
    {
        yield return new WaitForSeconds(2.0f);

        _superArmorFx.SetActive(true);
        _superArmorSE.Play();
        Collider2D[] co = Physics2D.OverlapCircleAll(this.transform.position, _attackDistance);
        for (int i = 0; i < co.Length; ++i)
        {
            if (co[i].gameObject.tag.Contains("Player"))
            {
                co[i].gameObject.GetComponent<Player>().Damaged(_attackDamage);
                break;
            }
        }
        _superArmorMaden = _MaxSuperArmor;

        while (_superArmorMaden >= 0)
        {
            yield return new WaitForSeconds(0.1f);
            _superArmorMaden -= 10;
            _superArmor += 10;
            superArmorSlider.value = _superArmor;
        }
        _superArmorBroken = false;
        _superArmorFx.SetActive(false);
    }

    void Die()
    {
        _pc.DieAnim();
        _isDie = true;
        _isStun = false;
        _isAttacking = false;
        _jumpTrigger = false;
        StopAllCoroutines();

        StageManager.Instance.pause = true;
        StartCoroutine(DieProcess());
    }

    IEnumerator DieProcess()
    {
        Time.timeScale = 0.2f;

        yield return new WaitForSeconds(1.0f);

        Time.timeScale = 1.0f;
        _clearCanvas.SetActive(true);
        SoundManager.Instance.clearBGM();
        DataStreamToStage.Instance.SetClearData(_bossID);
        _clearCanvas.SetActive(true);
        StopAllCoroutines();
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
        _isAttacking = false;
        _jumpTrigger = false;
        _hp = _maxHp;
        _superArmor = _MaxSuperArmor;
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
        if (_superArmor > 0) return;
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

    IEnumerator CutScene()
    {
        StageManager.Instance.pause = true;
        if (_camera == null) _camera = Camera.main;
        _camera.GetComponent<billboard>().changeTarget(this.gameObject);

        yield return new WaitForSeconds(2.0f);

        _camera.GetComponent<billboard>().changeTarget(_target);
        _banner.SetActive(true);
    }
}
