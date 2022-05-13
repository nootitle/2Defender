using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    [SerializeField] GameObject _fx = null;
    [SerializeField] GameObject _head = null;
    [SerializeField] float _damage = 10.0f;
    [SerializeField] AudioSource _se = null;
    Coroutine _co = null;

    public void fire(GameObject _shooter)
    {
        _se.Play();
        _head.SetActive(true);
        float rnd = Random.Range(-20.0f, 20.0f);
        this.transform.position = new Vector3(_shooter.transform.position.x
            + rnd, _shooter.transform.position.y + 10.0f, _shooter.transform.position.z);    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _fx.SetActive(true);
        _fx.transform.position = _head.transform.position;

        Player pl = collision.gameObject.GetComponent<Player>();
        if(pl != null)
        {
            pl.Damaged(_damage);
        }

        _head.SetActive(false);
        if (_co != null) StopCoroutine(_co);
        _co = StartCoroutine(selfDestroy());
    }
    
    IEnumerator selfDestroy()
    {
        yield return new WaitForSeconds(1.0f);

        _fx.SetActive(false);
        _head.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
