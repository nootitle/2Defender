using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectManager : MonoBehaviour
{
    [SerializeField] GameObject _stageSelectCanvas = null;
    [SerializeField] GameObject _lobbyCanvas = null;

    public void GoToLobby()
    {
        _lobbyCanvas.SetActive(true);
        _stageSelectCanvas.SetActive(false);
    }
}
