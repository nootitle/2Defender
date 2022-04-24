using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscMenu : MonoBehaviour
{
    [SerializeField] GameObject _player = null;

    public void GoToLobby()
    {
        _player.GetComponent<Player>().updateUserData();
    }
}
