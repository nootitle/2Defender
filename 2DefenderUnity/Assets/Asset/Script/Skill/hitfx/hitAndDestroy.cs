using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitAndDestroy : MonoBehaviour
{
    [SerializeField] float _destroyTime = 3.0f;
    [SerializeField] AudioSource _SE = null;
    [SerializeField] bool _isStickToPlayer = false;
    [SerializeField] GameObject _player = null;

    void Start()
    {
        if (_SE != null) _SE.Play();
        StartCoroutine(selfDestroy());
        if(_player == null)
            _player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (_isStickToPlayer)
            this.transform.position = _player.transform.position;

    }

    IEnumerator selfDestroy()
    {
        yield return new WaitForSeconds(_destroyTime);

        Destroy(this.gameObject);
    }
}
