using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UltBarManager : MonoBehaviour
{
    public static UltBarManager Instance = null;
    [SerializeField] Slider _slider = null;
    [SerializeField] GameObject _riseFx = null;
    [SerializeField] GameObject _fullFx = null;
    float _ultGauge = 0.0f;
    public float GetUltGauge() { return _ultGauge; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void updateSlider(float value)
    {
        if (_ultGauge >= 100.0f) return;

        _slider.value = Mathf.Min(_slider.maxValue, _slider.value + value);
        _ultGauge = _slider.value;
        _riseFx.SetActive(true);

        if(_ultGauge >= 100.0f)
            _fullFx.SetActive(true);
    }

    public void initSlider()
    {
        _slider.value = 0.0f;
        _ultGauge = 0.0f;
    }
}
