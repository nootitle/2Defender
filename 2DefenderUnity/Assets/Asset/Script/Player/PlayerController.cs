using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _sr;
    Rigidbody2D _rb;
    bool _isjumping = false;
    bool _isRunning = false;
    public bool flip;
    [SerializeField] bool _joyStickMode = true;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
        _rb = this.transform.parent.GetComponent<Rigidbody2D>();
        flip = _sr.flipX;
    }

    void Update()
    {
        if (StageManager.Instance.pause) return;

        if(!_joyStickMode)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            if (x < 0)
                _sr.flipX = true;
            else if (x > 0)
                _sr.flipX = false;
            flip = _sr.flipX;

            if (Input.GetKeyDown(KeyCode.Space))
                jump();
            Move(x, y);
        }  
    }

    private void Move(float x, float y)
    {
        if (_isjumping) return;        

        if (x != 0.0f && Input.GetKey(KeyCode.LeftShift))
        {
            _animator.ResetTrigger("Walk");
            _animator.ResetTrigger("Idle");
            _animator.SetTrigger("Run");
        }
        else
        {
            _animator.ResetTrigger("Run");
            if (x != 0.0f)
            {
                _animator.SetTrigger("Walk");
                _animator.ResetTrigger("Idle");
            }
            else
            {
                _animator.ResetTrigger("Walk");
                _animator.SetTrigger("Idle");
            }
        }

        _animator.SetFloat("Speed", x);
    }

    public void MoveForJoyStick(float Hor)
    {
        if (_isjumping) return;

        if (Hor != 0.0f && _isRunning)
        {
            _animator.ResetTrigger("Walk");
            _animator.ResetTrigger("Idle");
            _animator.SetTrigger("Run");
        }
        else
        {
            _animator.ResetTrigger("Run");
            if (Hor != 0.0f)
            {
                _animator.SetTrigger("Walk");
                _animator.ResetTrigger("Idle");
            }
            else
            {
                _animator.ResetTrigger("Walk");
                _animator.SetTrigger("Idle");
            }
        }

        _animator.SetFloat("Speed", Hor);

        if (Hor < 0)
            _sr.flipX = true;
        else if (Hor > 0)
            _sr.flipX = false;
        flip = _sr.flipX;
    }

    public void sprintForButton()
    {
        _isRunning = !_isRunning;
    }

    public void jump()
    {
        if (!_isjumping)
        {
            _isjumping = true;
            _animator.SetTrigger("jump");
            _animator.SetBool("isjumping", true);
            resetMoveTrigger();
        }
    }

    public void jumpCoolDown()
    {
        if (_isjumping)
        {
            _isjumping = false;
            _animator.ResetTrigger("jump");
            _animator.SetBool("isjumping", false);
        }
    }

    public void Attack()
    {
        resetMoveTrigger();
        _animator.SetTrigger("Attack_Normal");
    }

    public void Stomp()
    {
        resetMoveTrigger();
        _animator.SetTrigger("stomp");
    }

    void resetMoveTrigger()
    {
        _animator.ResetTrigger("Run");
        _animator.ResetTrigger("Walk");
        _animator.ResetTrigger("Idle");
    }

    public void DamagedAnim()
    {
        _animator.SetTrigger("Damaged");
    }

    public void DieAnim()
    {
        _animator.SetBool("Die", true);
        _animator.SetTrigger("DieOnce");
    }

    public void slidingAnim()
    {
        _animator.SetTrigger("Sliding");
    }
}
