using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScroll_boss3 : MonoBehaviour
{
    [SerializeField] GameObject _textCanvas = null;
    [SerializeField] GameObject _playerCG = null;
    [SerializeField] GameObject _bossCG = null;
    [SerializeField] Text _text = null;
    Image _pImage = null;
    Image _bImage = null;
    int textID = 0;

    private void OnEnable()
    {
        _pImage = _playerCG.GetComponent<Image>();
        _bImage = _bossCG.GetComponent<Image>();

        textID = 0;
        StageManager.Instance.pause = true;
        switchingText();
        ++textID;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            switchingText();
            ++textID;
        }
    }

    void switchingText()
    {
        SoundManager.Instance.clickSE();
        switch (textID)
        {
            case 0:
                {
                    _text.text = "약탈자 지휘관 : 뭣들 해! 고작 한 놈 잡는데 몇 명이 달려드는 거냐! 이놈들아!";
                    _text.color = Color.red;
                    _bossCG.transform.position += Vector3.up * 30.0f;
                    _pImage.color = new Color(_pImage.color.r, _pImage.color.g, _pImage.color.b, 0.7f);
                    _bImage.color = new Color(_bImage.color.r, _bImage.color.g, _bImage.color.b, 1.0f);
                    break;
                }
            case 1:
                {
                    _text.text = "주인공 : 답답하면 네가 직접 뛰지 그러냐.";
                    _text.color = Color.white;
                    _bossCG.transform.position -= Vector3.up * 30.0f;
                    _playerCG.transform.position += Vector3.up * 30.0f;
                    _pImage.color = new Color(_pImage.color.r, _pImage.color.g, _pImage.color.b, 1.0f);
                    _bImage.color = new Color(_bImage.color.r, _bImage.color.g, _bImage.color.b, 0.7f);
                    break;
                }
            case 2:
                {
                    _text.text = "약탈자 지휘관 : 건방떨지마라. 아주 묵사발에 피떡으로 만들어버릴테다. 날파리같은 놈!";
                    _text.color = Color.red;
                    _bossCG.transform.position += Vector3.up * 30.0f;
                    _playerCG.transform.position -= Vector3.up * 30.0f;
                    _pImage.color = new Color(_pImage.color.r, _pImage.color.g, _pImage.color.b, 0.7f);
                    _bImage.color = new Color(_bImage.color.r, _bImage.color.g, _bImage.color.b, 1.0f);
                    break;
                }
            case 3:
                {
                    _text.text = "주인공 : 그래, 빨리와라. 그리고 그대~로 죽어서 나의 돈이 되도록 해.";
                    _text.color = Color.white;
                    _bossCG.transform.position -= Vector3.up * 30.0f;
                    _playerCG.transform.position += Vector3.up * 30.0f;
                    _pImage.color = new Color(_pImage.color.r, _pImage.color.g, _pImage.color.b, 1.0f);
                    _bImage.color = new Color(_bImage.color.r, _bImage.color.g, _bImage.color.b, 0.7f);
                    break;
                }
            case 4:
                {
                    _text.text = "";
                    StageManager.Instance.pause = false;
                    _textCanvas.SetActive(false);
                    _bossCG.transform.position += Vector3.up * 30.0f;
                    _playerCG.transform.position -= Vector3.up * 30.0f;
                    break;
                }
        }
    }
}
