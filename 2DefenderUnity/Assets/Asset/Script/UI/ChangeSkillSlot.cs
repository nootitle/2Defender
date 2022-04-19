using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkillSlot : MonoBehaviour
{
    [SerializeField] int _slotID = 0;
    [SerializeField] int _changeID = 0;

    public void setChangeID(int value)
    {
        _changeID = value;
    }

    public void changeSkill()
    {
        Skill_Info.Instance.ChangeSkill(_changeID, _slotID);
    }
}
