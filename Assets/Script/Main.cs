using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{

    public GoogleAds googleAds = null;

    // Start is called before the first frame update
    void Start()
    {
       // googleAds.ShowInterstitialAd();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 再スタートする
    public void onRestertButton()
    {
        SceneManager.LoadScene("Main");
    }
}
