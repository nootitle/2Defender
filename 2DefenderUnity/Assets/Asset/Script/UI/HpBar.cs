using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] Slider _hpBar = null;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void setHpBar(float damage)
    {
        _hpBar.value += damage;
    }
}
