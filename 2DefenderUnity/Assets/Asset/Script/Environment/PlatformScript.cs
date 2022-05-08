using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    bool playerCheck = false;
    PlatformEffector2D platformObject = null;

    void Start()
    {
        playerCheck = false;
        platformObject = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift) && playerCheck)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                platformObject.rotationalOffset = 180.0f;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                platformObject.rotationalOffset = 0.0f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name.Contains("Player"))
            playerCheck = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Player"))
        {
            playerCheck = false;
        }
    }
}
