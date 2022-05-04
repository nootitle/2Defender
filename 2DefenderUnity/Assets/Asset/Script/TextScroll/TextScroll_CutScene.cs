using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScroll_CutScene : MonoBehaviour
{
    [SerializeField] GameObject _textCanvas = null;
    [SerializeField] GameObject _playerCG = null;
    [SerializeField] GameObject _bossCG = null;
    [SerializeField] Text _text = null;
    [SerializeField] Animator _playerAnim = null;
    [SerializeField] Animator _bossAnim = null;
    [SerializeField] GameObject _clearCanvas = null;
    Image _pImage = null;
    Image _bImage = null;
    int textID = 0;

    private void OnEnable()
    {
        _pImage = _playerCG.GetComponent<Image>();
        _bImage = _bossCG.GetComponent<Image>();

        textID = 0;
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
                    _text.text = "약탈자 지휘관 : 뭐 이런 괴물이 다 있어. 이거 완전 살인기계아냐.";
                    _text.color = Color.red;
                    _bossCG.transform.position += Vector3.up * 30.0f;
                    _pImage.color = new Color(_pImage.color.r, _pImage.color.g, _pImage.color.b, 0.7f);
                    _bImage.color = new Color(_bImage.color.r, _bImage.color.g, _bImage.color.b, 1.0f);
                    break;
                }
            case 1:
                {
                    _text.text = "주인공 : 네가 그런 말을 할줄은 몰랐는데.";
                    _text.color = Color.white;
                    _bossCG.transform.position -= Vector3.up * 30.0f;
                    _playerCG.transform.position += Vector3.up * 30.0f;
                    _pImage.color = new Color(_pImage.color.r, _pImage.color.g, _pImage.color.b, 1.0f);
                    _bImage.color = new Color(_bImage.color.r, _bImage.color.g, _bImage.color.b, 0.7f);
                    break;
                }
            case 2:
                {
                    _text.text = "약탈자 지휘관 : 이봐. 날 살려주면, 내 목에 걸린 현상금의 2배를 줄게. 한 번만 다시 생각해보라고.";
                    _text.color = Color.red;
                    _bossCG.transform.position += Vector3.up * 30.0f;
                    _playerCG.transform.position -= Vector3.up * 30.0f;
                    _pImage.color = new Color(_pImage.color.r, _pImage.color.g, _pImage.color.b, 0.7f);
                    _bImage.color = new Color(_bImage.color.r, _bImage.color.g, _bImage.color.b, 1.0f);
                    break;
                }
            case 3:
                {
                    _text.text = "주인공 : 넌 아직도 내가 누군지 모르겠나.";
                    _text.color = Color.white;
                    _bossCG.transform.position -= Vector3.up * 30.0f;
                    _playerCG.transform.position += Vector3.up * 30.0f;
                    _pImage.color = new Color(_pImage.color.r, _pImage.color.g, _pImage.color.b, 1.0f);
                    _bImage.color = new Color(_bImage.color.r, _bImage.color.g, _bImage.color.b, 0.7f);
                    break;
                }
            case 4:
                {
                    _text.text = "약탈자 지휘관 : 그게 무슨 말이지.";
                    _text.color = Color.red;
                    _bossCG.transform.position += Vector3.up * 30.0f;
                    _playerCG.transform.position -= Vector3.up * 30.0f;
                    _pImage.color = new Color(_pImage.color.r, _pImage.color.g, _pImage.color.b, 0.7f);
                    _bImage.color = new Color(_bImage.color.r, _bImage.color.g, _bImage.color.b, 1.0f);
                    break;
                }
            case 5:
                {
                    _text.text = "주인공 : 10년 전, 네가 털었던 라켄이라는 마을을 기억하나.";
                    _text.color = Color.white;
                    _bossCG.transform.position -= Vector3.up * 30.0f;
                    _playerCG.transform.position += Vector3.up * 30.0f;
                    _pImage.color = new Color(_pImage.color.r, _pImage.color.g, _pImage.color.b, 1.0f);
                    _bImage.color = new Color(_bImage.color.r, _bImage.color.g, _bImage.color.b, 0.7f);
                    break;
                }
            case 6:
                {
                    _text.text = "약탈자 지휘관 : 라켄.. 아니 잠깐 내가 미안..";
                    _text.color = Color.red;
                    _bossCG.transform.position += Vector3.up * 30.0f;
                    _playerCG.transform.position -= Vector3.up * 30.0f;
                    _pImage.color = new Color(_pImage.color.r, _pImage.color.g, _pImage.color.b, 0.7f);
                    _bImage.color = new Color(_bImage.color.r, _bImage.color.g, _bImage.color.b, 1.0f);
                    _playerAnim.SetTrigger("Ult_CutScene");
                    CutInManager.Instance.Activate();
                    _bossAnim.SetBool("Die", true);
                    break;
                }
            case 7:
                {
                    _text.text = "주인공 : 아니 대답할 필요없어. 생각해보니, 별로 듣고 싶지 않네. 그냥 죽어.";
                    _text.color = Color.white;
                    _bossCG.transform.position -= Vector3.up * 30.0f;
                    _playerCG.transform.position += Vector3.up * 30.0f;
                    _pImage.color = new Color(_pImage.color.r, _pImage.color.g, _pImage.color.b, 1.0f);
                    _bImage.color = new Color(_bImage.color.r, _bImage.color.g, _bImage.color.b, 0.7f);
                    _playerAnim.SetTrigger("Idle");
                    break;
                }
            case 8:
                {
                    _text.text = "주인공 : 한 놈도 남김없이 죽일거야. 한 놈도 남김없이...";
                    break;
                }
            case 9:
                {
                    _text.text = "";
                    _textCanvas.SetActive(false);
                    _bossCG.transform.position += Vector3.up * 30.0f;
                    _playerCG.transform.position -= Vector3.up * 30.0f;
                    _clearCanvas.SetActive(true);
                    SoundManager.Instance.clearBGM();
                    break;
                }
        }
    }
}
