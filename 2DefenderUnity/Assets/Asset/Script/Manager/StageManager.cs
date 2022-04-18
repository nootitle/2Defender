using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance = null;
    [SerializeField] GameObject _rewardWindow = null;

    int killCount;
    public bool pause = false;
    public void setKillCount(int value) { killCount += value; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        killCount = 0;
    }

    void Update()
    {
        showRewardWindow();
        stage1();
    }

    void stage1()
    {
        if (killCount != 0 && killCount % 5 == 0)
        {
            EnemyManager.Instance.setSpawnLevel(EnemyManager.Instance.getSpawnLevel() + 1);
            EnemyManager.Instance.setMaxEnemy(EnemyManager.Instance.getMaxEnemy() + 1);
            killCount = 0;
        }
    }

    public void showRewardWindow()
    {
        if(killCount != 0 && killCount % 5 == 0)
        {
            if (!_rewardWindow.activeSelf)
                _rewardWindow.SetActive(true);
            pause = true;
        }
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
}
