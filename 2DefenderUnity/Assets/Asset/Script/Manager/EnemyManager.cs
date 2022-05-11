using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    static public EnemyManager Instance = null;

    [SerializeField] List<GameObject> EnemyList = null;
    [SerializeField] List<GameObject> SpawnPoints = null;
    [SerializeField] int maxEnemy = 5;
    int EnemyNum;
    int spawnLevel;
    [SerializeField] bool _respawnStop = false;
    public bool getRespawnStop() { return _respawnStop; }
    public void setRespawnStop(bool value) { _respawnStop = value; }

    public int getSpawnLevel() { return spawnLevel; }
    public void setSpawnLevel(int level)
    {
        if(level <= EnemyList.Count)
            spawnLevel = level;
    }
    public int getMaxEnemy() { return maxEnemy; }
    public void setMaxEnemy(int value)
    {
        if (value <= EnemyList.Count &&
            value <= spawnLevel)
            maxEnemy = value;
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        if (maxEnemy > EnemyList.Count)
            maxEnemy = EnemyList.Count;
        EnemyNum = maxEnemy;
        spawnLevel = maxEnemy;
    }

    void Update()
    {
        if(!_respawnStop)
            spawn();
    }

    public void spawn()
    {
        if(EnemyNum < maxEnemy)
        {
            for (int i = 0; i < spawnLevel; ++i)
            {
                if (!EnemyList[i].activeSelf)
                {
                    ++EnemyNum;
                    EnemyList[i].SetActive(true);
                    int rnd = Random.Range(0, SpawnPoints.Count - 1);
                    EnemyList[i].transform.position = SpawnPoints[rnd].transform.position;
                    EnemyList[i].GetComponent<Enemy_Hit>().respawn();

                    if (EnemyNum >= maxEnemy)
                        break;
                }
            }
        }
    }

    public void deathCount()
    {
        --EnemyNum;
        StageManager.Instance.setKillCount(1);
    }
}
