using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
{
    [SerializeField] private RectTransform Stick;
    [SerializeField] private RectTransform BackBoard;
    private float Speed;

    private void Awake()
    {
        if(Stick == null)
            Stick = this.GetComponent<RectTransform>();
        if(BackBoard == null)
            BackBoard = this.transform.GetChild(0).GetComponent<RectTransform>();
    }

    void Start()
    {
        Speed = 0.0f;
    }

    void GetInput(Vector2 _Position)
    {
        Stick.localPosition = new Vector2(Mathf.Clamp(_Position.x - BackBoard.position.x, -30.0f, 30.0f),
            Mathf.Clamp(_Position.y - BackBoard.position.y, -30.0f, 30.0f));

        Speed = _Position.x - BackBoard.position.x;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.position.x < (Screen.width >> 1))
        {
            BackBoard.position = Input.mousePosition;
        }

        GetInput(eventData.position);       
    }

    public void OnDrag(PointerEventData eventData) 
    {
        GetInput(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Stick.localPosition = Vector2.zero;
        BackBoard.position = new Vector3(150.0f, 150.0f, 0.0f);
        Speed = 0.0f;
    }

    public float GetSpeed()
    {
        return Speed;
    }
}
