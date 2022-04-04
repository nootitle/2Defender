using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator _animator;
    private float Speed;

    void Start()
    {
        _animator = GetComponent<Animator>();
        Speed = 0.0f;
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _animator.SetTrigger("Run");
            _animator.ResetTrigger("Walk");
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _animator.SetTrigger("Walk");
            _animator.ResetTrigger("Run");
        }

        //if(Input.GetKey(KeyCode.LeftShift))
        //Speed = Mathf.Lerp(x, 2.0f, Time.deltaTime);
        //else
        //Speed = Mathf.Lerp(0.0f, x, Time.deltaTime);

        //Mathf.Clamp(Speed, 0.0f, 1.0f);

        //if (Input.GetKeyUp(KeyCode.LeftShift))
            //Speed = 0.0f;
        
        //x += Speed;

        _animator.SetFloat("Speed", x);
    }
}
