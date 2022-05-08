using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateStageSelectWindow : MonoBehaviour
{
    [SerializeField] GameObject _stage2 = null;
    [SerializeField] GameObject _stage2Lock = null;
    [SerializeField] GameObject _stage3 = null;
    [SerializeField] GameObject _stage3Lock = null;
    [SerializeField] GameObject _stage4 = null;
    [SerializeField] GameObject _stage4Lock = null;

    private void OnEnable()
    {
        string id = DataStreamToStage.Instance.getID();
        int currentMapID = PlayerPrefs.GetInt(id + "PlayerSetting_Level");

        if (currentMapID >= 3)
        {
            _stage2.SetActive(true);
            _stage3.SetActive(true);
            _stage4.SetActive(true);
            _stage2Lock.SetActive(false);
            _stage3Lock.SetActive(false);
            _stage4Lock.SetActive(false);
        }
        else if (currentMapID >= 2)
        {
            _stage2.SetActive(true);
            _stage3.SetActive(true);
            _stage2Lock.SetActive(false);
            _stage3Lock.SetActive(false);
        }
        else if(currentMapID >= 1)
        {
            _stage2.SetActive(true);
            _stage2Lock.SetActive(false);
        }
    }
}
