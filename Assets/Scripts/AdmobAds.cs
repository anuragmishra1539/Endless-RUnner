using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using admob;
public class AdmobAds : MonoBehaviour
{
    private static AdmobAds _instance;

    public static AdmobAds Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<AdmobAds>();
            }

            return _instance;
        }
    }
    Admob ad;
    string appID = "";
    string bannerID = "";
    string interstitialID = "";
    string videoID = "";
    string nativeBannerID = "";

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
#if UNITY_IOS
        		 appID="ca-app-pub-3940256099942544~1458002511";
				 bannerID="ca-app-pub-3940256099942544/2934735716";
				 interstitialID="ca-app-pub-3940256099942544/4411468910";
				 videoID="ca-app-pub-3940256099942544/1712485313";
				 nativeBannerID = "ca-app-pub-3940256099942544/3986624511";
#elif UNITY_ANDROID
        appID = "ca-app-pub-8896729561451658~9597078480";
        bannerID = "ca-app-pub-8896729561451658/3584713213";
        interstitialID = "ca-app-pub-8896729561451658/6757671463";
        videoID = "ca-app-pub-7876837603494376/2218821968";
        nativeBannerID = "ca-app-pub-3940256099942544/2247696110";
#endif
        AdProperties adProperties = new AdProperties();


        ad = Admob.Instance();
        ad.bannerEventHandler += onBannerEvent;
        ad.interstitialEventHandler += onInterstitialEvent;
        ad.rewardedVideoEventHandler += onRewardedVideoEvent;


    }


    public void ShowBanner()
    {
        Admob.Instance().showBannerRelative(bannerID, AdSize.SMART_BANNER, AdPosition.BOTTOM_CENTER);
    }
    public void showbannertype2()
    {
        Admob.Instance().showBannerRelative(bannerID, AdSize.SMART_BANNER, AdPosition.TOP_CENTER);
    }

    public void ShowInterstitial()
    {

        if (ad.isInterstitialReady())
        {
            ad.showInterstitial();
        }
        else
        {
            ad.loadInterstitial(interstitialID);
        }
    }



    public void ShowRewardedVideo()
    {

        if (ad.isRewardedVideoReady())
        {
            ad.showRewardedVideo();
        }
        else
        {
            ad.loadRewardedVideo(videoID);
        }
    }

    void onInterstitialEvent(string eventName, string msg)
    {
        Debug.Log("handler onAdmobEvent---" + eventName + "   " + msg);
        if (eventName == AdmobEvent.onAdLoaded)
        {
            Admob.Instance().showInterstitial();
        }
    }
    void onBannerEvent(string eventName, string msg)
    {
        Debug.Log("handler onAdmobBannerEvent---" + eventName + "   " + msg);
    }
    void onRewardedVideoEvent(string eventName, string msg)
    {

        //Give Reward Here
        PlayerPrefs.SetInt("amount", 11);


    }
    // Start is called before the first frame update

}
