using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill_Info : MonoBehaviour
{
    public static Skill_Info Instance = null;

    [SerializeField] public List<Sprite> _iconSource = null;
    [SerializeField] public List<float> _coolTime = null;
    [SerializeField] public List<string> _name = null;
    [SerializeField] public List<string> _description = null;
    [SerializeField] GameObject _player = null;
    public List<int> _alreadyHave = null;
    public int IDCount = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    //SkillSlotList ������Ʈ�� ��ųid ������ ��ų ������, ��Ÿ���� �����صξ���, start���� �̸� �̿���, �ڽ� ������Ʈ(skillSlot��) �ʱ�ȭ
    private void Start()
    {
        for(int i = 0; i < 2; ++i)
        {
            _alreadyHave.Add(i);
            this.transform.GetChild(i).GetComponent<Image>().sprite = _iconSource[i];
            if(i >= _coolTime.Count)
                this.transform.GetChild(i).GetComponent<SkillSlotController>().setID(i);
            else
                this.transform.GetChild(i).GetComponent<SkillSlotController>().setID(i, _coolTime[i]);
        }
        IDCount = 2;
    }

    public Player GetPlayer()
    {
        return _player.GetComponent<Player>();
    }

    public void AddSkill(int id)
    {
        if (id >= _iconSource.Count) return;

        for(int i = 0; i < _alreadyHave.Count; ++i)
            if(_alreadyHave[i] == id)
            {
                Debug.Log("�ߺ��� ��ų");
                return;
            }

        _alreadyHave.Add(id);
        this.transform.GetChild(IDCount).GetComponent<Image>().sprite = _iconSource[id];
        if (id >= _coolTime.Count)
            this.transform.GetChild(IDCount).GetComponent<SkillSlotController>().setID(id);
        else
            this.transform.GetChild(IDCount).GetComponent<SkillSlotController>().setID(id, _coolTime[id]);
        ++IDCount;
    }

    public int GetSkillMaxNum()
    {
        return _iconSource.Count;
    }
}
