using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class SkillSlotController : MonoBehaviour
{
    [SerializeField] private Image SlotSprite;
    [SerializeField] private GameObject SlotFx;
    [SerializeField] private float CoolTime = 4.0f;
    [SerializeField] int _id = -1;
    [SerializeField] public List<GameObject> _levelIcon = null;
    Player _player = null;
    private bool SkillOn;
    Coroutine co = null;
    Coroutine co2 = null;
    int level = 1;
    public int GetID() { return _id; }

    void Start()
    {
        _player = this.transform.parent.GetComponent<Skill_Info>().GetPlayer();
        SkillOn = false;
        if (CoolTime <= 0)
            CoolTime = 1.0f;
        _id = -1;
    }

    public void setID(int id, float coolTime = 0.5f)
    {
        _id = id;
        CoolTime = coolTime;
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

    public void StartCooldown()
    {
        if (_id == -1) return;
        if (SkillOn) return;

        SkillOn = true;
        _player.CallSkill(_id);
        if (co != null) StopCoroutine(co);
        co = StartCoroutine(SkillCooldown());
        
    }

    IEnumerator SkillCooldown()
    {
        while (SlotSprite.fillAmount < 1.0f)
        {
            SlotSprite.fillAmount += Time.deltaTime * (1 / CoolTime);
            yield return null;
        }

        if (SlotFx != null)
            SlotFx.SetActive(true);
        SlotSprite.fillAmount = 0.0f;

        if (co2 != null) StopCoroutine(co2);
        co2 = StartCoroutine(CooldownFx());
    }

    IEnumerator CooldownFx()
    {
        yield return new WaitForSeconds(0.1f);

        if (SlotFx != null)
            SlotFx.SetActive(false);
        SkillOn = false;
    }
}