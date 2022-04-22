using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBlink : MonoBehaviour
{
    Text _text = null;
    bool dir = false;

    void Start()
    {
        _text = this.GetComponent<Text>();    
    }

    void Update()
    {
        if(dir)
        {
            _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, _text.color.a - 0.002f);
            if(_text.color.a <= 0)
            {
                dir = false;
                _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, 0.0f);
            }
        }
        else
        {
            _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, _text.color.a + 0.002f);
            if (_text.color.a >= 1)
            {
                dir = true;
                _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, 1.0f);
            }
        }
    }
}
