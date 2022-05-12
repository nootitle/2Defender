using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss5 : MonoBehaviour
{
    [SerializeField] GameObject _center = null;
    [SerializeField] float _maxHp = 100.0f;
    float _hp = 100.0f;
    [SerializeField] float _walkSpeed = 5.0f;
    float direction = 0.0f;
    [SerializeField] float _attackDistance1 = 2.0f;
    [SerializeField] float _attackDistance2 = 6.0f;
    [SerializeField] float _attackDelay = 2.0f;
    [SerializeField] float _attackDamage = 2.0f;
    [SerializeField] float _stun = 2.0f;
    [SerializeField] AudioSource _hitSE = null;
    [SerializeField] AudioSource _painSE = null;
    [SerializeField] AudioSource _fireSE1 = null;
    [SerializeField] AudioSource _fireSE2 = null;
    [SerializeField] List<GameObject> _lightnings = null;
    [SerializeField] GameObject _throwPosition = null;
    float _delayCount = 0.0f;
    bool _isStun = false;
    bool _isDie = false;
    bool _isAttacking = false;
    Coroutine _stunCo;
    Coroutine _extraHitCo;
    Coroutine _extraHitCo2;
    Coroutine _extraHitCo3;

    private Rigidbody2D _rb = null;
    Vector3 _originalPosition;

    [SerializeField] GameObject _target = null;
    Player _player = null;

    [SerializeField] GameObject _grenade = null;

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
    [SerializeField] SpriteRenderer _shieldObject = null;

    [SerializeField] GameObject _clearCanvas = null;

    [SerializeField] Camera _camera = null;
    Coroutine _cutSceneCo = null;

    [SerializeField] int _bossID = 5;
    [SerializeField] GameObject _banner = null;

    bool _isAppear = false;

    void Start()
    {
        _hp = _maxHp;
        _superArmor = _MaxSuperArmor;
        _rb = this.GetComponent<Rigidbody2D>();
        _originalPosition = this.transform.position;
        direction = 1.0f;
        _delayCount = _attackDelay;
        if (_target == null)
            _target = GameObject.Find("Player");
        if (_target != null)
            _player = _target.GetComponent<Player>();

        hpSlider = _hpSlider.GetComponent<Slider>();
        superArmorSlider = _superArmorSlider.GetComponent<Slider>();
    }

    private void OnEnable()
    {
        _isAppear = true;
        _bossCanvas.SetActive(true);
        _shieldObject.color = new Color(_shieldObject.color.r,
            _shieldObject.color.g, _shieldObject.color.b, 0.0f);
        SoundManager.Instance.playBossBGM();
        Invoke("appearEnd", 4.0f);

        if (_cutSceneCo != null) StopCoroutine(_cutSceneCo);
        _cutSceneCo = StartCoroutine(CutScene());
    }

    void Update()
    {
        if (_isDie) return;
        if (_isAppear) return;
        if (_isStun) return;
        if (_isAttacking) return;
        if (StageManager.Instance.pause) return;

        if (_target != null && Vector2.Distance(_target.transform.position, _center.transform.position) <= _attackDistance1)
        {
            int rnd = Random.Range(0, 100);
            if (rnd < 50)
                Attack1();
            else
                LightningAttack();

        }
        else if (_target != null && Vector2.Distance(_target.transform.position, _center.transform.position) <= _attackDistance2)
        {
            int rnd = Random.Range(0, 100);
            if (rnd < 50)
                GrenadeAttack();
            else
                LightningAttack();
        }

        if (_delayCount < _attackDelay)
            _delayCount += Time.deltaTime;
    }

    void appearEnd()
    {
        _isAppear = false;
        _shieldObject.color = new Color(_shieldObject.color.r,
            _shieldObject.color.g, _shieldObject.color.b, 1.0f);
    }

    private void Moving()
    {
        if (_target.transform.position.x - _center.transform.position.x > 0)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
        transform.Translate(direction * _walkSpeed * Time.deltaTime, 0.0f, 0.0f);
    }

    void Attack1()
    {
        if (_delayCount >= _attackDelay)
        {
            _isAttacking = true;

            if (_extraHitCo != null) StopCoroutine(_extraHitCo);
            _extraHitCo = StartCoroutine(ExtraHit());
        }
    }

    IEnumerator ExtraHit()
    {
        yield return new WaitForSeconds(1.0f);

        _isAttacking = false;
        _delayCount = 0.0f;
    }

    void GrenadeAttack()
    {
        if (_delayCount >= _attackDelay)
        {
            _isAttacking = true;
            _fireSE1.Play();

            if (_extraHitCo2 != null) StopCoroutine(_extraHitCo2);
            _extraHitCo2 = StartCoroutine(ExtraHit2());
        }
    }

    IEnumerator ExtraHit2()
    {
        yield return new WaitForSeconds(0.8f);

        _fireSE2.Play();
        for (int i = 0; i < 3; ++i)
        {
            GameObject gm = Instantiate(_grenade);
            gm.transform.position = _throwPosition.transform.position;
            Rigidbody2D _gmRb = gm.GetComponent<Rigidbody2D>();

            if (_target.transform.position.x - _center.transform.position.x > 0)
            {
                _gmRb.AddForce(Vector3.up * (2.0f * i) + Vector3.right * 30.0f, ForceMode2D.Impulse);
            }
            else
            {
                _gmRb.AddForce(Vector3.up * (2.0f * i) + Vector3.left * 30.0f, ForceMode2D.Impulse);
            }
        }


        _isAttacking = false;
        _delayCount = 0.0f;
    }

    void LightningAttack()
    {
        if (_delayCount >= _attackDelay)
        {
            _isAttacking = true;
            if (_extraHitCo3 != null) StopCoroutine(_extraHitCo3);
            _extraHitCo3 = StartCoroutine(ExtraHit3());
        }
    }

    IEnumerator ExtraHit3()
    {
        for (int j = 0; j < _lightnings.Count; ++j)
        {
            if (_isStun) break;

            yield return new WaitForSeconds(0.2f);
            _fireSE1.Play();
            _fireSE2.Play();

            _lightnings[j].SetActive(true);
            _lightnings[j].GetComponent<Lightning>().fire(_center);
        }

        yield return new WaitForSeconds(0.5f);
        _isAttacking = false;
        _delayCount = 0.0f;
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
            _shieldObject.color = new Color(_shieldObject.color.r,
                _shieldObject.color.g, _shieldObject.color.b, _superArmor / _MaxSuperArmor);
        }
        else
        {
            if (_superArmor > 0)
            {
                value -= _superArmor;
                _shieldObject.color = new Color(_shieldObject.color.r,
                    _shieldObject.color.g, _shieldObject.color.b, _superArmor / _MaxSuperArmor);

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
                if (_stunCo != null) StopCoroutine(_stunCo);
                _stunCo = StartCoroutine("Stun");
            }
        }
    }

    IEnumerator _retreatSuperArmor()
    {
        yield return new WaitForSeconds(5.0f);

        _superArmorFx.SetActive(true);
        _superArmorSE.Play();
        Collider2D[] co = Physics2D.OverlapCircleAll(this.transform.position, _attackDistance1);
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
            _shieldObject.color = new Color(_shieldObject.color.r,
                _shieldObject.color.g, _shieldObject.color.b, _superArmor / _MaxSuperArmor);
        }
        _superArmorBroken = false;
        _superArmorFx.SetActive(false);
    }

    void Die()
    {
        _isDie = true;
        _isStun = false;
        _isAttacking = false;
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
        }
    }

    IEnumerator CutScene()
    {
        StageManager.Instance.pause = true;
        if (_camera == null) _camera = Camera.main;
        _camera.GetComponent<billboard>().changeTarget(this.gameObject);

        yield return new WaitForSeconds(4.0f);

        _camera.GetComponent<billboard>().changeTarget(_target);
        _banner.SetActive(true);
    }
}
