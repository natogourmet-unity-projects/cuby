using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dropper : MonoBehaviour
{

    public static Dropper instance;
    private void Awake()
    {
        instance = this;
    }

    public bool OnDrop()
    {
        RectTransform panel = transform as RectTransform;

        return (!RectTransformUtility.RectangleContainsScreenPoint(panel, Input.mousePosition));
    }

    public void Test()
    {
        Debug.Log("It's Clicked");
    }
}

