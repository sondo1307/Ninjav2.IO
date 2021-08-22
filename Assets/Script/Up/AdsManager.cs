using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using AppsFlyerSDK;

public class AdsManager : MonoBehaviour
{
    public static AdsManager instance;
    private Action callbackAds;

    //MAX
    string interId = "9f262b7c77e21242";
    string videoId = "c17fd9a6872df21a";
    string bannerId = "e930a4209cb431e3";
    int retryAttempt;

    int reloadAds;

    public bool HaveNetwork { get => Application.internetReachability != NetworkReachability.NotReachable; }


    //Runtime
    private float timeCooldownShowAds;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        AppsFlyerInit();
        Init();
    }

    private void Update()
    {
        if( timeCooldownShowAds > 0)
        {
            timeCooldownShowAds -= Time.deltaTime;
        }
    }

    private void AppsFlyerInit()
    {
        AppsFlyer.initSDK("G3MBmMRHTuEpXbqyqSWGeK", "");
        AppsFlyer.startSDK();
    }

    public void Init()
    {
        MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) =>
        {
            // AppLovin SDK is initialized, start loading ads


            // Attach callback
            MaxSdkCallbacks.OnInterstitialLoadedEvent += OnInterstitialLoadedEvent;
            MaxSdkCallbacks.OnInterstitialLoadFailedEvent += OnInterstitialFailedEvent;
            MaxSdkCallbacks.OnInterstitialAdFailedToDisplayEvent += InterstitialFailedToDisplayEvent;
            MaxSdkCallbacks.OnInterstitialHiddenEvent += OnInterstitialDismissedEvent;
            MaxSdkCallbacks.OnInterstitialDisplayedEvent += OnInterstitialDisplayedEvent;

            // Load the first interstitial
            LoadInterAd();

            MaxSdkCallbacks.OnRewardedAdLoadedEvent += OnRewardedAdLoadedEvent;
            MaxSdkCallbacks.OnRewardedAdLoadFailedEvent += OnRewardedAdFailedEvent;
            MaxSdkCallbacks.OnRewardedAdFailedToDisplayEvent += OnRewardedAdFailedToDisplayEvent;
            MaxSdkCallbacks.OnRewardedAdDisplayedEvent += OnRewardedAdDisplayedEvent;
            MaxSdkCallbacks.OnRewardedAdClickedEvent += OnRewardedAdClickedEvent;
            MaxSdkCallbacks.OnRewardedAdHiddenEvent += OnRewardedAdDismissedEvent;
            MaxSdkCallbacks.OnRewardedAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;

            //Banner callback
            MaxSdkCallbacks.OnBannerAdLoadedEvent += OnBannerLoadedEvent;
            MaxSdkCallbacks.OnBannerAdLoadFailedEvent += OnBannerLoadFailedEvent;
            MaxSdkCallbacks.OnBannerAdClickedEvent += OnBannerClickedEvent;
            MaxSdkCallbacks.OnBannerAdCollapsedEvent += OnBannerCollapsedEvent;
            MaxSdkCallbacks.OnBannerAdExpandedEvent += OnBannedExpandedEvent;


            // Load the first rewarded ad
            LoadVideoAds();

            InitializeBannerAds();
        };

        MaxSdk.SetSdkKey("ZoNyqu_piUmpl33-qkoIfRp6MTZGW9M5xk1mb1ZIWK6FN9EBu0TXSHeprC3LMPQI7S3kTc1-x7DJGSV8S-gvFJ");
        MaxSdk.SetUserId("USER_ID");
        MaxSdk.InitializeSdk();
    }

    public void InitializeBannerAds()
    {
        // Banners are automatically sized to 320×50 on phones and 728×90 on tablets
        // You may call the utility method MaxSdkUtils.isTablet() to help with view sizing adjustments
        MaxSdk.CreateBanner(bannerId, MaxSdkBase.BannerPosition.BottomCenter);

        // Set background or background color for banners to be fully functional
        MaxSdk.SetBannerBackgroundColor(bannerId, Color.white);

        //Event 
        //FirebaseAnalystic.Instance.ShowAdsSuccess(FirebaseAnalystic.AdsType.Banner);
    }

    public void ShowBanner()
    {
        if (!HaveNetwork)
            return;

#if UNITY_ANDROID || UNITY_IOS
        MaxSdk.ShowBanner(bannerId);
#endif
    }

    public void HideBanner()
    {
        if (!HaveNetwork)
            return;

#if UNITY_ANDROID || UNITY_IOS
        MaxSdk.HideBanner(bannerId);
#endif
    }


    public void LoadInterAd()
    {
#if UNITY_ANDROID || UNITY_IOS
        MaxSdk.LoadInterstitial(interId);
#endif
    }

    public void ShowInterAd(Action callback = null)
    {
        if (!HaveNetwork)
            return;

#if UNITY_ANDROID || UNITY_IOS
        FirebaseAnalystic.Instance.ClickAds();

        if(timeCooldownShowAds <= 0)
        {
            if (MaxSdk.IsInterstitialReady(interId))
            {
                MaxSdk.ShowInterstitial(interId);
            }

            timeCooldownShowAds = 30f;
        }

#endif

        if (callback != null) callback();
    }

    public void LoadVideoAds()
    {
#if UNITY_ANDROID || UNITY_IOS
        MaxSdk.LoadRewardedAd(videoId);
#endif
    }

    public void ShowVideoAds(FirebaseAnalystic.RewardPos pos, Action callback = null)
    {
        if (!HaveNetwork)
            return;

        if (callback != null)
            callbackAds = callback;

#if UNITY_ANDROID || UNITY_IOS
        callbackAds += () => FirebaseAnalystic.Instance.ShowRewardAt(pos);

        if (MaxSdk.IsRewardedAdReady(videoId))
        {
            MaxSdk.ShowRewardedAd(videoId);
        }

        
#endif
    }

    #region Max callback

    private void OnInterstitialLoadedEvent(string adUnitId)
    {
        // Interstitial ad is ready for you to show. MaxSdk.IsInterstitialReady(adUnitId) now returns 'true'

        // Reset retry attempt
        retryAttempt = 0;
        reloadAds = 0;
    }

    private void OnInterstitialFailedEvent(string adUnitId, int errorCode)
    {
        // Interstitial ad failed to load 
        // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds)

        retryAttempt++;
        double retryDelay = Math.Pow(2, Math.Min(6, retryAttempt));
        Invoke("LoadInterAd", (float)retryDelay);

        //if (reloadAds++ > 3)
        //    return;

        //CoroutineUtils.PlayCoroutine(() => LoadInterAd(), 5f);
        FirebaseAnalystic.Instance.LoadAdsFail(FirebaseAnalystic.AdsType.Inter);
    }

    private void InterstitialFailedToDisplayEvent(string adUnitId, int errorCode)
    {
        // Interstitial ad failed to display. AppLovin recommends that you load the next ad.
        LoadInterAd();

    }

    private void OnInterstitialDismissedEvent(string adUnitId)
    {
        // Interstitial ad is hidden. Pre-load the next ad.
        Time.timeScale = 1;
        LoadInterAd();
    }

    private void OnInterstitialDisplayedEvent(string adUnitId)
    {
        // Interstitial ad show
        Time.timeScale = 0;

        FirebaseAnalystic.Instance.ShowAdsSuccess(FirebaseAnalystic.AdsType.Inter);
    }

    private void OnRewardedAdLoadedEvent(string adUnitId)
    {
        // Rewarded ad is ready for you to show. MaxSdk.IsRewardedAdReady(adUnitId) now returns 'true'.

        // Reset retry attempt
        retryAttempt = 0;
    }

    private void OnRewardedAdFailedEvent(string adUnitId, int errorCode)
    {
        // Rewarded ad failed to load 
        // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds).

        retryAttempt++;
        double retryDelay = Math.Pow(2, Math.Min(6, retryAttempt));

        //Invoke("LoadVideoAds", (float)retryDelay);

        FirebaseAnalystic.Instance.LoadAdsFail(FirebaseAnalystic.AdsType.Video);
    }

    private void OnRewardedAdFailedToDisplayEvent(string adUnitId, int errorCode)
    {
        // Rewarded ad failed to display. AppLovin recommends that you load the next ad.
        Time.timeScale = 1;
        LoadVideoAds();
    }

    private void OnRewardedAdDisplayedEvent(string adUnitId)
    {
        Time.timeScale = 0;
        Debug.Log("ADS:RewardedVideoAdOpenedEvent");
    }

    private void OnRewardedAdClickedEvent(string adUnitId) { }

    private void OnRewardedAdDismissedEvent(string adUnitId)
    {
        // Rewarded ad is hidden. Pre-load the next ad
        Time.timeScale = 1;
        LoadVideoAds();
    }

    private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward)
    {
        // The rewarded ad displayed and the user should receive the reward.
        Time.timeScale = 1;
        AudioListener.pause = false;
        LoadVideoAds();

        if (callbackAds != null)
        {
            callbackAds();
        }
        Debug.Log("ADS:OnRewardedAdReceivedRewardEvent");
        if (MyScene.Instance.lastRewardAdMode == MyScene.RewardAdMode.PlusCoin)
        {
            GameDataManager.Instance.SetCoin(500);
            FindObjectOfType<CoinTxt>().SetText(GameDataManager.Instance.gameDataScrObj.totalCoin);
        }
        else if (MyScene.Instance.lastRewardAdMode == MyScene.RewardAdMode.X5)
        {
            GameDataManager.Instance.SetCoin(PlayerData.Instance.coinEarnThisRun * 5);
        }
        else if (MyScene.Instance.lastRewardAdMode == MyScene.RewardAdMode.Key)
        {
            GameDataManager.Instance.SetKey(3);
        }
        else if (MyScene.Instance.lastRewardAdMode == MyScene.RewardAdMode.BuySkin)
        {
            SkinBtnClick[] a = FindObjectsOfType<SkinBtnClick>();
            a[FindObjectsOfType<SkinBtnClick>().Length - MyScene.Instance.lastSkin1BuyByVideoClicked-1].SetAfterSawRewardAd();
        }
        FirebaseAnalystic.Instance.ShowAdsSuccess(FirebaseAnalystic.AdsType.Video);
    }
    private void OnBannedExpandedEvent(string obj)
    {
        throw new NotImplementedException();
    }

    private void OnBannerCollapsedEvent(string obj)
    {
        throw new NotImplementedException();
    }

    private void OnBannerClickedEvent(string obj)
    {
        throw new NotImplementedException();
    }

    private void OnBannerLoadFailedEvent(string arg1, int arg2)
    {
        FirebaseAnalystic.Instance.LoadAdsFail(FirebaseAnalystic.AdsType.Banner);
    }

    private void OnBannerLoadedEvent(string obj)
    {
        //FirebaseAnalystic.Instance.ShowAdsSuccess(FirebaseAnalystic.AdsType.Banner);
    }
    #endregion
}
