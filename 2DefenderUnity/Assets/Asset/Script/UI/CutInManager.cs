using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutInManager : MonoBehaviour
{
    public static CutInManager Instance = null;
    [SerializeField] GameObject _player = null;

    RectTransform rt = null;
    [SerializeField] GameObject _window = null;
    [SerializeField] GameObject _textObj = null;
    [SerializeField] float _speed = 250.0f;
    [SerializeField] GameObject _fx = null;
    [SerializeField] List<GameObject> _ult1Fxs = null;
    bool _isActive = false;
    [SerializeField] float _radius = 50.0f;
    [SerializeField] float _damage = 150.0f;
    [SerializeField] GameObject _hitFx = null;

    RectTransform rt2 = null;
    [SerializeField] GameObject _window2 = null;
    [SerializeField] GameObject _textObj2 = null;
    [SerializeField] float _speed2 = 250.0f;
    [SerializeField] GameObject _fx2 = null;
    [SerializeField] List<GameObject> _ult2Fxs = null;
    bool _isActive2 = false;
    [SerializeField] float _radius2 = 50.0f;
    [SerializeField] float _damage2 = 150.0f;
    [SerializeField] GameObject _hitFx2 = null;
    [SerializeField] int _hitNum = 20;
    [SerializeField] List<AudioSource> _ult2SE = null;
    [SerializeField] GameObject _panel = null;

    Coroutine _end2Co = null;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Update()
    {
        if(_isActive)
        {
            if (rt.sizeDelta.y < 500.0f)
            {
                rt.sizeDelta = new Vector2(this.GetComponent<RectTransform>().sizeDelta.x,
                    this.GetComponent<RectTransform>().sizeDelta.y + _speed * Time.deltaTime);
            }
            else
            {
                rt.sizeDelta = new Vector2(this.GetComponent<RectTransform>().sizeDelta.x, 500.0f);
                _isActive = false;
                Invoke("disable", 1.0f);
            }
        }

        if (_isActive2)
        {
            if (rt2.sizeDelta.y < 500.0f)
            {
                rt2.sizeDelta = new Vector2(this.GetComponent<RectTransform>().sizeDelta.x,
                    this.GetComponent<RectTransform>().sizeDelta.y + _speed2 * Time.deltaTime);
            }
            else
            {
                rt2.sizeDelta = new Vector2(this.GetComponent<RectTransform>().sizeDelta.x, 500.0f);
                _isActive2 = false;
                Invoke("disable2", 1.0f);
            }
        }
    }

    public void Activate()
    {
        if(StageManager.Instance != null)
            StageManager.Instance.pause = true;
        if(_player == null)
            _player = GameObject.Find("Player");

        _window.SetActive(true);
        _textObj.SetActive(true);
        rt = _window.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(this.GetComponent<RectTransform>().sizeDelta.x, 0.0f);
        _isActive = true;
        _fx.SetActive(true);
    }

    public void Activate2()
    {
        if (StageManager.Instance != null)
            StageManager.Instance.pause = true;
        if (_player == null)
            _player = GameObject.Find("Player");

        _window2.SetActive(true);
        _textObj2.SetActive(true);
        rt2 = _window2.GetComponent<RectTransform>();
        rt2.sizeDelta = new Vector2(this.GetComponent<RectTransform>().sizeDelta.x, 0.0f);
        _isActive2 = true;
        _fx2.SetActive(true);
    }

    void disable()
    {
        rt.sizeDelta = new Vector2(this.GetComponent<RectTransform>().sizeDelta.x, 0.0f);
        _textObj.SetActive(false);
        for(int i = 0; i < _ult1Fxs.Count; ++i)
            _ult1Fxs[i].SetActive(true);
        _panel.SetActive(true);
        Invoke("end", 1.0f);
    }

    void disable2()
    {
        rt2.sizeDelta = new Vector2(this.GetComponent<RectTransform>().sizeDelta.x, 0.0f);
        _textObj2.SetActive(false);
        for (int i = 0; i < _ult2Fxs.Count; ++i)
        {
            _ult2Fxs[i].SetActive(true);
            _ult2Fxs[i].transform.position = _player.transform.position;
            _ult2SE[i % 3].Play();
        }
        _panel.SetActive(true);
        Invoke("end2", 1.0f);
    }

    private void end()
    {
        _window.SetActive(false);
        for (int i = 0; i < _ult1Fxs.Count; ++i)
            _ult1Fxs[i].SetActive(false);
        if (StageManager.Instance != null)
            StageManager.Instance.pause = false;

        Collider2D[] co = Physics2D.OverlapCircleAll(_player.transform.position, _radius);
        for (int i = 0; i < co.Length; ++i)
        {
            Enemy_Hit EH = co[i].gameObject.GetComponent<Enemy_Hit>();
            if(EH != null)
            {
                EH.Hit(_damage);
                GameObject gm = Instantiate(_hitFx);
                gm.transform.position = EH.GetCenter().transform.position;
            }
        }

        _panel.SetActive(false);
    }

    private void end2()
    {
        _window2.SetActive(false);
        for (int i = 0; i < _ult2Fxs.Count; ++i)
            _ult2Fxs[i].SetActive(false);

        if (_end2Co != null) StopCoroutine(_end2Co);
        _end2Co = StartCoroutine(end2Process());
    }

    IEnumerator end2Process()
    {
        Time.timeScale = 0.3f;
        for (int j = 0; j < _hitNum; ++j)
        {
            yield return new WaitForSeconds(0.1f);           

            Collider2D[] co = Physics2D.OverlapCircleAll(_player.transform.position, _radius2);
            for (int i = 0; i < co.Length; ++i)
            {
                Enemy_Hit EH = co[i].gameObject.GetComponent<Enemy_Hit>();
                if (EH != null)
                {
                    EH.Hit(_damage2);
                    GameObject gm = Instantiate(_hitFx2);
                    gm.transform.position = EH.GetCenter().transform.position;
                }
            }

            if(j == 5) Time.timeScale = 0.5f;
            else if(j == 10) Time.timeScale = 0.7f;
        }
        _panel.SetActive(false);
        Time.timeScale = 1.0f;

        if (StageManager.Instance != null)
            StageManager.Instance.pause = false;
    }
}
