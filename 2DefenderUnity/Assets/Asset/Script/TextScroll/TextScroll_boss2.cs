using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScroll_boss2 : MonoBehaviour
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
                    _text.text = "도굴단 대장 : 여기가 어딘줄 알고, 간이 부었구만.";
                    _text.color = Color.red;
                    _bossCG.transform.position += Vector3.up * 30.0f;
                    _pImage.color = new Color(_pImage.color.r, _pImage.color.g, _pImage.color.b, 0.7f);
                    _bImage.color = new Color(_bImage.color.r, _bImage.color.g, _bImage.color.b, 1.0f);
                    break;
                }
            case 1:
                {
                    _text.text = "주인공 : 글쎄, 그보다 '네로'라는 녀석을 알고 있지?";
                    _text.color = Color.white;
                    _bossCG.transform.position -= Vector3.up * 30.0f;
                    _playerCG.transform.position += Vector3.up * 30.0f;
                    _pImage.color = new Color(_pImage.color.r, _pImage.color.g, _pImage.color.b, 1.0f);
                    _bImage.color = new Color(_bImage.color.r, _bImage.color.g, _bImage.color.b, 0.7f);
                    break;
                }
            case 2:
                {
                    _text.text = "도굴단 대장 : 몰라. 네 친구를 왜 여기서 찾냐.";
                    _text.color = Color.red;
                    _bossCG.transform.position += Vector3.up * 30.0f;
                    _playerCG.transform.position -= Vector3.up * 30.0f;
                    _pImage.color = new Color(_pImage.color.r, _pImage.color.g, _pImage.color.b, 0.7f);
                    _bImage.color = new Color(_bImage.color.r, _bImage.color.g, _bImage.color.b, 1.0f);
                    break;
                }
            case 3:
                {
                    _text.text = "주인공 : 그야 네가 그 놈의 의뢰를 받고, 내 아버지에게 누명을 씌웠었으니까.";
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
