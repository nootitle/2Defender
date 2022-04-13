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
    }

    void Update()
    {
        spawn();
    }

    public void spawn()
    {
        if(EnemyNum < maxEnemy)
        {
            foreach(GameObject gm in EnemyList)
            {
                if (!gm.activeSelf)
                {
                    ++EnemyNum;
                    gm.SetActive(true);
                    gm.GetComponent<Enemy_Hit>().respawn();
                    int rnd = Random.Range(0, SpawnPoints.Count - 1);
                    gm.transform.position = SpawnPoints[rnd].transform.position;
                }
            }
        }
    }

    public void deathCount()
    {
        --EnemyNum;
    }
}
