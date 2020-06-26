using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldMan : MonoBehaviour
{
    public GameObject currentPanel;
    public GameObject stuckPanel;
    public GameObject gotItPanel;
    

    private void OnTriggerEnter(Collider other)
    {
        currentPanel.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        currentPanel.SetActive(false);
    }

    public void SetStuck()
    {
        currentPanel = stuckPanel;
    }

    public void SetGotIt()
    {
        currentPanel = gotItPanel;
    }

    
}
