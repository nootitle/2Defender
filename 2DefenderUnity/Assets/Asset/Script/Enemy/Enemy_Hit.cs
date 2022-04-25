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
            case 6:
                {
                    if (!this.GetComponent<H_Melee2>().GetDead())
                        this.GetComponent<H_Melee2>().Damaged(value);
                    break;
                }
            case 7:
                {
                    if (!this.GetComponent<H_Knight>().GetDead())
                        this.GetComponent<H_Knight>().Damaged(value);
                    break;
                }
            case 8:
                {
                    if (!this.GetComponent<Charger>().GetDead())
                        this.GetComponent<Charger>().Damaged(value);
                    break;
                }
            case 9:
                {
                    if (!this.GetComponent<Fighter>().GetDead())
                        this.GetComponent<Fighter>().Damaged(value);
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
            case 6:
                {
                    this.GetComponent<H_Melee2>().respawn();
                    break;
                }
            case 7:
                {
                    this.GetComponent<H_Knight>().respawn();
                    break;
                }
            case 8:
                {
                    this.GetComponent<Charger>().respawn();
                    break;
                }
            case 9:
                {
                    this.GetComponent<Fighter>().respawn();
                    break;
                }
            case 10:
                {
                    this.GetComponent<Boss1>().respawn();
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
            case 6:
                {
                    this.GetComponent<H_Melee2>().CallStun(duration);
                    break;
                }
            case 7:
                {
                    this.GetComponent<H_Knight>().CallStun(duration);
                    break;
                }
            case 8:
                {
                    this.GetComponent<Charger>().CallStun(duration);
                    break;
                }
            case 9:
                {
                    this.GetComponent<Fighter>().CallStun(duration);
                    break;
                }
            case 10:
                {
                    this.GetComponent<Boss1>().CallStun(duration);
                    break;
                }
        }
    }

    public GameObject GetCenter()
    {
        switch (_type)
        {
            case 0:
                {
                    return this.GetComponent<H_Melee>().GetCenter();
                }
            case 1:
                {
                    return this.GetComponent<Skeleton_Melee>().GetCenter();
                }
            case 2:
                {
                    return this.GetComponent<Skeleton_Melee>().GetCenter();
                }
            case 3:
                {
                    return this.GetComponent<Slime>().GetCenter();
                }
            case 4:
                {
                    return this.GetComponent<BOD>().GetCenter();
                }
            case 5:
                {
                    return this.GetComponent<Archor>().GetCenter();
                }
            case 6:
                {
                    return this.GetComponent<H_Melee2>().GetCenter();
                }
            case 7:
                {
                    return this.GetComponent<H_Knight>().GetCenter();
                }
            case 8:
                {
                    return this.GetComponent<Charger>().GetCenter();
                }
            case 9:
                {
                    return this.GetComponent<Fighter>().GetCenter();
                }
            case 10:
                {
                    return this.GetComponent<Boss1>().GetCenter();
                }
        }
        return null;
    }
}
