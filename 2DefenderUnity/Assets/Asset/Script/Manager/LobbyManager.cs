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
    List<string> statusList = null; // 0 : hp, 1 : �������ݷ�, 2 : ���ݸ������õ�(���� �߰�������), 3 : ȸ���������õ�(�߰� ȸ��), 4 : Ư������

    public void LobbyInit(string id)
    {
        statusList = new List<string>();
        string temp = PlayerPrefs.GetString(id + "PlayerSetting");
        string temp2 = "";
        Debug.Log("ȣ��" + temp);
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

        _hpText.text = "�ִ�ü�� : " + statusList[0];
        _meleeText.text = "�������ݷ� : " + statusList[1];
        _magicText.text = "���ݸ������õ� : " + statusList[2];
        _healText.text = "ȸ���������µ� : " + statusList[3];
        if (statusList[4] == "0")
            _weaponText.text = "Ư����� : ����";
        else if (statusList[4] == "1")
            _weaponText.text = "Ư����� : �糯��";
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
