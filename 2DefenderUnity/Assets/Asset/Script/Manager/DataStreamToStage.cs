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

    public void SetClearData(int mapID)
    {
        int currentMapID = PlayerPrefs.GetInt(currentID + "PlayerSetting_Level");
        if (currentMapID >= mapID) return;

        switch(mapID)
        {
            case 1: PlayerPrefs.SetInt(currentID + "PlayerSetting_Level", 1); break;
            case 2: PlayerPrefs.SetInt(currentID + "PlayerSetting_Level", 2); break;
            case 3: PlayerPrefs.SetInt(currentID + "PlayerSetting_Level", 3); break;
        }
    }
}
