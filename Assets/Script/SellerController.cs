using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellerController : MonoBehaviour {

    public UsableItem compost;
    public GameObject sellingPanel;
    public GameObject soldPanel;
    public GameObject notSoldPanel;
    public GameObject newPanel;

    private GameObject currentPanel;

    private void OnTriggerEnter(Collider other)
    {
        sellingPanel.SetActive(true);
        currentPanel = sellingPanel;
    }

    private void OnTriggerExit(Collider other)
    {
        currentPanel.SetActive(false);
    }

    public void SetNewSellingPanel(GameObject newSP)
    {
        sellingPanel = newSP;
    }

    public void BuyCompost()
    {
        currentPanel.SetActive(false);
        if (GameController.instance.GiveCompost(compost))
        {
            soldPanel.SetActive(true);
            currentPanel = soldPanel;
            sellingPanel = newPanel;
        }
        else
        {
            notSoldPanel.SetActive(true);
            currentPanel = notSoldPanel;
        }
        
    }
}
