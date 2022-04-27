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
    int textID = 0;

    private void OnEnable()
    {
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
        switch (textID)
        {
            case 0:
                {
                    _text.text = "도굴단 대장 : 여기가 어딘줄 알고, 간이 부었구만.";
                    _text.color = Color.red;
                    _bossCG.transform.position += Vector3.up * 15.0f;
                    break;
                }
            case 1:
                {
                    _text.text = "주인공 : 글쎄, 그보다 수인 현상금도 손가락이랑 교환하면 되는지, 혹시 아냐?";
                    _text.color = Color.white;
                    _bossCG.transform.position -= Vector3.up * 15.0f;
                    _playerCG.transform.position += Vector3.up * 15.0f;
                    break;
                }
            case 2:
                {
                    _text.text = "도굴단 대장 : 겁만 없는 게 아니라, 돈에 눈이 멀었구만.";
                    _text.color = Color.red;
                    _bossCG.transform.position += Vector3.up * 15.0f;
                    _playerCG.transform.position -= Vector3.up * 15.0f;
                    break;
                }
            case 3:
                {
                    _text.text = "주인공 : 도굴꾼 주제에 말이 많어.";
                    _text.color = Color.white;
                    _bossCG.transform.position -= Vector3.up * 15.0f;
                    _playerCG.transform.position += Vector3.up * 15.0f;
                    break;
                }
            case 4:
                {
                    _text.text = "";
                    StageManager.Instance.pause = false;
                    _textCanvas.SetActive(false);
                    _bossCG.transform.position += Vector3.up * 15.0f;
                    _playerCG.transform.position -= Vector3.up * 15.0f;
                    break;
                }
        }
    }
}
