using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{

    public GoogleAds googleAds = null;

    void Start()
    {
       // googleAds.ShowInterstitialAd();
       FadeManager.GetInstance().StartFadeIn();
    }

    void Update()
    {
        
    }

    // 再スタートする
    public void onRestertButton()
    {
        //SceneManager.LoadScene("Main");
        AudioManager.instance.loop(true);
        FadeManager.GetInstance().StartFadeOut("Main");
    }

    // 
    public void onBackTitleButton()
    {
        //SceneManager.LoadScene("Title");
        AudioManager.instance.loop(true);
        FadeManager.GetInstance().StartFadeOut("Title");
    }

}
