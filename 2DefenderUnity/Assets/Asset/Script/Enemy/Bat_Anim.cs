using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_Anim : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _sr;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
    }

    public void MoveAnim(float speed)
    {
         _animator.SetTrigger("Idle");

        if (speed < 0)
            _sr.flipX = true;
        else if (speed > 0)
            _sr.flipX = false;
        _animator.SetFloat("Speed", speed);
    }

    public void Attack()
    {
        resetMoveTrigger();
        _animator.SetTrigger("Attack1");
    }

    public void Stomp()
    {
        resetMoveTrigger();
        _animator.SetTrigger("Attack2");
    }

    void resetMoveTrigger()
    {
        _animator.ResetTrigger("Idle");
    }

    public void DamagedAnim()
    {
        resetMoveTrigger();
        _animator.SetTrigger("Damaged");
    }

    public void setFlip(bool value)
    {
        _sr.flipX = value;
    }

    public void DieAnim()
    {
        //_animator.SetBool("Die", true);
        _animator.SetTrigger("Die");
    }
}
