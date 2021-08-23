using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchAdBtn : MonoBehaviour
{
    public enum RewardAdMode
    {
        X5,
        PlusCoin,
        Key,
        BuySkin,
    };

    [SerializeField] private RewardAdMode mode;

    public void OnClick()
    {
        if (mode == RewardAdMode.X5)
        {
            MyScene.Instance.lastRewardAdMode = MyScene.RewardAdMode.X5;
            AdsManager.instance.ShowVideoAds(FirebaseAnalystic.RewardPos.X5_Gold);
        }
        else if (mode == RewardAdMode.PlusCoin)
        {
            MyScene.Instance.lastRewardAdMode = MyScene.RewardAdMode.PlusCoin;
            AdsManager.instance.ShowVideoAds(FirebaseAnalystic.RewardPos.SuckMoney);
        }
        else if (mode == RewardAdMode.Key)
        {
            MyScene.Instance.lastRewardAdMode = MyScene.RewardAdMode.Key;
            AdsManager.instance.ShowVideoAds(FirebaseAnalystic.RewardPos.X2_Gold);
        }
        else if (mode == RewardAdMode.BuySkin)
        {
            MyScene.Instance.lastRewardAdMode = MyScene.RewardAdMode.BuySkin;
            AdsManager.instance.ShowVideoAds(FirebaseAnalystic.RewardPos.BuySkin);
        }
    }
}
