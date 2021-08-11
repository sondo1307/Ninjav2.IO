using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitShopBtn : MonoBehaviour
{
    public ContentManager contentManager1;
    public ContentManager contentManager2;
    public PlayerSkinUI playerSkinUI1;
    public CapsuleSkinUI capsuleSkinUI;

    public void ExitShopBtnClick()
    {
        contentManager1.ForceSetOutlineChildOff(contentManager1.tempOutline);
        contentManager1.transform.GetChild(GameDataManager.Instance.gameDataScrObj.outlineSkin1Cur)
            .GetComponentInChildren<SkinBtnClick>().SetOutline(true);
        playerSkinUI1.SetSkin(GameDataManager.Instance.gameDataScrObj.skin1Mesh, GameDataManager.Instance.gameDataScrObj.skin1Material);

        contentManager2.ForceSetOutlineChildOff(contentManager2.tempOutline);
        contentManager2.transform.GetChild(GameDataManager.Instance.gameDataScrObj.outlineSkin2Cur)
            .GetComponentInChildren<CapsuleSkinBtnClick>().SetOutline(true);
        capsuleSkinUI.SetCapsuleSkin(GameDataManager.Instance.gameDataScrObj.skin2MeshFilterMesh, GameDataManager.Instance.gameDataScrObj.skin2MeshRendererMat);
    }
}
