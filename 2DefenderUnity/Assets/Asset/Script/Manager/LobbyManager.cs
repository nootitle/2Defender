using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] GameObject _loginCanvas = null;
    [SerializeField] GameObject _lobbyCanvas = null;
    [SerializeField] GameObject _storageWindow = null;
    [SerializeField] Text _hpText = null;
    [SerializeField] Text _meleeText = null;
    [SerializeField] Text _magicText = null;
    [SerializeField] Text _healText = null;
    [SerializeField] Text _weaponText = null;
    string CurrentID = "";
    public void setID(string str) { CurrentID = str; }
    List<string> statusList = null; // 0 : hp, 1 : 근접공격력, 2 : 공격마법숙련도(마법 추가데미지), 3 : 회복마법숙련도(추가 회복), 4 : 특수무장

    public void LobbyInit(string id)
    {
        statusList = new List<string>();
        string temp = PlayerPrefs.GetString(id + "PlayerSetting");
        string temp2 = "";
        Debug.Log("호출" + temp);
        for (int i = 0; i < temp.Length; ++i)
        {
            if (temp[i] == ' ')
            {
                Debug.Log(temp2);
                statusList.Add(temp2);
                Debug.Log(float.Parse(temp2));
                temp2 = "";
            }
            else
                temp2 += temp[i];
        }

        _hpText.text = "최대체력 : " + statusList[0];
        _meleeText.text = "근접공격력 : " + statusList[1];
        _magicText.text = "공격마법숙련도 : " + statusList[2];
        _healText.text = "회복마법숙력도 : " + statusList[3];
        if (statusList[4] == "0")
            _weaponText.text = "특수장비 : 없음";
        else if (statusList[4] == "1")
            _weaponText.text = "특수장비 : 양날검";
    }

    public void GoToStage()
    {
        SceneManager.LoadScene(1);
    }

    public void showStatus()
    {
        if (_storageWindow.activeSelf)
            _storageWindow.SetActive(false);
        else
            _storageWindow.SetActive(true);
    }

    public void GoToLogin()
    {
        _lobbyCanvas.SetActive(false);
        _loginCanvas.SetActive(true);
    }
}
