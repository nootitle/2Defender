using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Hit : MonoBehaviour
{
    [SerializeField] int _type = 0;

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
            case 2:
                {
                    if (!this.GetComponent<Skeleton_Melee>().GetDead())
                        this.GetComponent<Skeleton_Melee>().Damaged(value);
                    break;
                }
            case 3:
                {
                    if (!this.GetComponent<Slime>().GetDead())
                        this.GetComponent<Slime>().Damaged(value);
                    break;
                }
            case 4:
                {
                    if (!this.GetComponent<BOD>().GetDead())
                        this.GetComponent<BOD>().Damaged(value);
                    break;
                }
            case 5:
                {
                    if (!this.GetComponent<Archor>().GetDead())
                        this.GetComponent<Archor>().Damaged(value);
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
            case 2:
                {
                    this.GetComponent<Skeleton_Melee>().respawn();
                    break;
                }
            case 3:
                {
                    this.GetComponent<Slime>().respawn();
                    break;
                }
            case 4:
                {
                    this.GetComponent<BOD>().respawn();
                    break;
                }
            case 5:
                {
                    this.GetComponent<Archor>().respawn();
                    break;
                }
        }
    }

    public void Stun(float duration)
    {
        switch (_type)
        {
            case 0:
                {
                    this.GetComponent<H_Melee>().CallStun(duration);
                    break;
                }
            case 1:
                {
                    this.GetComponent<Skeleton_Melee>().CallStun(duration);
                    break;
                }
            case 2:
                {
                    this.GetComponent<Skeleton_Melee>().CallStun(duration);
                    break;
                }
            case 3:
                {
                    this.GetComponent<Slime>().CallStun(duration);
                    break;
                }
            case 4:
                {
                    this.GetComponent<BOD>().CallStun(duration);
                    break;
                }
            case 5:
                {
                    this.GetComponent<Archor>().CallStun(duration);
                    break;
                }
        }
    }
}
