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
    Coroutine _sprintCo;

    private bool skillListCheck;
    private Rigidbody2D _rb = null;

    [SerializeField] GameObject _bg = null;
    private float pointX = 0;
    [SerializeField] PlayerController _pc = null;
    [SerializeField] FootStepSE _se = null;
    [SerializeField] MeleeSE _se_Melee = null;
    [SerializeField] DamagedSE _se_damaged = null;

    //스킬 프리펩

    [SerializeField] GameObject _fireBall = null;

    void Start()
    {
        skillListCheck = false;
        SkillList.SetActive(false);
        _rb = this.GetComponent<Rigidbody2D>();
        _hp = _maxHp;
    }

    void Update()
    {
        float Hor = Input.GetAxis("Horizontal");
        //float Ver = Input.GetAxis("Vertical");
        //GetAxisRaw : -1, 0, 1만 출력

        Attack();
        jump();
        sprint();

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
        //Camera.main.GetComponent<CameraController>().setX(Hor * 0.025f * Time.deltaTime);
        //float xlimit = Camera.main.transform.position.x +
        //Camera.main.aspect * Camera.main.orthographicSize;
        //Vector2 limit = Camera.main.GetComponent<CameraController>().GetCameraEdge(0);

        if (Input.GetKeyDown(KeyCode.Tab))
            SkillListInvisible();

        if (transform.position.x > pointX - _mapInterval)
        {
            GameObject Obj = Instantiate(_bg);
            Vector3 vPos = new Vector3(pointX + _mapInterval, 0.0f, 0.0f);
            pointX += _mapInterval;
            Obj.transform.position = vPos;
        }

        //스킬 실험
        fireBall();

        if (_delayCount < _attackDelay)
            _delayCount += Time.deltaTime;


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

    private void jump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(!_jumpTrigger)
            {
                _se.PlayJump();
                _jumpTrigger = true;
                _rb.AddForce(Vector3.up * _jumpPower, ForceMode2D.Impulse);
            }
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
            _se.PlayLand();
        }
    }

    IEnumerator sprintTerm()
    {
        yield return new WaitForSeconds(0.5f);

        _sprintTrigger = true;
    }

    void Attack()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if (_delayCount >= _attackDelay)
            {
                _pc.Attack();
                _se_Melee.bladeSE();
                _delayCount = 0.0f;
                Collider2D[] co = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), _attackDistance);

                foreach (Collider2D c in co)
                {
                    H_Melee HM = c.gameObject.GetComponent<H_Melee>();
                    if (HM != null)
                        HM.Damaged(_attackDamage);
                }
            }
        }
    }

    public void Damaged(float value)
    {
        _pc.DamagedAnim();
        _hp -= value;
        _se_damaged.PlayDamaged();
        Debug.Log(_hp);
    }

    //스킬 작업중

    void fireBall()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(_fireBall != null)
            {
                if (_delayCount >= _attackDelay)
                {
                    _pc.Attack();
                    GameObject gm = Instantiate(_fireBall);
                    gm.GetComponent<Bullet_straight>().setAlies(true);
                    if(_pc.flip)
                    {
                        gm.GetComponent<Bullet_straight>().setDirection(Vector3.left);
                        gm.transform.position = this.transform.position + Vector3.up * 2.0f + Vector3.left * 2.0f;
                    }
                    else
                    {
                        gm.GetComponent<Bullet_straight>().setDirection(Vector3.right);
                        gm.transform.position = this.transform.position + Vector3.up * 2.0f + Vector3.right;
                    }                  
                    _delayCount = 0.0f;
                }
            }
        }
    }
}
