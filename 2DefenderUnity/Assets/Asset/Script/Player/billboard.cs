using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billboard : MonoBehaviour
{
    [SerializeField] GameObject _target = null;
    [SerializeField] float _cameraSpeed = 5.0f;
    bool _isActive = true;
    public void setActive(bool value) { _isActive = value; }
    public bool isActive(bool value) { return _isActive; }

    void Update()
    {
        if(_isActive)
        {
            this.transform.position = new Vector3(Mathf.Lerp(this.transform.position.x,
                _target.transform.position.x, _cameraSpeed * Time.deltaTime),
                this.transform.position.y, this.transform.position.z);
        }
    }

    public void changeTarget(GameObject gm)
    {
        _target = gm;
    }
}
