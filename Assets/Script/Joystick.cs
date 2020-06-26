using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {

    Vector2 input = Vector2.zero;
    public float handleLimit = 1f;

    public RectTransform bg;
    public RectTransform handle;

    public float horizontal { get { return input.x; } }
    public float vertical { get { return input.y; } }
    public Vector2 direction { get { return new Vector2(horizontal, vertical); } }

    Vector2 joystickPosition = Vector2.zero;
    private Camera cam = new Camera();

    #region Singleton
    public static Joystick singleton;

    private void Awake()
    {
        singleton = this;
    }
    #endregion

    private void Start()
    {
        joystickPosition = RectTransformUtility.WorldToScreenPoint(cam, bg.position);
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 _direction = eventData.position - joystickPosition;
        input = (_direction.magnitude > bg.sizeDelta.x / 2f) ? _direction.normalized : _direction / (bg.sizeDelta.x / 2f);
        handle.anchoredPosition = (input * bg.sizeDelta.x / 2f) * handleLimit;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        input = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }
}
