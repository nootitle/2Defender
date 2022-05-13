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
                    {
                        this.GetComponent<H_Melee>().Damaged(value);
                        ComboManager.Instance.setCombo();
                        UltBarManager.Instance.updateSlider(1.0f);
                    }
                    break;
                }
            case 1:
                {
                    if(!this.GetComponent<Skeleton_Melee>().GetDead())
                    {
                        this.GetComponent<Skeleton_Melee>().Damaged(value);
                        ComboManager.Instance.setCombo();
                        UltBarManager.Instance.updateSlider(1.0f);
                    }
                    break;
                }
            case 2:
                {
                    if (!this.GetComponent<Skeleton_Melee>().GetDead())
                    {
                        this.GetComponent<Skeleton_Melee>().Damaged(value);
                        ComboManager.Instance.setCombo();
                        UltBarManager.Instance.updateSlider(1.0f);
                    }
                    break;
                }
            case 3:
                {
                    if (!this.GetComponent<Slime>().GetDead())
                    {
                        this.GetComponent<Slime>().Damaged(value);
                        ComboManager.Instance.setCombo();
                        UltBarManager.Instance.updateSlider(1.0f);
                    }
                    break;
                }
            case 4:
                {
                    if (!this.GetComponent<BOD>().GetDead())
                    {
                        this.GetComponent<BOD>().Damaged(value);
                        ComboManager.Instance.setCombo();
                        UltBarManager.Instance.updateSlider(2.0f);
                    }
                    break;
                }
            case 5:
                {
                    if (!this.GetComponent<Archor>().GetDead())
                    {
                        this.GetComponent<Archor>().Damaged(value);
                        ComboManager.Instance.setCombo();
                        UltBarManager.Instance.updateSlider(1.0f);
                    }
                    break;
                }
            case 6:
                {
                    if (!this.GetComponent<H_Melee2>().GetDead())
                    {
                        this.GetComponent<H_Melee2>().Damaged(value);
                        ComboManager.Instance.setCombo();
                        UltBarManager.Instance.updateSlider(1.0f);
                    }
                    break;
                }
            case 7:
                {
                    if (!this.GetComponent<H_Knight>().GetDead())
                    {
                        this.GetComponent<H_Knight>().Damaged(value);
                        ComboManager.Instance.setCombo();
                        UltBarManager.Instance.updateSlider(1.0f);
                    }
                    break;
                }
            case 8:
                {
                    if (!this.GetComponent<Charger>().GetDead())
                    {
                        this.GetComponent<Charger>().Damaged(value);
                        ComboManager.Instance.setCombo();
                        UltBarManager.Instance.updateSlider(1.0f);
                    }
                    break;
                }
            case 9:
                {
                    if (!this.GetComponent<Fighter>().GetDead())
                    {
                        this.GetComponent<Fighter>().Damaged(value);
                        ComboManager.Instance.setCombo();
                        UltBarManager.Instance.updateSlider(1.0f);
                    }
                    break;
                }
            case 10:
                {
                    if (!this.GetComponent<Boss1>().GetDead())
                    {
                        this.GetComponent<Boss1>().Damaged(value);
                        ComboManager.Instance.setCombo();
                        UltBarManager.Instance.updateSlider(4.0f);
                    }
                    break;
                }
            case 11:
                {
                    if (!this.GetComponent<Boss2>().GetDead())
                    {
                        this.GetComponent<Boss2>().Damaged(value);
                        ComboManager.Instance.setCombo();
                        UltBarManager.Instance.updateSlider(4.0f);
                    }
                    break;
                }
            case 12:
                {
                    if (!this.GetComponent<Boss3>().GetDead())
                    {
                        this.GetComponent<Boss3>().Damaged(value);
                        ComboManager.Instance.setCombo();
                        UltBarManager.Instance.updateSlider(4.0f);
                    }
                    break;
                }
            case 13:
                {
                    if (!this.GetComponent<Plunder>().GetDead())
                    {
                        this.GetComponent<Plunder>().Damaged(value);
                        ComboManager.Instance.setCombo();
                        UltBarManager.Instance.updateSlider(4.0f);
                    }
                    break;
                }
            case 14:
                {
                    if (!this.GetComponent<Boss4>().GetDead())
                    {
                        this.GetComponent<Boss4>().Damaged(value);
                        ComboManager.Instance.setCombo();
                        UltBarManager.Instance.updateSlider(4.0f);
                    }
                    break;
                }
            case 15:
                {
                    if (!this.GetComponent<Bat>().GetDead())
                    {
                        this.GetComponent<Bat>().Damaged(value);
                        ComboManager.Instance.setCombo();
                        UltBarManager.Instance.updateSlider(4.0f);
                    }
                    break;
                }
            case 16:
                {
                    if (!this.GetComponent<Spiral>().GetDead())
                    {
                        this.GetComponent<Spiral>().Damaged(value);
                        ComboManager.Instance.setCombo();
                        UltBarManager.Instance.updateSlider(4.0f);
                    }
                    break;
                }
            case 17:
                {
                    if (!this.GetComponent<Turret>().GetDead())
                    {
                        this.GetComponent<Turret>().Damaged(value);
                        ComboManager.Instance.setCombo();
                        UltBarManager.Instance.updateSlider(4.0f);
                    }
                    break;
                }
            case 18:
                {
                    if (!this.GetComponent<Boss5>().GetDead())
                    {
                        this.GetComponent<Boss5>().Damaged(value);
                        ComboManager.Instance.setCombo();
                        UltBarManager.Instance.updateSlider(4.0f);
                    }
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
            case 11:
                {
                    this.GetComponent<Boss2>().respawn();
                    break;
                }
            case 12:
                {
                    this.GetComponent<Boss3>().respawn();
                    break;
                }
            case 13:
                {
                    this.GetComponent<Plunder>().respawn();
                    break;
                }
            case 14:
                {
                    this.GetComponent<Boss4>().respawn();
                    break;
                }
            case 15:
                {
                    this.GetComponent<Bat>().respawn();
                    break;
                }
            case 16:
                {
                    this.GetComponent<Spiral>().respawn();
                    break;
                }
            case 17:
                {
                    this.GetComponent<Turret>().respawn();
                    break;
                }
            case 18:
                {
                    this.GetComponent<Boss5>().respawn();
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
            case 11:
                {
                    this.GetComponent<Boss2>().CallStun(duration);
                    break;
                }
            case 12:
                {
                    this.GetComponent<Boss3>().CallStun(duration);
                    break;
                }
            case 13:
                {
                    this.GetComponent<Plunder>().CallStun(duration);
                    break;
                }
            case 14:
                {
                    this.GetComponent<Boss4>().CallStun(duration);
                    break;
                }
            case 15:
                {
                    this.GetComponent<Bat>().CallStun(duration);
                    break;
                }
            case 16:
                {
                    this.GetComponent<Spiral>().CallStun(duration);
                    break;
                }
            case 17:
                {
                    this.GetComponent<Turret>().CallStun(duration);
                    break;
                }
            case 18:
                {
                    this.GetComponent<Boss5>().CallStun(duration);
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
            case 11:
                {
                    return this.GetComponent<Boss2>().GetCenter();
                }
            case 12:
                {
                    return this.GetComponent<Boss3>().GetCenter();
                }
            case 13:
                {
                    return this.GetComponent<Plunder>().GetCenter();
                }
            case 14:
                {
                    return this.GetComponent<Boss4>().GetCenter();
                }
            case 15:
                {
                    return this.GetComponent<Bat>().GetCenter();
                }
            case 16:
                {
                    return this.GetComponent<Spiral>().GetCenter();
                }
            case 17:
                {
                    return this.gameObject;
                }
            case 18:
                {
                    return this.GetComponent<Boss5>().GetCenter();
                }
        }
        return null;
    }
}
