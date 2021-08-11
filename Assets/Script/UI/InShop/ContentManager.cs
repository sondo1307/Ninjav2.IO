using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentManager : MonoBehaviour
{
    public int tempOutline;
    private void Start()
    {
        if (transform.CompareTag("OutlineSkin1"))
        {
            tempOutline = GameDataManager.Instance.gameDataScrObj.outlineSkin1Cur;
            transform.GetChild(tempOutline).GetComponentInChildren<SkinBtnClick>().SetOutline(true);
        }
        else
        {
            tempOutline = GameDataManager.Instance.gameDataScrObj.outlineSkin2Cur;
            transform.GetChild(tempOutline).GetComponentInChildren<CapsuleSkinBtnClick>().SetOutline(true);
        }
    }

    public void SetOutlineChildOff(int a)
    {
        if (transform.GetChild(tempOutline).GetComponentInChildren<SkinBtnClick>())
        {
            if (a!= tempOutline)
            {
                transform.GetChild(tempOutline).GetComponentInChildren<SkinBtnClick>().SetOutline(false);
                tempOutline = a;
            }
        }
        else
        {
            if (a != tempOutline)
            {
                transform.GetChild(tempOutline).GetComponentInChildren<CapsuleSkinBtnClick>().SetOutline(false);
                tempOutline = a;
            }
        }
    }    
    public void ForceSetOutlineChildOff(int a)
    {
        if (transform.GetChild(tempOutline).GetComponentInChildren<SkinBtnClick>())
        {
            transform.GetChild(tempOutline).GetComponentInChildren<SkinBtnClick>().SetOutline(false);
            tempOutline = GameDataManager.Instance.gameDataScrObj.outlineSkin1Cur;
        }
        else
        {

            transform.GetChild(tempOutline).GetComponentInChildren<CapsuleSkinBtnClick>().SetOutline(false);
            tempOutline = GameDataManager.Instance.gameDataScrObj.outlineSkin2Cur;
        }
    }
}
