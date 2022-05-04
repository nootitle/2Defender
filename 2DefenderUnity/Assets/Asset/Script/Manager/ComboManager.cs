using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour
{
    public static ComboManager Instance = null;

    [SerializeField] GameObject _mainText = null;
    [SerializeField] GameObject _subText = null;
    Text _text = null;
    int _combo = 0;
    float _cnt = 0.0f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Update()
    {
        if(_cnt >= 5.0f)
        {
            _combo = 0;
            _text = _mainText.GetComponent<Text>();
            _text.text = "";
            _subText.SetActive(false);
            _mainText.SetActive(false);
        }

        if(_combo > 0)
        {
            _cnt += Time.deltaTime;
        }
    }

    public void setCombo()
    {
        ++_combo;
        _cnt = 0.0f;
        _mainText.SetActive(true);
        _subText.SetActive(true);
        _text = _mainText.GetComponent<Text>();
        _text.text = _combo.ToString();
    }
}
