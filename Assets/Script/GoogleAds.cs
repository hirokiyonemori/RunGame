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
        //�A�v���N�����Ɉ�x�K�����s�i���̃X�N���v�g�Ŏ��s���Ă�����s�v�j
        MobileAds.Initialize(initStatus => { });
        //�L����\��
        RequestInterstitial();
    }
    //�L����\�����郁�\�b�h
    public void RequestInterstitial()
    {
#if UNITY_ANDROID
        //adUnitId = "�L�����j�b�gID���R�s�y�iAndroid�j";  //�{��
        //adUnitId = "ca-app-pub-3940256099942544/1033173712";  //�e�X�g
#elif UNITY_IOS
        //adUnitId = "�L�����j�b�gID���R�s�y�iiOS�j";  //�{��
        //adUnitId = "ca-app-pub-3940256099942544/4411468910";  //�e�X�g
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