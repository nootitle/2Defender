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

    [SerializeField] int _gold = 0;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    public void setGold(int gold)
    {
        _gold += gold;
        _goldText.text = _gold.ToString();
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
}
