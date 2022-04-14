using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSkill : MonoBehaviour
{
    [SerializeField] List<GameObject> Chocie = null;
    [SerializeField] List<GameObject> name = null;
    [SerializeField] List<GameObject> description = null;

    private void OnEnable()
    {
        SetSkillChoice();
    }

    void Update()
    {
    }

    public void SetSkillChoice()
    {
        List<int> set = new List<int>();

        for (int j = 0; j < Chocie.Count; ++j)
        {
            bool flag = false;
            int rnd = Random.Range(0, Skill_Info.Instance.GetSkillMaxNum() - 1);

            for (int i = 0; i < set.Count; ++i)
                if (set[i] == rnd)
                {
                    flag = true;
                    break;
                }
            if (flag)
            {
                --j;
                continue;
            }

            set.Add(rnd);
            Chocie[j].GetComponent<Image>().sprite = Skill_Info.Instance._iconSource[rnd];
            name[j].transform.GetChild(0).GetComponent<Text>().text = Skill_Info.Instance._name[rnd];
            description[j].transform.GetChild(0).GetComponent<Text>().text = Skill_Info.Instance._description[rnd];
            Chocie[j].GetComponent<GetSkill>().setID(rnd);
        }
    }
}
