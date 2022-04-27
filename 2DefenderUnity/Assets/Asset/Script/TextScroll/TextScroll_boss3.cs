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
                    _text.text = "��Ż�� ���ְ� : ���� ��! ���� �� �� �� ��Ƽ� �̷��� �ִ°ų�!";
                    _text.color = Color.red;
                    _bossCG.transform.position += Vector3.up * 15.0f;
                    break;
                }
            case 1:
                {
                    _text.text = "���ΰ� : ����ϸ� �װ� ���� ���� �׷���.";
                    _text.color = Color.white;
                    _bossCG.transform.position -= Vector3.up * 15.0f;
                    _playerCG.transform.position += Vector3.up * 15.0f;
                    break;
                }
            case 2:
                {
                    _text.text = "��Ż�� ���ְ� : �ǹ涳������. ���� ������� �����״�.";
                    _text.color = Color.red;
                    _bossCG.transform.position += Vector3.up * 15.0f;
                    _playerCG.transform.position -= Vector3.up * 15.0f;
                    break;
                }
            case 3:
                {
                    _text.text = "���ΰ� : �׷�, ���� �׾ ���� �ǵ��� ��.";
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
