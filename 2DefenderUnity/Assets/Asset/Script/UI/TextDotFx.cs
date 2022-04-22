using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDotFx : MonoBehaviour
{
    [SerializeField] string mainText = "";
    Text _text = null;
    int cnt = 0;
    int limit = 999;
    Coroutine _co = null;

    private void OnEnable()
    {
        limit = 999;
        _text = this.GetComponent<Text>();
        _co = StartCoroutine(fx());
    }

    IEnumerator fx()
    {
        while(limit >= 0)
        {
            yield return new WaitForSeconds(0.5f);

            switch (cnt)
            {
                case 0: _text.text = mainText; ++cnt; break;
                case 1: _text.text = mainText + "."; ++cnt; break;
                case 2: _text.text = mainText + ".."; ++cnt; break;
                case 3: _text.text = mainText + "..."; ++cnt; break;
                case 4: _text.text = mainText + "...."; ++cnt; break;
                case 5: _text.text = mainText + "....."; cnt = 0; break;
            }
            --limit;
        }
    }
}
