using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] Slider _hpBar = null;

    public void setHpBar(float hp)
    {
        _hpBar.value = hp;
    }

    public void setMaxHpBar(float value)
    {
        this.transform.GetComponent<RectTransform>().sizeDelta += new Vector2(value, 0.0f);
        _hpBar.maxValue += value;
    }

    public void MaxHpBarInit(float value)
    {
        this.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(value, this.transform.GetComponent<RectTransform>().sizeDelta.y);
        _hpBar.maxValue = value;
    }
}
