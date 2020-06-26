using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularMesh : MonoBehaviour {

    [ContextMenu("Convert To Regular Mesh")]
	void Convert()
    {
        SkinnedMeshRenderer smr = GetComponent<SkinnedMeshRenderer>();
        MeshRenderer mr = gameObject.AddComponent<MeshRenderer>();
        MeshFilter mf = gameObject.AddComponent<MeshFilter>();

        mf.sharedMesh = smr.sharedMesh;
        mr.sharedMaterials = smr.sharedMaterials;

        DestroyImmediate(smr);
        DestroyImmediate(this);
    }
}
