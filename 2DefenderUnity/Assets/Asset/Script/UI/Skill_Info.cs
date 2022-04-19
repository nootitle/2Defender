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
    [SerializeField] GameObject _goldTextWindow = null;
    [SerializeField] public List<int> allowSkill = null;
    public List<int> _alreadyHave = null;
    public int IDCount = 0;
    public int maxID = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    //SkillSlotList 오브젝트에 스킬id 순으로 스킬 아이콘, 쿨타임을 정리해두었고, start에선 이를 이용해, 자식 오브젝트(skillSlot들) 초기화
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
        maxID = 6;

        for (int i = 0; i < 7; ++i)
            allowSkill.Add(i);
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
                if(_player.transform.GetComponent<Player>()._SkillLevelList[id] <= 3)
                {
                    _player.transform.GetComponent<Player>().setSkillLevel(id, 1);
                    this.transform.GetChild(i).GetComponent<SkillSlotController>().setLevelIcon();
                }
                else
                {
                    StorageManager.Instance.setGold(100);
                    if(!_goldTextWindow.activeSelf)
                    {
                        _goldTextWindow.SetActive(true);
                        _goldTextWindow.GetComponent<WindowDisappear>().triggerOn();
                    }
                }
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
        return maxID;
    }

    public void AddNewSkillOnList(int id)
    {
        for (int i = 0; i < allowSkill.Count; ++i)
            if (allowSkill[i] == id)
                return;

        allowSkill.Add(id);
        if (id > maxID)
            maxID = id;
    }

    public void ChangeSkill(int id, int idx)
    {
        _alreadyHave[idx] = id;

        this.transform.GetChild(idx).GetComponent<Image>().sprite = _iconSource[id];
        if (id >= _coolTime.Count)
            this.transform.GetChild(idx).GetComponent<SkillSlotController>().setID(id);
        else
            this.transform.GetChild(idx).GetComponent<SkillSlotController>().setID(id, _coolTime[id]);

        _player.transform.GetComponent<Player>().setSkillLevel(id, -1);
        this.transform.GetChild(idx).GetComponent<SkillSlotController>().offAllLevelIcon();
    }
}
