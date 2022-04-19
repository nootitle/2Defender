using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconDescription : MonoBehaviour
{
    [SerializeField] GameObject _description = null;

    void Update()
    {
        if (Input.mousePosition.x > this.transform.position.x - 35.0f && Input.mousePosition.x < this.transform.position.x + 35.0f &&
            Input.mousePosition.y > this.transform.position.y - 35.0f && Input.mousePosition.y < this.transform.position.y + 35.0f)
        {
            _description.SetActive(true);
            _description.transform.position = Input.mousePosition + new Vector3(5.0f, -5.0f, 0.0f);
        }
        else
            _description.SetActive(false);
    }
}
