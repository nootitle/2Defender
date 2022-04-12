using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Hit : MonoBehaviour
{
    [SerializeField] int _type = 0;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Hit(float value)
    {
        switch(_type)
        {
            case 0:
                {
                    this.GetComponent<H_Melee>().Damaged(value);
                    Debug.Log("투사체 적중");
                    break;
                }
        }
    }
}
