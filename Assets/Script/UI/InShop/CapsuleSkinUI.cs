using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleSkinUI : MonoBehaviour
{
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    //public MeshFilter curMeshF;
    //public MeshRenderer curMeshR;

    private void Awake()
    {

    }

    private void Start()
    {
        //curMeshF = meshFilter;
        //curMeshR = meshRenderer;
        SetCapsuleSkin(GameDataManager.Instance.gameDataScrObj.skin2MeshFilterMesh, GameDataManager.Instance.gameDataScrObj.skin2MeshRendererMat);
    }

    public void SetCapsuleSkin(Mesh a, Material b)
    {
        meshFilter.mesh = a;
        meshRenderer.material = b;
    }
}
