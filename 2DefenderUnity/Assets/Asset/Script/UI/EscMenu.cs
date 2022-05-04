using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EscMenu : MonoBehaviour
{
    public static EscMenu Instance = null;

    [SerializeField] GameObject _player = null;
    [SerializeField] Text _text = null;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void GoToLobby()
    {
        _player.GetComponent<Player>().updateUserData();
    }

    public void GoToLobbyWithoutUpdate()
    {
        SceneManager.LoadScene(0);
    }

    public void UpdateGold(int gold)
    {
        _text.text = "¹ú¾îµéÀÎ °ñµå : " + gold.ToString();
    }
}
