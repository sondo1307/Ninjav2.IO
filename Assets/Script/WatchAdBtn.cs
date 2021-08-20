using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchAdBtn : MonoBehaviour
{

    private void Start()
    {
    }
    public void OnClick()
    {
        AdsManager.instance.ShowVideoAds(FirebaseAnalystic.RewardPos.SuckMoney);
    }
}
