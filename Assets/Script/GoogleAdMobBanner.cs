using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* ’Ç‰Á */
using GoogleMobileAds.Api;

public class GoogleAdMobBanner : MonoBehaviour
{
    private BannerView bannerView;

#if UNITY_ANDROID
    
    public string adUnitId = "ca-app-pub-8148356110096114/4508284743";
#elif UNITY_IPHONE
        public string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
            public string adUnitId = "unexpected_platform";
#endif

    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });

        this.RequestBanner();
    }

    private void RequestBanner()
    {
        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);

    }
}