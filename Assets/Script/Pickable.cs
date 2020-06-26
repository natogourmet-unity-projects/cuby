using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    public UsableItem item;
    public GameObject pickWindow;

    private void OnTriggerEnter(Collider other)
    {
        pickWindow.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        pickWindow.SetActive(false);
    }

    public void PickItem()
    {
        GameController.instance.GiveItem(item);
        Destroy(gameObject);
    }
}
