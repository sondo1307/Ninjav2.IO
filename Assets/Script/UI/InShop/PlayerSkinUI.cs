using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkinUI : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer skin1;
    public Mesh curMesh;
    public Material curMat;

    private void Start()
    {
        curMesh = skin1.sharedMesh;
        curMat = skin1.sharedMaterial;
    }

    public void SetSkin(Mesh a, Material b)
    {
        skin1.sharedMesh = a;
        skin1.sharedMaterial = b;
    }
}
