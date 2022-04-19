using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GetSkill : MonoBehaviour
{
    [SerializeField] int Skill_ID = 0;
    [SerializeField] GameObject _playerObject = null;
    [SerializeField] GameObject _changeSkillWindow = null;
    [SerializeField] GameObject _SkillSlotList = null;

    void Start()
    {
        if(_playerObject == null)
            _playerObject = GameObject.Find("Player");
    }

    public void PlayerNearByForClick()
    {
        if(Skill_Info.Instance._alreadyHave.Count >= Skill_Info.Instance.transform.childCount)
        {
            bool flag = false;
            for(int i = 0; i < Skill_Info.Instance._alreadyHave.Count; ++i)
                if(Skill_Info.Instance._alreadyHave[i] == Skill_ID)
                {
                    flag = true;
                    break;
                }

            if(!flag)
            {
                _changeSkillWindow.SetActive(true);
                _SkillSlotList.SetActive(true);
                for (int i = 0; i < _SkillSlotList.transform.childCount; ++i)
                    _SkillSlotList.transform.GetChild(i).GetComponent<ChangeSkillSlot>().setChangeID(Skill_ID);
            }
            else
            {
                Skill_Info.Instance.AddSkill(Skill_ID);
                StageManager.Instance.offRewardWindow();
            }

        }
        else
        {
            Skill_Info.Instance.AddSkill(Skill_ID);
            StageManager.Instance.offRewardWindow();
        }
    }

    public void setID(int id)
    {
        Skill_ID = id;
    }
}
