using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorageManager : MonoBehaviour
{
    public static StorageManager Instance = null;
    [SerializeField] Text _goldText = null;
    [SerializeField] GameObject _storageWindow = null;
    [SerializeField] GameObject _passiveList = null;
    [SerializeField] GameObject _dropList = null;
    [SerializeField] GameObject _LobbyManager = null;
    [SerializeField] List<int> _alreadyHave = null;
    [SerializeField] GameObject _purchaseMessage = null;
    [SerializeField] GameObject _alreadyHaveMessage = null;

    [SerializeField] int _gold = 0;
    int goldDelta = 0;
    public int getGoldDelta() { return goldDelta; }
    public void addGoldDelta(int value) { goldDelta += value; }

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        goldDelta = 0;
    }

    public void setGold(int gold)
    {
        _gold += gold;
        _goldText.text = _gold.ToString();
        if(_LobbyManager != null)
            _LobbyManager.GetComponent<LobbyManager>().updateGold(_gold);
        if(EscMenu.Instance != null)
            EscMenu.Instance.UpdateGold(goldDelta);
    }

    public void setGoldDirectly(int gold)
    {
        _gold = gold;
        _goldText.text = _gold.ToString();
        if (_LobbyManager != null)
            _LobbyManager.GetComponent<LobbyManager>().updateGold(_gold);
    }

    public int getGold()
    {
        return _gold;
    }

    public void showStorageWindow()
    {
        if (_storageWindow.activeSelf)
            _storageWindow.SetActive(false);
        else
            _storageWindow.SetActive(true);
    }

    public void changeList(int value)
    {
        switch(value)
        {
            case 0:
                _passiveList.SetActive(true);
                _dropList.SetActive(false);
                break;
            case 1:
                _passiveList.SetActive(false);
                _dropList.SetActive(true);
                break;
        }
    }

    public void UpgradeStatus(int id)
    {
        switch(id)
        {
            case 0:
                {
                    if(_gold >= 50)
                    {
                        setGold(-50);
                        _LobbyManager.GetComponent<LobbyManager>().updateStatus(5.0f, 0.0f, 0.0f, 0.0f);
                        _purchaseMessage.SetActive(true);
                    }
                    break;
                }
            case 1:
                {
                    if (_gold >= 50)
                    {
                        setGold(-50);
                        _LobbyManager.GetComponent<LobbyManager>().updateStatus(0.0f, 5.0f, 0.0f, 0.0f);
                        _purchaseMessage.SetActive(true);
                    }
                    break;
                }
            case 2:
                {
                    if (_gold >= 50)
                    {
                        setGold(-50);
                        _LobbyManager.GetComponent<LobbyManager>().updateStatus(0.0f, 0.0f, 5.0f, 0.0f);
                        _purchaseMessage.SetActive(true);
                    }
                    break;
                }
            case 3:
                {
                    if (_gold >= 50)
                    {
                        setGold(-50);
                        _LobbyManager.GetComponent<LobbyManager>().updateStatus(0.0f, 0.0f, 0.0f, 0.5f);
                        _purchaseMessage.SetActive(true);
                    }
                    break;
                }
            case 4:
                {
                    bool flag = true;
                    for (int i = 0; i < _LobbyManager.GetComponent<LobbyManager>().weaponList.Count; ++i)
                    {
                        if (_LobbyManager.GetComponent<LobbyManager>().weaponList[i] == 1)
                            flag = false;
                    }

                    if (_gold >= 50 && flag)
                    {
                        setGold(-50);
                        _LobbyManager.GetComponent<LobbyManager>().updateStatus(0.0f, 0.0f, 0.0f, 0.0f, 1);
                        _purchaseMessage.SetActive(true);
                    }
                    break;
                }

        }
    }

    public void ListUpAlreadyHave()
    {
        _alreadyHave = new List<int>();

        string userID = DataStreamToStage.Instance.getID();
        string stream = PlayerPrefs.GetString(userID + "PlayerAllowedSkills");
        string temp = "";

        for (int i = 0; i < stream.Length; ++i)
        {
            if (stream[i] == ' ')
            {
                if (temp != "")
                    _alreadyHave.Add(int.Parse(temp));
                temp = "";
            }
            else
                temp += stream[i];
        }
    }

    public void OpenNewSkill(int id)
    {
        if(_alreadyHave != null)
        {
            for (int i = 0; i < _alreadyHave.Count; ++i)
                if (_alreadyHave[i] == id)
                {
                    _alreadyHaveMessage.SetActive(true);
                    return;
                }
        }

        switch (id)
        {
            case 7:
                {
                    if (_gold >= 50)
                    {
                        setGold(-50);
                        _alreadyHave.Add(id);
                        _purchaseMessage.SetActive(true);
                    }
                    break;
                }
            case 8:
                {
                    if (_gold >= 50)
                    {
                        setGold(-50);
                        _alreadyHave.Add(id);
                        _purchaseMessage.SetActive(true);
                    }
                    break;
                }
            case 9:
                {
                    if (_gold >= 50)
                    {
                        setGold(-50);
                        _alreadyHave.Add(id);
                        _purchaseMessage.SetActive(true);
                    }
                    break;
                }
            case 10:
                {
                    if (_gold >= 50)
                    {
                        setGold(-50);
                        _alreadyHave.Add(id);
                        _purchaseMessage.SetActive(true);
                    }
                    break;
                }
            case 11:
                {
                    if (_gold >= 50)
                    {
                        setGold(-50);
                        _alreadyHave.Add(id);
                        _purchaseMessage.SetActive(true);
                    }
                    break;
                }
            case 12:
                {
                    if (_gold >= 50)
                    {
                        setGold(-50);
                        _alreadyHave.Add(id);
                        _purchaseMessage.SetActive(true);
                    }
                    break;
                }
            case 13:
                {
                    if (_gold >= 50)
                    {
                        setGold(-50);
                        _alreadyHave.Add(id);
                        _purchaseMessage.SetActive(true);
                    }
                    break;
                }
            case 14:
                {
                    if (_gold >= 50)
                    {
                        setGold(-50);
                        _alreadyHave.Add(id);
                        _purchaseMessage.SetActive(true);
                    }
                    break;
                }
        }

        string str = "";
        for(int i = 0; i < _alreadyHave.Count; ++i)
        {
            if (i == 0)
                str += _alreadyHave[i].ToString() + " ";
            else
                str += " " + _alreadyHave[i].ToString() + " ";
        }

        string userID = DataStreamToStage.Instance.getID();
        PlayerPrefs.SetString(userID + "PlayerAllowedSkills", str);
    }

    public void updateGoldData()
    {
        List<string> statusList = new List<string>();
        if(DataStreamToStage.Instance != null)
        {
            string userID = DataStreamToStage.Instance.getID();
            string temp = PlayerPrefs.GetString(userID + "PlayerSetting");
            string temp2 = "";

            for (int i = 0; i < temp.Length; ++i)
            {
                if (temp[i] == ' ')
                {
                    statusList.Add(temp2);
                    temp2 = "";
                }
                else
                    temp2 += temp[i];
            }

            statusList[5] = _gold.ToString();
            PlayerPrefs.SetString(userID + "PlayerSetting", statusList[0] + " " +
                statusList[1] + " " + statusList[2] + " " + statusList[3] + " " +
                statusList[4] + " " + statusList[5] + " ");
        }
    }
}
