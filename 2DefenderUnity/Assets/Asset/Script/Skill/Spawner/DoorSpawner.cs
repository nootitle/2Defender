using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSpawner : MonoBehaviour
{
    [SerializeField] GameObject _spawn = null;
    [SerializeField] float _spawnTime = 2.5f;
    [SerializeField] float _destroyTime = 2.5f;

    private void OnEnable()
    {
        StartCoroutine(spawnProcess());
    }

    public void spawn()
    {
        StartCoroutine(spawnProcess());
    }

    IEnumerator spawnProcess()
    {
        yield return new WaitForSeconds(_spawnTime);

        _spawn.SetActive(true);
        _spawn.transform.position = this.transform.position;

        yield return new WaitForSeconds(_destroyTime);

        Destroy(this.gameObject);
    }
}
