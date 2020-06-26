using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Usable Items/Item")]
public class UsableItem : ScriptableObject {

    public GameObject obj;
    public Sprite sprite;
    public string name;
    public LayerMask mask;
}
