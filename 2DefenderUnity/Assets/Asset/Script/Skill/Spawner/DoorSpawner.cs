using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSpawner : MonoBehaviour
{
    [SerializeField] GameObject _player = null;
    [SerializeField] GameObject _spawn = null;
    [SerializeField] float _spawnTime = 2.5f;
    [SerializeField] float _destroyTime = 2.5f;
    [SerializeField] AudioSource _se = null;
    Animator _an = null;
    bool _isMoving = false;
    bool _isOpen = false;
    bool _getDown = false;

    private void Start()
    {
        _an = this.GetComponent<Animator>();
    }

    private void Update()
    {
        if(Vector3.Distance(_player.transform.position, this.transform.position) < 10.0f && !_isMoving)
        {
            _isMoving = true;
        }

        if(_isMoving && !_isOpen)
        {
            if(this.transform.position.y <= -4.2f)
            {
                this.transform.position += Vector3.up * 5.0f * Time.deltaTime;
            }
            else
            {
                this.transform.position = new Vector3(this.transform.position.x, -4.2f, this.transform.position.z);
                _isOpen = true;
                StartCoroutine(spawnProcess());
            }
        }

        if(_getDown)
        {
            if (this.transform.position.y >= -15.0f)
            {
                this.transform.position -= Vector3.up * 5.0f * Time.deltaTime;
            }
            else
            {
                this.transform.position = new Vector3(this.transform.position.x, -15.0f, this.transform.position.z);
                _getDown = false;
                Destroy(this.gameObject);
            }
        }
    }

    public void spawn()
    {
        StartCoroutine(spawnProcess());
    }

    IEnumerator spawnProcess()
    {
        _spawn.SetActive(true);
        _spawn.transform.position = this.transform.position;

        yield return new WaitForSeconds(_spawnTime);
        _se.Play();
        _an.SetTrigger("Open");


        yield return new WaitForSeconds(_destroyTime);

        _se.Play();
        _getDown = true;
    }
}
