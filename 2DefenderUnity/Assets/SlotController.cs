using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotController : MonoBehaviour, IDropHandler
{
    private Canvas _Canvas;
    private RectTransform _RectTransform;

    void GetParent(Transform _transform)
    {
        _Canvas = _transform.parent.GetComponent<Canvas>();

        if (_Canvas == null)
        {
            GetParent(_transform.parent);
        }
    }

    void Awake()
    {
        _RectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        GetParent(transform);
    }

    void Update()
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition =
                _RectTransform.anchoredPosition;
            Debug.Log(_RectTransform.anchoredPosition);
        }
    }
}
