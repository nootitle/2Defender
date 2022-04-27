using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScroll_boss1 : MonoBehaviour
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
        if(Input.GetMouseButtonDown(0))
        {
            switchingText();
            ++textID;
        }
    }

    void switchingText()
    {
        switch(textID)
        {
            case 0:
                {
                    _text.text = "왕관 찬탈자 : 누구냐! 우리 앞길을 가로막는 녀석이!";
                    _text.color = Color.red;
                    _bossCG.transform.position += Vector3.up * 15.0f;
                    break;
                }
            case 1:
                {
                    _text.text = "주인공 : 곧 죽을 놈이 내가 누군질 알아서 뭐하게.";
                    _text.color = Color.white;
                    _bossCG.transform.position -= Vector3.up * 15.0f;
                    _playerCG.transform.position += Vector3.up * 15.0f;
                    break;
                }
            case 2:
                {
                    _text.text = "왕관 찬탈자 : 초면에 말본새하곤! 항복하지 않으면 목숨은 없다.";
                    _text.color = Color.red;
                    _bossCG.transform.position += Vector3.up * 15.0f;
                    _playerCG.transform.position -= Vector3.up * 15.0f;
                    break;
                }
            case 3:
                {
                    _text.text = "주인공 : 진정해. 난 네 엄지손가락이 하나 필요할 뿐이야. 의뢰를 받았거든.";
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
