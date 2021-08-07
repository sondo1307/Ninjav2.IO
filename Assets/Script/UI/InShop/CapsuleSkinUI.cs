using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleSkinUI : MonoBehaviour
{
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    public MeshFilter curMeshF;
    public MeshRenderer curMeshR;

    private void Awake()
    {
        curMeshF = meshFilter;
        curMeshR = meshRenderer;
    }

    public void SetCapsuleSkin(Mesh a, Material b)
    {
        meshFilter.mesh = a;
        meshRenderer.material = b;
    }
}
