using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BonusGroupControl : MonoBehaviour
{
    public GameObject bonusTop1;
    public GameObject bonusTop2;
    public GameObject bonusTop3;

    public void GetBonus(int a)
    {
        if (a == 1)
        {
            bonusTop1.SetActive(true);
            bonusTop1.transform.DOLocalMoveY(0, 0.5f).SetEase(Ease.Linear);
        }
        else if (a==2)
        {
            bonusTop2.SetActive(true);
            bonusTop2.transform.DOLocalMoveY(0, 0.5f).SetEase(Ease.Linear);
        }
        else
        {
            bonusTop3.SetActive(true);
            bonusTop3.transform.DOLocalMoveY(0, 0.5f).SetEase(Ease.Linear);
        }
    }
}
