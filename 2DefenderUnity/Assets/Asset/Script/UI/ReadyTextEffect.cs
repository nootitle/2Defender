using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyTextEffect : MonoBehaviour
{
    [SerializeField] GameObject _textObject = null;
    [SerializeField] float _speed = 5.0f;
    RectTransform _tr = null;
    bool _come = false;
    bool _leave = false;

    private void OnEnable()
    {
        _tr = _textObject.GetComponent<RectTransform>();
        _tr.anchoredPosition = new Vector2(-1250.0f, _tr.anchoredPosition.y);
        _come = true;
        StageManager.Instance.pause = true;
    }

    void Update()
    {
        if(_come)
        {
            if (_tr.anchoredPosition.x < 0)
            {
                _tr.anchoredPosition += Vector2.right * _speed * Time.deltaTime;
            }
            else
            {
                _come = false;
                _tr.anchoredPosition = new Vector2(0.0f, _tr.anchoredPosition.y);
                Invoke("setLeave", 1.0f);
            }
        }
        else if(_leave)
        {
            if (_tr.anchoredPosition.x < 1250.0f)
            {
                _tr.anchoredPosition += Vector2.right * _speed * Time.deltaTime;
            }
            else
            {
                _leave = false;
                StageManager.Instance.pause = false;
                this.gameObject.SetActive(false);
            }
        }
    }

    void setLeave()
    {
        _leave = true;
    }
}
