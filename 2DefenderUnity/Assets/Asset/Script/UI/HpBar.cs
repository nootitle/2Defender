using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] Slider _hpBar = null;

    public void setHpBar(float damage)
    {
        _hpBar.value = damage;
    }

    public void setMaxHpBar(float value)
    {
        this.transform.GetComponent<RectTransform>().sizeDelta += new Vector2(value, 0.0f);
        _hpBar.maxValue += value;
    }
}
