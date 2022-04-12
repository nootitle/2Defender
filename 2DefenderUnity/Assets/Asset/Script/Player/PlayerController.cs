using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _sr;
    Rigidbody2D _rb;
    bool _isjumping = false;
    public bool flip;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
        _rb = this.transform.parent.GetComponent<Rigidbody2D>();
        flip = _sr.flipX;
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (x < 0)
            _sr.flipX = true;
        else if(x > 0)
            _sr.flipX = false;
        flip = _sr.flipX;

        jump();
        Move(x, y);        
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

    private void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(!_isjumping)
            {
                _isjumping = true;
                _animator.SetTrigger("jump");
                _animator.SetBool("isjumping", true);
                resetMoveTrigger();
            }
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
}
