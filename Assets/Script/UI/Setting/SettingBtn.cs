using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SettingBtn : MonoBehaviour
{
    private bool check = true;

    public void SettingBtnClick()
    {
        if (check)
        {
            transform.parent.GetComponentInParent<RectTransform>().DOAnchorPosX(-5, 0.15f).SetEase(Ease.Linear);
            check = false;
        }
        else if (!check)
        {
            transform.parent.GetComponentInParent<RectTransform>().DOAnchorPosX(-364, 0.15f).SetEase(Ease.Linear);
            check = true;
        }
    }
}
