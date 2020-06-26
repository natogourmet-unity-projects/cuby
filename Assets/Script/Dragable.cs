using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dragable : MonoBehaviour, IDragHandler, IEndDragHandler {

    RectTransform panel;

    private void Start()
    {
        panel = transform as RectTransform;

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;

        GameController.instance.SetDragging(RectTransformUtility.RectangleContainsScreenPoint(panel, Input.mousePosition), transform.parent.gameObject);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameController.instance.OnDrop(transform.parent.gameObject);
        transform.localPosition = Vector3.zero;
        GameController.instance.SetDragging(false, null);
    }

}
