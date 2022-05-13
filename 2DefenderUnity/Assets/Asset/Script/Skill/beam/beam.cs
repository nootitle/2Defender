using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beam : MonoBehaviour
{
    [SerializeField] float _attackPower = 10.0f;
    [SerializeField] float _speed = 5.0f;
    [SerializeField] AudioSource _se = null;
    Coroutine _co = null;

    private void OnEnable()
    {
        _se.Play();
        if (_co != null) StopCoroutine(_co);
        _co = StartCoroutine(selfDisable());
    }

    void Update()
    {
        rotateSelf();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            Player PL = collision.gameObject.GetComponent<Player>();
            if(PL != null)
                PL.Damaged(_attackPower);
        }
        if(collision.gameObject.layer == 13)
        {
            Alies AL = collision.gameObject.GetComponent<Alies>();
            if (AL != null)
                AL.Hit(_attackPower);
        }
    }

    void rotateSelf()
    {
        float z = Mathf.Clamp(this.transform.eulerAngles.z + _speed * Time.deltaTime, 0.0f, 360.0f);
        this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y, z);
    }

    IEnumerator selfDisable()
    {
        yield return new WaitForSeconds(2.0f);

        this.gameObject.SetActive(false);
    }
}
