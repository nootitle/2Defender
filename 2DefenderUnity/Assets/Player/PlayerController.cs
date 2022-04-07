using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _sr;
    Rigidbody2D _rb;
    private float Speed;
    bool _isjumping = false;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
        _rb = this.transform.parent.GetComponent<Rigidbody2D>();
        Speed = 0.0f;
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (x < 0)
            _sr.flipX = true;
        else if(x > 0)
            _sr.flipX = false;

        jump();
        Move(x, y);        
    }

    private void Move(float x, float y)
    {
        if (_isjumping) return;

        

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _animator.SetTrigger("Run");
            _animator.ResetTrigger("Walk");
            _animator.ResetTrigger("Idle");
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

                _animator.ResetTrigger("Run");
                _animator.ResetTrigger("Walk");
                _animator.ResetTrigger("Idle");

                Invoke("jumpCoolDown", 1.0f);
            }
        }
    }

    void jumpCoolDown()
    {
        if (_isjumping)
        {
            _isjumping = false;
            _animator.ResetTrigger("jump");

            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            Move(x, y);
        }
    }
}
