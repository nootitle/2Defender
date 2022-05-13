using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Missile1 : MonoBehaviour
{
    [SerializeField] GameObject _bulletBody = null;
    [SerializeField] GameObject _explosion = null;
    [SerializeField] AudioSource _SE = null;
    [SerializeField] float _turnTime = 1.0f;
    [SerializeField] float _turnPower = 5.0f;
    bool _start = false;
    bool _turnDir = false;
    float _rotatePower = 50.0f;
    [SerializeField] Rigidbody2D _rb = null;

    private void Start()
    {
        _rb = this.GetComponent<Rigidbody2D>();
        _start = true;

        int rnd = Random.Range(0, 100);
        if (rnd < 50)
        {
            _turnDir = false;
            _rotatePower = 50.0f;
            _rb.AddForce(Vector2.left * _turnPower + Vector2.up * _turnPower, ForceMode2D.Impulse);
        }
        else
        {
            _turnDir = true;
            _rotatePower = 50.0f;
            _rb.AddForce(Vector2.right * _turnPower + Vector2.up * _turnPower, ForceMode2D.Impulse);
        }
    }

    private void Update()
    {
        if(!_turnDir)
        {
            this.transform.Rotate(Vector3.forward * _rotatePower * Time.deltaTime);
        }
        else
        {
            this.transform.Rotate(Vector3.back * _rotatePower * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_start)
        {
            this.transform.rotation = Quaternion.Euler(Vector3.zero);
            _SE.Play();
            _bulletBody.SetActive(false);
            _explosion.SetActive(true);
            _start = false;
            StartCoroutine(selfDestroy());
        }
    }

    IEnumerator selfDestroy()
    {
        yield return new WaitForSeconds(1.0f);

        Destroy(this.gameObject);
    }
}
