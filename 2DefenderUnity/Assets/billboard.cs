using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billboard : MonoBehaviour
{
    [SerializeField] GameObject _player = null;
    float zValue = 0;

    void Start()
    {

    }

    void Update()
    {
        this.transform.position = new Vector3(_player.transform.position.x,
            this.transform.position.y, this.transform.position.z);
    }
}
