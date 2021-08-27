using Firebase.Analytics;
using System;
using UnityEngine;

public class FirebaseAnalystic : MonoBehaviour
{
    public static FirebaseAnalystic Instance;
    public enum AdsType
    {
        Video,
        Inter,
        Banner
    }

    public enum RewardPos
    {
        TrySkin,
        Revive,
        X5_Gold,
        X2_Gold,
        BuySkin,
        BuyHealth,
        TrySkinInGame,
        SuckMoney
    }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }

    ///Tracking

    #region FAT BOY & SLIM GIRL
    public void PlayLevel(int level)
    {
#if UNITY_ANDROID || UNITY_IOS
        Firebase.Analytics.FirebaseAnalytics.LogEvent("Level_Play", "Level", (level).ToString());
#endif
    }

    public void WinLevel(int level)
    {
#if UNITY_ANDROID || UNITY_IOS
        Firebase.Analytics.FirebaseAnalytics.LogEvent("Level_Win", "Level", (level).ToString());

#endif
    }
    public void LoseLevel(int level)
    {
#if UNITY_ANDROID || UNITY_IOS
        Firebase.Analytics.FirebaseAnalytics.LogEvent("Level_Lose", "Level", (level).ToString());
#endif
    }

    public void ClickAds()
    {
#if UNITY_ANDROID || UNITY_IOS
        Firebase.Analytics.FirebaseAnalytics.LogEvent("Click_Inter");
#endif
    }
    #endregion

    //    public void ShowInterAdsSuccess()
    //    {
    //#if UNITY_ANDROID || UNITY_IOS

    //        Firebase.Analytics.FirebaseAnalytics.LogEvent("Show_Inter_Ads_success");
    //#endif
    //    }



    public void ShowAdsSuccess(AdsType adsType)
    {

#if UNITY_ANDROID || UNITY_IOS

        Firebase.Analytics.FirebaseAnalytics.LogEvent("Show_Ads", "Ads_Type", adsType.ToString());

        if (adsType == AdsType.Inter)
        {
            Firebase.Analytics.FirebaseAnalytics.LogEvent("Show_Inter");
        }
#endif
    }



    public void LoadAdsFail(AdsType adsType)
    {
#if UNITY_ANDROID || UNITY_IOS

        Firebase.Analytics.FirebaseAnalytics.LogEvent("Load_Ads_Fail", "Ads_Type", adsType.ToString());
#endif
    }

    public void DontShowAds()
    {
#if UNITY_ANDROID || UNITY_IOS

        Firebase.Analytics.FirebaseAnalytics.LogEvent("Not_Show_Ads");
#endif
    }

    public void ShowRewardAt(RewardPos pos)
    {
#if UNITY_ANDROID || UNITY_IOS

        Firebase.Analytics.FirebaseAnalytics.LogEvent("RewardPos", "Position", pos.ToString());
#endif
    }
}

