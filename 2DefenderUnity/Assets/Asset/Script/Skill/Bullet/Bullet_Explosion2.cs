using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Explosion2 : MonoBehaviour
{
    [SerializeField] float _splashDamage = 10.0f;
    [SerializeField] float _radius = 10.0f;
    [SerializeField] float _explosionDuration = 5.0f;
    [SerializeField] GameObject _hitFx = null;
    [SerializeField] bool _hitFxExist = false;
    Coroutine _co = null;

    private void OnEnable()
    {
        explosion();
    }

    void explosion()
    {
        Collider2D[] col = Physics2D.OverlapCircleAll(this.transform.position, _radius);
        foreach (Collider2D c in col)
        {
            Enemy_Hit EH = c.transform.gameObject.GetComponent<Enemy_Hit>();
            if (EH != null)
            {
                EH.Hit(_splashDamage);
                if (_hitFxExist)
                {
                    GameObject gm = Instantiate(_hitFx);
                    gm.transform.position = c.transform.position;
                }
            }
        }

        if (_co != null) StopCoroutine(_co);
        _co = StartCoroutine(disable());
    }

    IEnumerator disable()
    {
        yield return new WaitForSeconds(_explosionDuration);

        this.transform.parent.gameObject.SetActive(false);
    }
}
