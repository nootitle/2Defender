using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //캐릭터 기본 기능

    [SerializeField] private GameObject SkillList;
    [SerializeField] float _jumpPower = 5.0f;
    [SerializeField] float _sprintSpeed = 10.0f;
    [SerializeField] float _mapInterval = 30.0f;
    [SerializeField] float _maxHp = 100.0f;
    public float _hp = 100.0f;
    [SerializeField] float _attackDistance = 2.0f;
    [SerializeField] float _attackDelay = 2.0f;
    [SerializeField] float _attackDamage = 2.0f;
    [SerializeField] float _stun = 2.0f;
    [SerializeField] GameObject _stunFx = null;
    [SerializeField] public List<int> _SkillLevelList = null;
    Coroutine _stunCo = null;
    Coroutine _extraAttackCo = null;
    float _magicExtraDamage = 0.0f;
    public float _extraHeal = 0.0f;
    float _delayCount = 0.0f;

    bool _jumpTrigger = false;
    bool _sprintTrigger = false;
    bool _isDie = false;
    bool _isStun = false;

    private bool skillListCheck;
    private Rigidbody2D _rb = null;

    [SerializeField] GameObject _bg = null;
    [SerializeField] bool _bgExtension = false;
    private float pointX = 0;
    [SerializeField] PlayerController _pc = null;
    [SerializeField] GameObject _normalMode = null;
    [SerializeField] GameObject _swordMode = null;
    int _modeID = 0;
    [SerializeField] FootStepSE _se = null;
    [SerializeField] MeleeSE _se_Melee = null;
    [SerializeField] DamagedSE _se_damaged = null;
    [SerializeField] AudioSource _swordFallSE = null;

    //UI

    [SerializeField] GameObject _hpBar = null;
    [SerializeField] GameObject _skillSlot = null;
    [SerializeField] Joystick _joyStick = null;
    [SerializeField] bool _joyStickMode = true;

    //스킬 프리펩
    [SerializeField] GameObject _fireBall = null;

    [SerializeField] GameObject _healFx = null;

    [SerializeField] GameObject _bulletsObject = null;
    Coroutine _spreadProcessCo = null;

    [SerializeField] GameObject _throwingStoneObject = null;

    [SerializeField] GameObject _sharpShooterFx = null;
    [SerializeField] float _sharpShooterDuration = 5.0f;
    [SerializeField] float _magicExtraDamage_sharpShooter = 5.0f;
    Coroutine _sharpShooterCo = null;

    [SerializeField] List<GameObject> _trap = null;

    [SerializeField] List<GameObject> _trackingBullets = null;
    [SerializeField] GameObject _trackingCastFx = null;
    [SerializeField] float _trackingAreaRadius = 5.0f;

    [SerializeField] GameObject _shieldBar = null;
    [SerializeField] GameObject _shieldFx = null;
    [SerializeField] AudioSource _shieldSE = null;
    [SerializeField] float _shieldMax = 500.0f;
    float _shield = 0.0f;

    [SerializeField] float _durationSliding = 2.0f;
    [SerializeField] AudioSource _slidingSE = null;
    Coroutine _slidingCo = null;
    bool _isSliding = false;
    string currentID = "";

    [SerializeField] GameObject _gameOverCanvas = null;
    [SerializeField] GameObject _escMenuCanvas = null;

    private void Awake()
    {
        List<string> statusList = new List<string>();

        if (DataStreamToStage.Instance != null)
        {
            string id = DataStreamToStage.Instance.getID();

            string stream = PlayerPrefs.GetString(id + "PlayerSetting");
            currentID = id;

            string temp = "";
            for (int i = 0; i < stream.Length; ++i)
            {
                if (stream[i] == ' ')
                {
                    statusList.Add(temp);
                    temp = "";
                }
                else
                    temp += stream[i];
            }

            _maxHp = float.Parse(statusList[0]);
            _hpBar.GetComponent<HpBar>().MaxHpBarInit(_maxHp);
            _hpBar.GetComponent<HpBar>().setHpBar(_maxHp);
            _attackDamage = float.Parse(statusList[1]);
            _magicExtraDamage = float.Parse(statusList[2]);
            _extraHeal = float.Parse(statusList[3]);
            if (statusList[4] == "0")
                _modeID = 0;
            else if (statusList[4] == "1")
            {
                useSwordMode();
            }
            StorageManager.Instance.setGold(int.Parse(statusList[5]));
        }
       
    }

    void Start()
    {
        skillListCheck = false;
        SkillList.SetActive(false);
        _rb = this.GetComponent<Rigidbody2D>();
        _hp = _maxHp;

        if(_SkillLevelList.Count == 0)
        {
            for (int i = 0; i <= Skill_Info.Instance._iconSource.Count; ++i)
                _SkillLevelList.Add(1);
        }
    }

    void Update()
    {
        if (_isDie) return;
        if (_isStun) return;
 
        if (StageManager.Instance.pause)
        {
            jumpCoolDown();
            _pc.MoveForJoyStick(0.0f);
            return;
        }

        float Hor;
        if(_joyStickMode)
        {
            float Joy = _joyStick.GetSpeed();
            if (Joy < 0)
                Hor = -0.8f;
            else if (Joy > 0)
                Hor = 0.8f;
            else
                Hor = 0.0f;
        }
        else
        {
            Hor = Input.GetAxis("Horizontal");
        }

        //클릭으로 공격, 스킬 사용하는 부분은 SkillSlot들을 찾아갈 것
        //기본공격(키보드) 
        if (Input.GetKeyDown(KeyCode.F))
        {
            _skillSlot.transform.GetChild(0).GetComponent<SkillSlotController>().StartCooldown();
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            _skillSlot.transform.GetChild(1).GetComponent<SkillSlotController>().StartCooldown();
            stomping();
        }

        if (!_joyStickMode)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                jump();
            if (Input.GetKey(KeyCode.LeftShift))
                sprint();
            else
                sprintStop();
        }

        if (Hor >= 0.2f || Hor <= -0.2f)
        {
            if (!_jumpTrigger && !_sprintTrigger)
                _se.PlayWalk();
            else if (!_jumpTrigger && _sprintTrigger)
                _se.PlayRun();
        }
          
        if(!_sprintTrigger)
            transform.Translate(Hor * 5.0f * Time.deltaTime, 0.0f, 0.0f);
        else
            transform.Translate(Hor * _sprintSpeed * Time.deltaTime, 0.0f, 0.0f);
        if (_joyStickMode) _pc.MoveForJoyStick(Hor);

        if(_bgExtension)
        {
            if (transform.position.x > pointX - _mapInterval)
            {
                GameObject Obj = Instantiate(_bg);
                Vector3 vPos = new Vector3(pointX + _mapInterval, 0.0f, 0.0f);
                pointX += _mapInterval;
                Obj.transform.position = vPos;
            }
        }

        //스킬(키보드)
        /*
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _skillSlot.transform.GetChild(2).GetComponent<SkillSlotController>().StartCooldown();
            fireBall();
        }
        */

        if (Input.GetKeyDown(KeyCode.Escape))
            CallEsc();

        if (_delayCount < _attackDelay)
            _delayCount += Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        jumpCoolDown();

        if(_isSliding)
        {
            if(collision.transform.gameObject.tag.Contains("Enemy"))
            {
                Enemy_Hit EH = collision.gameObject.GetComponent<Enemy_Hit>();
                if (EH != null)
                {
                    _slidingSE.Play();
                    EH.Hit(_attackDamage * _SkillLevelList[10]);
                }
            }
        }
    }

    void CallEsc()
    {
        SoundManager.Instance.clickSE();
        if (_escMenuCanvas.activeSelf)
            _escMenuCanvas.SetActive(false);
        else
            _escMenuCanvas.SetActive(true);
    }

    public void SkillListInvisible()
    {
        skillListCheck = !skillListCheck;
        SkillList.SetActive(skillListCheck);
    }

    public void jump()
    {
        if (!_jumpTrigger)
        {
            _se.PlayJump();
            _jumpTrigger = true;
            _rb.AddForce(Vector3.up * _jumpPower, ForceMode2D.Impulse);
        }
    }

    private void sprint()
    {
        if (!_sprintTrigger)
            _sprintTrigger = true;
    }

    private void sprintStop()
    {
        if (_sprintTrigger)
            _sprintTrigger = false;
    }

    public void sprintForButton()
    {
        if (_sprintTrigger)
            sprintStop();
        else
            sprint();
    }

    void jumpCoolDown()
    {
        if(_jumpTrigger)
        {
            _jumpTrigger = false;
            _pc.jumpCoolDown();
            _se.PlayLand();
        }
    }

    public void Attack()
    {
        if (_delayCount >= _attackDelay)
        {
            _pc.Attack();
            _se_Melee.bladeSE();
            if(_modeID == 0)
                _delayCount = 0.0f;
            Collider2D[] co = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), _attackDistance);

            foreach (Collider2D c in co)
            {
                Enemy_Hit EH = c.gameObject.GetComponent<Enemy_Hit>();
                if (EH != null)
                    EH.Hit(_attackDamage * _SkillLevelList[0]);
            }

            if (_modeID == 1)
            {
                if (_extraAttackCo != null) StopCoroutine(_extraAttackCo);
                _extraAttackCo = StartCoroutine(extraAttack());
            }
        }
    }

    IEnumerator extraAttack()
    {
        yield return new WaitForSeconds(0.1f);

        _se_Melee.bladeSE();
        _delayCount = 0.0f;
        Collider2D[] co = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), _attackDistance);

        foreach (Collider2D c in co)
        {
            Enemy_Hit EH = c.gameObject.GetComponent<Enemy_Hit>();
            if (EH != null)
                EH.Hit(_attackDamage * _SkillLevelList[0]);
        }
    }

    public void stomping()
    {
        if (_delayCount >= _attackDelay)
        {
            _pc.Stomp();
            _se_Melee.bladeSE();
            _delayCount = 0.0f;

            Collider2D[] co = Physics2D.OverlapCircleAll(new Vector2(transform.position.x - 1.0f, transform.position.y + 2.0f), _attackDistance);

            foreach (Collider2D c in co)
            {
                Enemy_Hit EH = c.gameObject.GetComponent<Enemy_Hit>();
                if (EH != null)
                {
                    if (_modeID == 0)
                        EH.Hit(_attackDamage * 1.5f * _SkillLevelList[1]);
                    else if(_modeID == 1)
                        EH.Hit(_attackDamage * 2.0f * _SkillLevelList[1]);
                }

            }
        }
    }

    public void Damaged(float value)
    {
        if (_isDie) return;
        if (_isSliding) return;
        if (StageManager.Instance.pause) return;

        float temp = value;
        if(_shield > 0)
            _shieldSE.Play();

        if (_shield >= temp)
        {
            _shield -= temp;
            _shieldBar.GetComponent<HpBar>().setHpBar(_shield);
            return;
        }
        else
        {
            temp -= _shield;
            _shieldBar.GetComponent<HpBar>().setHpBar(_shield);
            _hp -= value;
        }

        if (_shield <= 0)
            _shieldFx.SetActive(false);

        _pc.DamagedAnim();
        if (_hpBar != null)
            _hpBar.GetComponent<HpBar>().setHpBar(_hp);
        _se_damaged.PlayDamaged();
        if (_hp <= 0)
            Die();
    }

    public void StunStart()
    {
        if (!_isDie && !_isStun)
        {
            _isStun = true;
            if (_stunCo != null) StopCoroutine(_stunCo);
            _stunFx.SetActive(true);
            _stunCo = StartCoroutine(Stun());
        }
    }

    public void healed(float value)
    {
        if (_isDie) return;

        float temp = Mathf.Min(_hp + value, _maxHp);
        _hp = temp;
        if (_hpBar != null)
            _hpBar.GetComponent<HpBar>().setHpBar(temp);
    }

    IEnumerator Stun()
    {
        yield return new WaitForSeconds(_stun);

        if (!_isDie)
        {
            _isStun = false;
            _stunFx.SetActive(false);
            _pc.MoveForJoyStick(0.0f);
        }
    }

    void Die()
    {
        StageManager.Instance.offRewardWindow();
        _hpBar.GetComponent<HpBar>().setHpBar(0);
        _pc.DieAnim();
        _isDie = true;
        _swordFallSE.Play();
        updateUserData();
        _gameOverCanvas.SetActive(true);
        SoundManager.Instance.defeatBGM();
    }

    public void updateUserData()
    {
        int gold = StorageManager.Instance.getGold();
        PlayerPrefs.SetString(currentID + "PlayerSetting", _maxHp.ToString() + " " +
            _attackDamage.ToString() + " " + _magicExtraDamage.ToString() + " " +
            _extraHeal.ToString() + " " + _modeID.ToString() + " " + gold.ToString() + " ");
    }

    //스킬 작업중

    public void setSkillLevel(int id, int level)
    {
        if (id == -1)
            _SkillLevelList[id] = 1;
        else
            _SkillLevelList[id] += level;
    }

    public void fireBall()
    {
        if (_fireBall != null)
        {
            _pc.Attack();
            GameObject gm = Instantiate(_fireBall);
            gm.GetComponent<Bullet_straight>().setAlies(true);
            gm.GetComponent<Bullet_straight>().setExtraDamage(_SkillLevelList[2]);
            if (_magicExtraDamage > 0)
                gm.GetComponent<Bullet_straight>().addExtraDamage(_magicExtraDamage);
            if (_pc.flip)
            {
                gm.GetComponent<Bullet_straight>().setDirection(Vector3.left);
                gm.transform.position = this.transform.position + Vector3.left;
            }
            else
            {
                gm.GetComponent<Bullet_straight>().setDirection(Vector3.right);
                gm.transform.position = this.transform.position + Vector3.right;
            }
        }
    }

    public void heal()
    {
        if (_healFx != null)
        {
            healed(5.0f * _SkillLevelList[3] + _extraHeal);
            GameObject gm = Instantiate(_healFx);
        }
    }

    public void spreadBullets()
    {
        if (_bulletsObject != null)
        {
            _pc.Attack();
            if (_spreadProcessCo != null) StopCoroutine(_spreadProcessCo);
            _spreadProcessCo = StartCoroutine(spreadBulletProcess());
        }
    }

    IEnumerator spreadBulletProcess()
    {
        for(int i = 0; i < 3; ++i)
        {
            MakeSpreadBullets();
            yield return new WaitForSeconds(0.2f);
        }
    }

    void MakeSpreadBullets()
    {
        Vector3 _angle = Vector3.zero;
        Vector3 _dir = Vector3.right;
        for (int i = 0; i < 5; ++i)
        {
            switch(i)
            {
                case 0: _angle = new Vector3(0.0f, 0.0f, 75.0f); _dir = new Vector3(0.8f, 0.2f, 0.0f); break;
                case 1: _angle = new Vector3(0.0f, 0.0f, 35.0f); _dir = new Vector3(0.9f, 0.1f, 0.0f); break;
                case 2: _angle = new Vector3(0.0f, 0.0f, 0.0f); _dir = new Vector3(1.0f, 0.0f, 0.0f); break;
                case 3: _angle = new Vector3(0.0f, 0.0f, -35.0f); _dir = new Vector3(0.9f, -0.1f, 0.0f); break;
                case 4: _angle = new Vector3(0.0f, 0.0f, -75.0f); _dir = new Vector3(0.8f, -0.2f, 0.0f); break;
            }

            GameObject gm = Instantiate(_bulletsObject);
            gm.GetComponent<Bullet_straight>().setAlies(true);
            gm.GetComponent<Bullet_straight>().setExtraDamage(_SkillLevelList[4]);
            if (_magicExtraDamage > 0)
                gm.GetComponent<Bullet_straight>().addExtraDamage(_magicExtraDamage);
            if (_pc.flip)
            {
                gm.GetComponent<Bullet_straight>().setDirection(-_dir, -_angle);
                gm.transform.position = this.transform.position + Vector3.left;
            }
            else
            {
                gm.GetComponent<Bullet_straight>().setDirection(_dir, _angle);
                gm.transform.position = this.transform.position + Vector3.right;
            }
        }
    }

    public void ThrowingStone()
    {
        if (_throwingStoneObject != null)
        {
            _pc.Attack();
            GameObject gm = Instantiate(_throwingStoneObject);
            gm.GetComponent<Bullet_straight>().setAlies(true);
            gm.GetComponent<Bullet_straight>().setExtraDamage(_SkillLevelList[5]);
            if (_magicExtraDamage > 0)
                gm.GetComponent<Bullet_straight>().addExtraDamage(_magicExtraDamage);
            if (_pc.flip)
            {
                gm.GetComponent<Bullet_straight>().setDirection(Vector3.left, new Vector3(0.0f, 0.0f, -180.0f));
                gm.transform.position = this.transform.position + Vector3.left;
            }
            else
            {
                gm.GetComponent<Bullet_straight>().setDirection(Vector3.right);
                gm.transform.position = this.transform.position + Vector3.right;
            }
            
        }
    }

    public void sharpShooter()
    {
        if(_sharpShooterFx != null)
        {
            if (_sharpShooterCo != null) 
                StopCoroutine(_sharpShooterCo);

            _pc.Stomp();
            _sharpShooterFx.SetActive(true);
            _magicExtraDamage += _magicExtraDamage_sharpShooter * _SkillLevelList[6];
            _sharpShooterCo = StartCoroutine(sharpShooterCoolDown());
        }
    }

    IEnumerator sharpShooterCoolDown()
    {
        yield return new WaitForSeconds(_sharpShooterDuration);

        _sharpShooterFx.SetActive(false);
        _magicExtraDamage -= _magicExtraDamage_sharpShooter * _SkillLevelList[6];
    }

    //이하 스킬은 추가 해금되는 스킬

    public void setTrap()
    {
        for (int i = 0; i < _trap.Count; ++i)
        {
            if(!_trap[i].activeSelf)
            {
                _trap[i].SetActive(true);
                _trap[i].GetComponent<BearTrap>().UpgradeDuration(_SkillLevelList[7]);
                if(_pc.flip)
                    _trap[i].transform.position = this.transform.position + new Vector3(-3.0f, 3.0f, 0.0f);
                else
                    _trap[i].transform.position = this.transform.position + new Vector3(3.0f, 3.0f, 0.0f);
                break;
            }
        }
    }

    public void trackingBullet()
    {
        GameObject gm = Instantiate(_trackingCastFx);
        gm.transform.position = this.transform.position;
        Collider2D[] co = Physics2D.OverlapCircleAll(this.transform.position, _trackingAreaRadius);
        int idx = 0;

        foreach (Collider2D c in co)
        {
            if (c.transform.gameObject.tag.Contains("Enemy"))
            {
                if (!_trackingBullets[idx].activeSelf)
                {
                    _trackingBullets[idx].SetActive(true);
                    _trackingBullets[idx].GetComponent<TrackingBullet>().Upgrade(_SkillLevelList[8]);
                    _trackingBullets[idx].GetComponent<TrackingBullet>().startTracking(c.transform.gameObject);

                }
                ++idx;
            }
            if (idx == _trackingBullets.Count)
                break;
        }
    }

    public void MakeShield()
    {
        _shieldSE.Play();
        _shieldFx.SetActive(true);
        _shield = _shieldMax * _SkillLevelList[9];
        _shieldBar.GetComponent<HpBar>().setHpBar(_shield);
    }

    public void Sliding()
    {
        _pc.slidingAnim();
        _isSliding = true;
        if (_slidingCo != null) StopCoroutine(_slidingCo);
        _slidingCo = StartCoroutine(ExitSliding());
    }

    IEnumerator ExitSliding()
    {
        yield return new WaitForSeconds(_durationSliding);

        _isSliding = false;
    }

    //기본공격, 스킬 통합 호출 함수

    public void CallSkill(int id)
    {
        if (_isDie) return;

        switch(id)
        {
            case 0: Attack(); break;
            case 1: stomping(); break;
            case 2: fireBall(); break;
            case 3: heal(); break;
            case 4: spreadBullets(); break;
            case 5: ThrowingStone(); break;
            case 6: sharpShooter(); break;
            case 7: setTrap(); break;
            case 8: trackingBullet(); break;
            case 9: MakeShield(); break;
            case 10: Sliding(); break;
        }
    }

    //패시브

    public void setMaxHp(float value)
    {
        _maxHp += value;
        _hpBar.GetComponent<HpBar>().setMaxHpBar(5.0f);
    }

    public void UpgradeMelee(float value)
    {
        _attackDamage += value;
    }

    public void UpgradeMagic(float value)
    {
        _magicExtraDamage += value;
    }

    public void UpgradeHeal(float value)
    {
        _extraHeal += value;
    }

    public void useSwordMode()
    {
        if(_modeID != 1)
        {
            _swordMode.SetActive(true);
            _normalMode.SetActive(false);
            _pc = _swordMode.GetComponent<PlayerController>();
            _attackDistance += 1.0f;
            _modeID = 1;
        }
        else
        {
            _swordMode.SetActive(false);
            _normalMode.SetActive(true);
            _pc = _normalMode.GetComponent<PlayerController>();
            _attackDistance -= 1.0f;
            _modeID = 0;
        }
    }

    public void AddNewSkillOnList(int id)
    {
        Skill_Info.Instance.AddNewSkillOnList(id);
    }
}
