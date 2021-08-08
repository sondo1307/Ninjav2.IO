using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitShopBtn : MonoBehaviour
{
    public OutlineScript outlineScript1;
    public OutlineScript outlineScript2;
    public PlayerSkinUI playerSkinUI1;
    public CapsuleSkinUI capsuleSkinUI;

    public void ExitShopBtnClick()
    {
        outlineScript1.SetPosition(outlineScript1.currentFatherObj.transform.position, outlineScript1.currentFatherObj.transform);
        playerSkinUI1.SetSkin(GameDataManager.Instance.gameDataScrObj.skin1Mesh, GameDataManager.Instance.gameDataScrObj.skin1Material);
        outlineScript2.SetPosition(outlineScript2.currentFatherObj.transform.position, outlineScript2.currentFatherObj.transform);
        capsuleSkinUI.SetCapsuleSkin(GameDataManager.Instance.gameDataScrObj.skin2MeshFilterMesh, GameDataManager.Instance.gameDataScrObj.skin2MeshRendererMat);
    }
}
