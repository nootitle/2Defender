using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragController : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Canvas _Canvas;
    private RectTransform _RectTransform;
    private CanvasGroup _CanvasGroup;

    void GetParent(Transform _transform)
    {
        _Canvas = _transform.parent.GetComponent<Canvas>();

        if(_Canvas == null)
        {
            GetParent(_transform.parent);
        }
    }

    void Awake()
    {
        _RectTransform = GetComponent<RectTransform>();
        _CanvasGroup = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        GetParent(transform);
    }

    void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //드롭할 때, 슬롯과 상호작용을 하려면 드래그 시의 blocksRaycasts를 잠깐 꺼놔야 한다.
        //드래그할 오브젝트에 캔버스 그룹 컴포넌트를 추가하여 제어할 수 있다.
        _CanvasGroup.alpha = 0.5f;
        _CanvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {

        _RectTransform.anchoredPosition += eventData.delta / _Canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _CanvasGroup.alpha = 1.0f;
        _CanvasGroup.blocksRaycasts = true;
    }
}
