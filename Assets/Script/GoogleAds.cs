using UnityEngine;
using GoogleMobileAds.Api;
using System;
using System.Globalization;

public class GoogleAds : MonoBehaviour
{
    //private string adUnitId;
    private InterstitialAd interstitialAd;
    private InterstitialAd _interstitial;
    private int reShowCount = 0;
    // Use this for initialization

    public Action<double> OnReward;
    
    void Start()
    {

        MobileAds.Initialize(initStatus => { });

    }
    //�L����\�����郁�\�b�h
    public void RequestInterstitial()
    {
#if UNITY_ANDROID
        // privateでやらないと表示はされない
        adUnitId = "ca-app-pub-8148356110096114/4508284743";  //�e�X�g
#elif UNITY_IOS
        //adUnitId = "�L�����j�b�gID���R�s�y�iiOS�j";  //�{��
        
        //adUnitId = "ca-app-pub-3940256099942544/4411468910";  //�e�X�g

        //interstitialAd = new InterstitialAd(adUnitId);
        //AdRequest request = new AdRequest.Builder().Build();

#endif
        //this.interstitialAd = new InterstitialAd(adUnitId);
        //AdRequest request = new AdRequest.Builder().Build();
        //interstitialAd.LoadAd(request);
    }
    
    
    //インタースティシャル広告を表示したいときに呼び出す
    public void InterstitialShow()
    {

#if UNITY_ANDROID
		string adUnitId = "ca-app-pub-3940256099942544/8691691433";
#elif UNITY_IOS
        string adUnitId = "ca-app-pub-8148356110096114/8676079053";
#else
		string adUnitId = "unexpected_platform";
#endif
        // Initialize an InterstitialAd.
        this._interstitial = new InterstitialAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this._interstitial.OnAdLoaded += HandleOnInterstitialLoaded;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the interstitial with the request.
        this._interstitial.LoadAd(request);
    }

    /// <summary>
    /// インタースティシャルを出す
    /// </summary>
    public void PlayInterstitial()
    {
        InitInterstitial();
    }

    /// <summary>
    /// インタースティシャル削除
    /// </summary>
    public void DestroyInterstitial()
    {
        if (this._interstitial != null)
        {
            this._interstitial.Destroy();
        }
    }

    private void HandleOnInterstitialLoaded(object sender, EventArgs args)
    {
        Debug.Log("HandleAdLoaded event received");
        this._interstitial.Show();
    }

    /// <summary>
    /// インタースティシャル読み込み
    /// </summary>
    private void InitInterstitial()
    {
#if UNITY_ANDROID
		string adUnitId = "ca-app-pub-3940256099942544/8691691433";
#elif UNITY_IOS
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
		string adUnitId = "unexpected_platform";
#endif
        // Initialize an InterstitialAd.
        this._interstitial = new InterstitialAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this._interstitial.OnAdLoaded += HandleOnInterstitialLoaded;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the interstitial with the request.
        this._interstitial.LoadAd(request);
    }




}