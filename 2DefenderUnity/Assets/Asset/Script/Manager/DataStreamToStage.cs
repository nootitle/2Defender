using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataStreamToStage : MonoBehaviour
{
    public static DataStreamToStage Instance = null;
    string currentID = "";
    bool _login = false;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }

    public void setID(string id)
    {
        currentID = id;
    }

    public string getID()
    {
        return currentID;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
        _login = true;
    }

    public void logOut()
    {
        _login = false;
    }

    public bool GetLogIn()
    {
        return _login;
    }
}
