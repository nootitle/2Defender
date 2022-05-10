using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{ 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player PL = collision.gameObject.GetComponent<Player>();
        if(PL != null)
            PL.Damaged(999999999.0f);

        Alies AL = collision.gameObject.GetComponent<Alies>();
        if (AL != null)
            AL.Hit(999999999.0f);

        Enemy_Hit EH = collision.gameObject.GetComponent<Enemy_Hit>();
        if (EH != null)
            EH.Hit(999999999.0f);
    }
}
