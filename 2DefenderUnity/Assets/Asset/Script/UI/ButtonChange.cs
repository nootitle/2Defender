using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonChange : MonoBehaviour
{
    Image bt = null;
    bool _isTransparent = false;

    void Start()
    {
        bt = this.GetComponent<Image>();
    }

    void Update()
    {
        
    }
    
    public void changeAlpha()
    {
        if(_isTransparent)
        {
            _isTransparent = false;
            bt.color = new Color(bt.color.r, bt.color.g, bt.color.b, 1.0f);
        }
        else
        {
            _isTransparent = true;
            bt.color = new Color(bt.color.r, bt.color.g, bt.color.b, 0.5f);
        }
    }
}
