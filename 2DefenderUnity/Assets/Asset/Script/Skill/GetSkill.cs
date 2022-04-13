using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GetSkill : MonoBehaviour
{
    [SerializeField] int Skill_ID = 0;
    [SerializeField] GameObject _playerObject = null;
    Player _player = null;

    void Start()
    {
        if(_playerObject == null)
            _playerObject = GameObject.Find("Player");
        _player = _playerObject.GetComponent<Player>();
    }

    void Update()
    {
        PlayerNearBy();
    }

    void PlayerNearBy()
    {
        if(Vector2.Distance(_playerObject.transform.position, this.transform.position) <= 4.0f)
        {
            if (Input.GetKeyDown(KeyCode.G))
                _player.GetSkill(Skill_ID);
        }
    }

    public void PlayerNearByForClick()
    {
         _player.GetSkill(Skill_ID);
    }

    public void setID(int id)
    {
        Skill_ID = id;
    }
}
