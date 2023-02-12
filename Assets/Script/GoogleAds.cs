using UnityEngine;
using GoogleMobileAds.Api;
using System;
public class GoogleAds : MonoBehaviour
{
    private string adUnitId;
    private InterstitialAd interstitialAd;

    private int reShowCount = 0;
    // Use this for initialization
    void Start()
    {
        //�A�v���N�����Ɉ�x�K�����s�i���̃X�N���v�g�Ŏ��s���Ă�����s�v�j
        MobileAds.Initialize(initStatus => { });
        //�L����\��
        RequestInterstitial();
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
#endif
        this.interstitialAd = new InterstitialAd(adUnitId);
        AdRequest request = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(request);
    }
    
    
    //インタースティシャル広告を表示したいときに呼び出す
    public void InterstitialShow()
    {
        //Debug.Log(" this.interstitialAd.IsLoaded() " + this.interstitialAd.IsLoaded());
        if (this.interstitialAd.IsLoaded())
        {
            this.interstitialAd.Show();
            reShowCount = 0;
        }
        else {
            //準備できてなかったら0.1秒ごとに準備できてるか確認
            if (reShowCount < 10) {
                Invoke ("InterstitialShow", 0.1f);
                reShowCount++;
            } else {
                //1秒たっても準備できなかったとき
                reShowCount = 0;
            }
        }   
    }
    
    
}