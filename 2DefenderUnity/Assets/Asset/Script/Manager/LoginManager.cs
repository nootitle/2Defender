using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    public static LoginManager Instance = null;
    [SerializeField] InputField _id_field = null;
    [SerializeField] InputField _pass_field = null;
    [SerializeField] GameObject _LowLengthMessage = null;
    [SerializeField] GameObject _AlreadyMessage = null;
    [SerializeField] GameObject _incorrectMessage = null;
    [SerializeField] GameObject _sucessMessage = null;
    [SerializeField] GameObject _loginCanvas = null;
    [SerializeField] GameObject _lobbyCanvas = null;
    [SerializeField] GameObject _lobbyManager = null;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void createAccount()
    {
        string id = _id_field.text;
        string pass = _pass_field.text;

        if (id.Length < 5 || id.Length > 8 ||
            pass.Length < 5 || pass.Length > 8)
        {
            allMessageOff();
            _LowLengthMessage.SetActive(true);
            return;
        }

        if (PlayerPrefs.GetString(id) != null &&
            PlayerPrefs.GetString(id) != "")
        {
            allMessageOff();
            _AlreadyMessage.SetActive(true);
            return;
        }
        
        PlayerPrefs.SetString(id, pass);
        PlayerPrefs.SetString(id + "PlayerSetting", "100 50 0 0 0 ");
    }

    public void login()
    {
        string id = _id_field.text;
        string pass = _pass_field.text;

        if(!PlayerPrefs.HasKey(id))
        {
            allMessageOff();
            _incorrectMessage.SetActive(true);
            return;
        }    

        if (PlayerPrefs.GetString(id) == pass)
        {
            allMessageOff();
            _lobbyCanvas.SetActive(true);
            _sucessMessage.SetActive(true);
            _loginCanvas.SetActive(false);
            _lobbyManager.GetComponent<LobbyManager>().setID(id);
            _lobbyManager.GetComponent<LobbyManager>().LobbyInit(id);
        }
        else
        {
            allMessageOff();
            _incorrectMessage.SetActive(true);
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    void allMessageOff()
    {
        _LowLengthMessage.SetActive(false);
        _AlreadyMessage.SetActive(false);
        _incorrectMessage.SetActive(false);
        _sucessMessage.SetActive(false);
    }
}
