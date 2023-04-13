using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* 追加 */
using GoogleMobileAds.Api;

public class GoogleAdMobBanner : MonoBehaviour
{
//     private BannerView bannerView;

// #if UNITY_ANDROID
    
//     public string adUnitId = "ca-app-pub-8148356110096114/4508284743";
// #elif UNITY_IPHONE
//         public string adUnitId = "ca-app-pub-8148356110096114/9927259585";
// #else
//         public string adUnitId = "unexpected_platform";
// #endif

    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
         //FirstSetting();
    }
/*
    private void RequestBanner()
    {
        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);

    }*/


    private BannerView _bannerView;
	private InterstitialAd _interstitial;
	private RewardedAd _rewardedAd;

	/// <summary>
	/// 初回に呼ぶ
	/// </summary>
	public void FirstSetting()
	{
		MobileAds.Initialize(initStatus => { });
	}

	/// <summary>
	/// バナー広告を生成
	/// </summary>
	/// <param name="size"></param>
	/// <param name="position"></param>
	public void RequestBanner(AdSize size, AdPosition position)
	{
#if UNITY_ANDROID
		string adUnitId = "ca-app-pub-8148356110096114/9927259585";
#elif UNITY_IOS
		string adUnitId = "ca-app-pub-8148356110096114/9927259585";
#else
		string adUnitId = "unexpected_platform";
#endif
		// Create a 320x50 banner at the top of the screen.
		this._bannerView = new BannerView(adUnitId, size, position);

		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();

		// Load the banner with the request.
		this._bannerView.LoadAd(request);

	}

	/// <summary>
	/// バナー広告削除
	/// </summary>
	public void DestroyBanner()
	{
		if (this._bannerView != null)
		{
			_bannerView.Destroy();
		}
	}

}