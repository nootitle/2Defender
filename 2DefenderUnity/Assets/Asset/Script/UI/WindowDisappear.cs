using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowDisappear : MonoBehaviour
{
    Image img = null;
    Text _text = null;

    public void triggerOn()
    {
        img = this.GetComponent<Image>();
        _text = this.transform.GetChild(0).GetComponent<Text>();
    }

    private void Update()
    {
        if (img.color.a > 0.0f || _text.color.a > 0.0f)
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a - 0.001f);
            _text.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a - 0.001f);
        }
        else
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, 1.0f);
            _text.color = new Color(img.color.r, img.color.g, img.color.b, 1.0f);
            this.gameObject.SetActive(false);
        }
    }
}
