using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplesTrigger : MonoBehaviour
{
    private bool firstTime = true;
    
    private void OnTriggerEnter(Collider other)
    {
        GameController.instance.SeeTree(true);
    }

    private void OnTriggerExit(Collider other)
    {
        GameController.instance.SeeTree(false);

    }
}
