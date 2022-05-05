using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] GameObject _fx = null;
    [SerializeField] float _damage = 15.0f;
    [SerializeField] float _radius = 2.0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D[] co = Physics2D.OverlapCircleAll(this.transform.position, _radius);
        for(int i = 0; i < co.Length; ++i)
        {
            Player PL = collision.gameObject.GetComponent<Player>();
            if(PL != null)
            {
                PL.Damaged(_damage);
            }

            Alies AL = collision.gameObject.GetComponent<Alies>();
            if(AL != null)
            {
                AL.Hit(_damage);
            }
        }

        GameObject gm = Instantiate(_fx);
        gm.transform.position = this.transform.position;
        Destroy(this.gameObject);
    }
}
