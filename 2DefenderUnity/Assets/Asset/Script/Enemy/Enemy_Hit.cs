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
                    if(!this.GetComponent<H_Melee>().GetDead())
                        this.GetComponent<H_Melee>().Damaged(value);
                    break;
                }
            case 1:
                {
                    if(!this.GetComponent<Skeleton_Melee>().GetDead())
                        this.GetComponent<Skeleton_Melee>().Damaged(value);
                    break;
                }
        }
    }

    public void respawn()
    {
        switch (_type)
        {
            case 0:
                {
                    this.GetComponent<H_Melee>().respawn();
                    break;
                }
            case 1:
                {
                    this.GetComponent<Skeleton_Melee>().respawn();
                    break;
                }
        }
    }
}
