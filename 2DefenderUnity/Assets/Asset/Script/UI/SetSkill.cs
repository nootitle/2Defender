using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSkill : MonoBehaviour
{
    [SerializeField] List<GameObject> Chocie = null;

    void Start()
    {
        if(Chocie == null)
        {
            for(int i = 0; ; ++i)
            {
                GameObject gm = this.transform.GetChild(i).gameObject;
                if (gm == null)
                    break;
                else
                    Chocie.Add(gm);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            SetSkillChoice();
    }

    public void SetSkillChoice()
    {
        List<int> set = new List<int>();

        for (int j = 0; j < Chocie.Count; ++j)
        {
            List<int> temp = Skill_Info.Instance._alreadyHave;
            int tempInt = Skill_Info.Instance.IDCount;
            bool flag = false;

            int rnd = Random.Range(0, tempInt - 1);

            for (int i = 0; i < temp.Count; ++i)
                if (i == rnd)
                {
                    flag = true;
                    break;
                }
            if (flag)
                break;

            for (int i = 0; i < set.Count; ++i)
                if (i == rnd)
                {
                    flag = true;
                    break;
                }
            if (flag)
                break;

            set.Add(rnd);
            Chocie[j].GetComponent<Image>().sprite = Skill_Info.Instance._iconSource[rnd];
            Chocie[j].GetComponent<GetSkill>().setID(rnd);
        }
    }
}
