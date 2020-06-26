using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingController : MonoBehaviour {

    public Transform resetPos;

    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = resetPos.position;
    }


}
