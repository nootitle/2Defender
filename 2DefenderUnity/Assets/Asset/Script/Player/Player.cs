using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //캐릭터 기본 기능

    [SerializeField] private GameObject SkillList;
    [SerializeField] float _jumpPower = 5.0f;
    [SerializeField] float _sprintSpeed = 10.0f;
    [SerializeField] float _mapInterval = 30.0f;
    [SerializeField] float _maxHp = 100.0f;
    float _hp = 100.0f;
    [SerializeField] float _attackDistance = 2.0f;
    [SerializeField] float _attackDelay = 2.0f;
    [SerializeField] float _attackDamage = 2.0f;
    float _delayCount = 0.0f;

    bool _jumpTrigger = false;
    bool _sprintTrigger = false;
    bool _isDie = false;

    private bool skillListCheck;
    private Rigidbody2D _rb = null;

    [SerializeField] GameObject _bg = null;
    [SerializeField] bool _bgExtension = false;
    private float pointX = 0;
    [SerializeField] PlayerController _pc = null;
    [SerializeField] FootStepSE _se = null;
    [SerializeField] MeleeSE _se_Melee = null;
    [SerializeField] DamagedSE _se_damaged = null;

    //UI

    [SerializeField] GameObject _hpBar = null;
    [SerializeField] GameObject _skillSlot = null;
    [SerializeField] Joystick _joyStick = null;
    [SerializeField] bool _joyStickMode = true;

    //스킬 프리펩
    [SerializeField] GameObject _fireBall = null;
    float _fireBallDelay = 0.5f;
    float fireBallDelay = 0.0f;

    [SerializeField] GameObject _healFx = null;
    float _healDelay = 0.5f;
    float healDelay = 0.0f;

    [SerializeField] GameObject _bulletsObject = null;
    float _bullets = 0.5f;
    float bullets = 0.0f;
    Coroutine _spreadProcessCo = null;

    [SerializeField] GameObject _throwingStoneObject = null;
    float _throwingStone = 0.5f;
    float throwingStone = 0.0f;

    void Start()
    {
        skillListCheck = false;
        SkillList.SetActive(false);
        _rb = this.GetComponent<Rigidbody2D>();
        _hp = _maxHp;

        _fireBallDelay = Skill_Info.Instance._coolTime[2];
        _healDelay = Skill_Info.Instance._coolTime[3];
        _bullets = Skill_Info.Instance._coolTime[4];
        _throwingStone = Skill_Info.Instance._coolTime[5];
    }

    void Update()
    {
        if (_isDie) return;
        if (StageManager.Instance.pause) return;

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

        if (Input.GetKeyDown(KeyCode.Tab))
            SkillListInvisible();

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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _skillSlot.transform.GetChild(2).GetComponent<SkillSlotController>().StartCooldown();
            fireBall();
        }

        if (_delayCount < _attackDelay)
            _delayCount += Time.deltaTime;
        updateSkillCoolDown();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        jumpCoolDown();
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
            _delayCount = 0.0f;
            Collider2D[] co = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), _attackDistance);

            foreach (Collider2D c in co)
            {
                Enemy_Hit EH = c.gameObject.GetComponent<Enemy_Hit>();
                if (EH != null)
                    EH.Hit(_attackDamage);
            }
        }
    }

    public void stomping()
    {
        if (_delayCount >= _attackDelay)
        {
            _pc.Stomp();
            _se_Melee.bladeSE();
            _delayCount = 0.0f;

            Collider2D[] co = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y + 1.0f), _attackDistance);

            foreach (Collider2D c in co)
            {
                Enemy_Hit EH = c.gameObject.GetComponent<Enemy_Hit>();
                if (EH != null)
                    EH.Hit(_attackDamage * 1.5f);
            }
        }
    }

    public void Damaged(float value)
    {
        if (_isDie) return;

        _pc.DamagedAnim();
        _hp -= value;
        if (_hpBar != null)
            _hpBar.GetComponent<HpBar>().setHpBar(-value);
        _se_damaged.PlayDamaged();
        if (_hp <= 0)
            Die();
    }

    public void healed(float value)
    {
        if (_isDie) return;

        float temp = Mathf.Min(_hp + value, _maxHp);
        _hp += temp;
        if (_hpBar != null)
            _hpBar.GetComponent<HpBar>().setHpBar(temp);
    }

    void Die()
    {
        _pc.DieAnim();
        _isDie = true;
    }

    //스킬 작업중

    void updateSkillCoolDown()
    {
        if (fireBallDelay < _fireBallDelay)
            fireBallDelay += Time.deltaTime;
        if (healDelay < _healDelay)
            healDelay += Time.deltaTime;
        if (bullets < _bullets)
            bullets += Time.deltaTime;
        if (throwingStone < _throwingStone)
            throwingStone += Time.deltaTime;
    }

    public void GetSkill(int id)
    {
        Skill_Info.Instance.AddSkill(id);
    }

    public void fireBall()
    {
        if (_fireBall != null)
        {
            if (fireBallDelay >= _fireBallDelay)
            {
                _pc.Attack();
                GameObject gm = Instantiate(_fireBall);
                gm.GetComponent<Bullet_straight>().setAlies(true);
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
                fireBallDelay = 0.0f;
            }
        }
    }

    public void heal()
    {
        if (_healFx != null)
        {
            if (healDelay >= _healDelay)
            {
                healed(5.0f);
                GameObject gm = Instantiate(_healFx);
                healDelay = 0.0f;
            }
        }
    }

    public void spreadBullets()
    {
        if (_bulletsObject != null)
        {
            if (bullets >= _bullets)
            {
                _pc.Attack();
                if (_spreadProcessCo != null) StopCoroutine(_spreadProcessCo);
                _spreadProcessCo = StartCoroutine(spreadBulletProcess());              
                bullets = 0.0f;
            }
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
            if (throwingStone >= _throwingStone)
            {
                _pc.Attack();
                GameObject gm = Instantiate(_throwingStoneObject);
                gm.GetComponent<Bullet_straight>().setAlies(true);
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
                throwingStone = 0.0f;
            }
        }
    }

    //기본공격, 스킬 통합 호출 함수

    public void CallSkill(int id)
    {
        switch(id)
        {
            case 0: Attack(); break;
            case 1: stomping(); break;
            case 2: fireBall(); break;
            case 3: heal(); break;
            case 4: spreadBullets(); break;
            case 5: ThrowingStone(); break;
        }
    }
}
