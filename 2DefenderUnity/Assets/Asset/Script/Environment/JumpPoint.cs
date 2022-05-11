using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPoint : MonoBehaviour
{
    [SerializeField] GameObject next = null;

    public GameObject nextPos()
    {
        return next;
    }
}
