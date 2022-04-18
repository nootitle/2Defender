using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billboard : MonoBehaviour
{
    [SerializeField] GameObject _player = null;
    [SerializeField] float _cameraSpeed = 5.0f;

    void Start()
    {

    }

    void Update()
    {
        this.transform.position = new Vector3(Mathf.Lerp(this.transform.position.x,
            _player.transform.position.x, _cameraSpeed * Time.deltaTime), 
            this.transform.position.y, this.transform.position.z);
    }
}
