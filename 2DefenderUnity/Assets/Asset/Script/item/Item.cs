using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] GameObject _player = null;
    [SerializeField] GameObject _center = null;
    [SerializeField] float _getDistance = 5.0f;
    [SerializeField] float _speed = 5.0f;
    [SerializeField] float _value = 50.0f;
    [SerializeField] int _itemType = 0;
    float _moveInterval = 0.2f;
    Vector3 InitialPos = Vector3.zero;
    bool dir = false;

    [SerializeField] GameObject _fx = null;

    /*
     * 1 : Æ÷¼Ç
     * 2 : 
     */

    void Start()
    {
        InitialPos = _center.transform.position;
        if (_center == null)
            _center = this.gameObject;
        if (_player == null)
            _player = GameObject.Find("Player");
    }

    void Update()
    {
        getItem();
        Move();
    }

    private void Move()
    {
        if(dir)
        {
            this.transform.position += Vector3.up * _speed * Time.deltaTime;
            if(this.transform.position.y >= InitialPos.y + _moveInterval)
                dir = false;
        }
        else if (!dir)
        {
            this.transform.position += Vector3.down * _speed * Time.deltaTime;
            if(this.transform.position.y <= InitialPos.y - _moveInterval)
                dir = true;
        }
    }

    void getItem()
    {
        if(Vector2.Distance(_center.transform.position, _player.transform.position) < _getDistance)
        {
            switch(_itemType)
            {
                case 1: item_type1(); break;
                case 2: item_type2(); break;
                case 3: item_type3(); break;
            }
        }
    }

    void item_type1()
    {
        _player.GetComponent<Player>().healed(_value);
        if(_fx != null)
        {
            GameObject gm = Instantiate(_fx);
            gm.transform.position = _center.transform.position;
        }

        Destroy(this.gameObject);
    }

    void item_type2()
    {
        _player.GetComponent<Player>().riseExtraAttack(_value, 20.0f);
        if (_fx != null)
        {
            GameObject gm = Instantiate(_fx);
            gm.transform.position = _center.transform.position;
        }

        Destroy(this.gameObject);
    }

    void item_type3()
    {
        _player.GetComponent<Player>().MakeShieldByItem(_value);
        if (_fx != null)
        {
            GameObject gm = Instantiate(_fx);
            gm.transform.position = _center.transform.position;
        }

        Destroy(this.gameObject);
    }
}
