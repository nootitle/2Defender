using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkillSlot : MonoBehaviour
{
    [SerializeField] int _slotID = 0;
    [SerializeField] int _changeID = 0;
    [SerializeField] public List<GameObject> _levelIcon = null;
    int level = 1;

    public void setChangeID(int value)
    {
        _changeID = value;
    }

    public void changeSkill()
    {
        Skill_Info.Instance.ChangeSkill(_changeID, _slotID);
        StageManager.Instance.offRewardWindow();
    }

    public void setLevelIcon()
    {
        if (level > 3) return;
        _levelIcon[level - 1].SetActive(true);
        ++level;
    }

    public void offAllLevelIcon()
    {
        for (int i = 0; i < _levelIcon.Count; ++i)
            _levelIcon[i].SetActive(false);
        level = 1;
    }
}
