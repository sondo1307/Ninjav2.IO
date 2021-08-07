using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoadSkin : MonoBehaviour
{
    public SkinnedMeshRenderer skin1;
    public MeshFilter skin2_1;
    public MeshRenderer skin2_2;

    private void Awake()
    {
    }

    private void Start()
    {
        skin1.sharedMesh = GameDataManager.Instance.gameDataScrObj.skin1Mesh;
        skin1.sharedMaterial = GameDataManager.Instance.gameDataScrObj.skin1Material;
        skin2_1.sharedMesh = GameDataManager.Instance.gameDataScrObj.skin2MeshFilterMesh;
        skin2_2.sharedMaterial = GameDataManager.Instance.gameDataScrObj.skin2MeshRendererMat;
    }


    public void SetRealSkin(Mesh a, Material b)
    {
        skin1.sharedMesh = a;
        skin1.sharedMaterial = b;
    }

    public void SetRealCapsuleSkin(Mesh a, Material b)
    {
        skin2_1.sharedMesh = a;
        skin2_2.sharedMaterial = b;
    }
}
