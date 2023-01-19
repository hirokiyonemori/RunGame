using UnityEngine;
using GoogleMobileAds.Api;
using System;
public class GoogleAds : MonoBehaviour
{
    public string adUnitId;
    private InterstitialAd interstitialAd;
    // Use this for initialization
    void Start()
    {
        //アプリ起動時に一度必ず実行（他のスクリプトで実行していたら不要）
        MobileAds.Initialize(initStatus => { });
        //広告を表示
        RequestInterstitial();
    }
    //広告を表示するメソッド
    public void RequestInterstitial()
    {
#if UNITY_ANDROID
        //adUnitId = "広告ユニットIDをコピペ（Android）";  //本番
        //adUnitId = "ca-app-pub-3940256099942544/1033173712";  //テスト
#elif UNITY_IOS
        //adUnitId = "広告ユニットIDをコピペ（iOS）";  //本番
        //adUnitId = "ca-app-pub-3940256099942544/4411468910";  //テスト
#endif
        this.interstitialAd = new InterstitialAd(adUnitId);
        AdRequest request = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(request);
        if (this.interstitialAd.IsLoaded())
        {
            this.interstitialAd.Show();
        }
    }
}