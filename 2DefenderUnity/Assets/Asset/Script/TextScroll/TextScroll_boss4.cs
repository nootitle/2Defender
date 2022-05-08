using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScroll_boss4 : MonoBehaviour
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
                    _text.text = "폭군 네로 : 하! 네놈이 내 부하들을 죽인 놈이냐!";
                    _text.color = Color.red;
                    _bossCG.transform.position += Vector3.up * 30.0f;
                    _pImage.color = new Color(_pImage.color.r, _pImage.color.g, _pImage.color.b, 0.7f);
                    _bImage.color = new Color(_bImage.color.r, _bImage.color.g, _bImage.color.b, 1.0f);
                    break;
                }
            case 1:
                {
                    _text.text = "주인공 : 그런 것도 타고 다니고, 아주 잘나가시는군. 남의 아버지에게 누명을 씨우고, 어머니를 탈취해간 놈치곤 말야.";
                    _text.color = Color.white;
                    _bossCG.transform.position -= Vector3.up * 30.0f;
                    _playerCG.transform.position += Vector3.up * 30.0f;
                    _pImage.color = new Color(_pImage.color.r, _pImage.color.g, _pImage.color.b, 1.0f);
                    _bImage.color = new Color(_bImage.color.r, _bImage.color.g, _bImage.color.b, 0.7f);
                    break;
                }
            case 2:
                {
                    _text.text = "폭군 네로 : ...그렇군. 너 그 반역자 놈의 아들인가. 그럼 너도 네 아비가 있는 곳으로 가라!";
                    _text.color = Color.red;
                    _bossCG.transform.position += Vector3.up * 30.0f;
                    _playerCG.transform.position -= Vector3.up * 30.0f;
                    _pImage.color = new Color(_pImage.color.r, _pImage.color.g, _pImage.color.b, 0.7f);
                    _bImage.color = new Color(_bImage.color.r, _bImage.color.g, _bImage.color.b, 1.0f);
                    break;
                }
            case 3:
                {
                    _text.text = "주인공 : 오늘을 위해 8년을 기다렸지.";
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
