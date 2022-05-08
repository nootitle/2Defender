using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss4_Anim : MonoBehaviour
{
    [SerializeField] Animator _animator;
    private SpriteRenderer _sr;

    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    public void MoveAnim(bool isStop, float speed)
    {
        if (!isStop)
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

    public void BeamAttack()
    {
        resetMoveTrigger();
        _animator.SetTrigger("BeamAttack");
    }

    public void GrenadeAttack()
    {
        resetMoveTrigger();
        _animator.SetTrigger("GrenadeAttack");
    }

    public void LightningAttack()
    {
        resetMoveTrigger();
        _animator.SetTrigger("LightningAttack");
    }

    public void Appear()
    {
        _animator.SetTrigger("Appear");
    }

    void resetMoveTrigger()
    {
        _animator.ResetTrigger("Walk");
        _animator.ResetTrigger("Idle");
    }

    public void DamagedAnim()
    {
        resetMoveTrigger();
        _animator.SetTrigger("Disabled");
    }

    public void setFlip(bool value)
    {
        _sr.flipX = value;
    }

    public void DieAnim()
    {
        _animator.SetTrigger("Die");
    }
}
