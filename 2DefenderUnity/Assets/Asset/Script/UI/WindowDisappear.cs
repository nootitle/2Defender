using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowDisappear : MonoBehaviour
{
    Image img = null;
    Text _text = null;
    Image _frame = null;

    private void OnEnable()
    {
        img = this.GetComponent<Image>();
        _text = this.transform.GetChild(0).GetComponent<Text>();
        _frame = this.transform.GetChild(1).GetComponent<Image>();
    }

    public void triggerOn()
    {
        img = this.GetComponent<Image>();
        _text = this.transform.GetChild(0).GetComponent<Text>();
        _frame = this.transform.GetChild(1).GetComponent<Image>();
    }

    private void Update()
    {
        if (img.color.a > 0.0f || _text.color.a > 0.0f)
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a - 0.002f);
            _frame.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a - 0.002f);
            _text.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a - 0.002f);
        }
        else
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, 1.0f);
            _frame.color = new Color(img.color.r, img.color.g, img.color.b, 1.0f);
            _text.color = new Color(img.color.r, img.color.g, img.color.b, 1.0f);
            this.gameObject.SetActive(false);
        }
    }
}
