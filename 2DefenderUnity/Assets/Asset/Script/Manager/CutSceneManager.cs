using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneManager : MonoBehaviour
{
    [SerializeField] GameObject _player = null;
    [SerializeField] Animator _playerAnim = null;
    [SerializeField] Animator _bossAnim = null;
    [SerializeField] GameObject _BannerWindow = null;
    [SerializeField] Camera _camera = null;
    bool _textStart = false;

    void Start()
    {
        if (_camera != null)
            _camera = Camera.main;
        _playerAnim.ResetTrigger("isjumping");
        _playerAnim.SetTrigger("Idle");
        _bossAnim.ResetTrigger("isjumping");
        _bossAnim.SetTrigger("CutSceneFall");
    }

    void Update()
    {
        if(!_textStart && Mathf.Abs(_camera.transform.position.x - _player.transform.position.x) < 1.0f)
        {
            _textStart = true;
            _BannerWindow.SetActive(true);
        }
    }
}
