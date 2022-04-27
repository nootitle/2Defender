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
                    _text.text = "약탈자 지휘관 : 뭣들 해! 지금 한 놈도 못 잡아서 이러고 있는거냐!";
                    _text.color = Color.red;
                    _bossCG.transform.position += Vector3.up * 15.0f;
                    break;
                }
            case 1:
                {
                    _text.text = "주인공 : 답답하면 네가 직접 뛰지 그러냐.";
                    _text.color = Color.white;
                    _bossCG.transform.position -= Vector3.up * 15.0f;
                    _playerCG.transform.position += Vector3.up * 15.0f;
                    break;
                }
            case 2:
                {
                    _text.text = "약탈자 지휘관 : 건방떨지마라. 아주 묵사발을 내줄테니.";
                    _text.color = Color.red;
                    _bossCG.transform.position += Vector3.up * 15.0f;
                    _playerCG.transform.position -= Vector3.up * 15.0f;
                    break;
                }
            case 3:
                {
                    _text.text = "주인공 : 그래, 빨리 죽어서 돈이 되도록 해.";
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
