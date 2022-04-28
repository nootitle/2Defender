using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance = null;
    [SerializeField] GameObject _rewardWindow = null;
    [SerializeField] GameObject _boss = null;
    [SerializeField] GameObject _readySign = null;

    int killCount;
    public bool pause = false;
    public void setKillCount(int value) 
    { 
        killCount += value;
        StorageManager.Instance.setGold(value * 10);
        StorageManager.Instance.addGoldDelta(value * 10);
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        killCount = 0;
        _readySign.SetActive(true);
    }

    void Update()
    {
        stage1();
    }

    void stage1()
    {
        if (killCount != 0 && killCount % 20 == 0 && killCount <= 100)
        {
            EnemyManager.Instance.setSpawnLevel(EnemyManager.Instance.getSpawnLevel() + 1);
            EnemyManager.Instance.setMaxEnemy(EnemyManager.Instance.getMaxEnemy() + 1);
            showRewardWindow();
        }
        if (killCount >= 110 && !EnemyManager.Instance.getRespawnStop())
        {
            _boss.SetActive(true);
            EnemyManager.Instance.setRespawnStop(true);
        }

    }

    public void showRewardWindow()
    {
        if (!_rewardWindow.activeSelf)
            _rewardWindow.SetActive(true);
        pause = true;
    }

    public void offRewardWindow()
    {
        if (_rewardWindow.activeSelf)
        {
            ++killCount;
            _rewardWindow.SetActive(false);
            pause = false;
        }
    }

    public void ReturnToMenu()
    {
        offRewardWindow();
        StorageManager.Instance.updateGoldData();
        if (DataStreamToStage.Instance != null)
            DataStreamToStage.Instance.ReturnToMenu();
        else
            SceneManager.LoadScene(0);
    }
}
