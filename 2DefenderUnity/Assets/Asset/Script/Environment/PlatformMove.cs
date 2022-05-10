using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    [SerializeField] float _speed = 5.0f;
    [SerializeField] bool _isUp = true;
    [SerializeField] float _range = 5.0f;
    Vector3 _center = Vector3.zero;
    bool dir = false;
    private void Start()
    {
        _center = this.transform.position;
    }
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (_isUp)
        {
            if (dir)
            {
                if (this.transform.position.y > _center.y + _range)
                    dir = false;
                else
                    this.transform.position += Vector3.up * _speed * Time.deltaTime;
            }
            else
            {
                if (this.transform.position.y < _center.y - _range)
                    dir = true;
                else
                    this.transform.position += Vector3.down * _speed * Time.deltaTime;
            }
        }
        else
        {
            if (dir)
            {
                if (this.transform.position.x > _center.x + _range)
                    dir = false;
                else
                    this.transform.position += Vector3.right * _speed * Time.deltaTime;
            }
            else
            {
                if (this.transform.position.x < _center.x - _range)
                    dir = true;
                else
                    this.transform.position += Vector3.left * _speed * Time.deltaTime;
            }
        }
    }
}
