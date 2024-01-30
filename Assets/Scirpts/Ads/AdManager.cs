using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    //ID=ca-app-pub-8593479994735877~1024172592
       //ca-app-pub-8593479994735877/4625378397

    [SerializeField] private string adUnityId = "";
    private InterstitialAd interstitialAd;
    void Start()
    {
        MobileAds.Initialize(initStatus => { });
        LoadInterstitalAd();
    }
    public void Ads()
    {
        //this.interstitialAd.Show();
        ShowInterstitialAd();
    }
    // reklamý yükle
    private void LoadInterstitalAd()
    {
        if (interstitialAd != null)
        {
            interstitialAd.Destroy(); // bir butona aktarýlmadýðý takdirde yalnýzca bir kere çalýþýr 
            interstitialAd = null;
        }
        Debug.Log("Loading th interstitial ad.");
        var adRequest = new AdRequest();

        InterstitialAd.Load(adUnityId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad" + "with error:" + error);
                    return;
                }
                Debug.Log("Interstitial ad loaded with response:" + ad.GetResponseInfo());
                interstitialAd = ad;
            }
            );
    }
    private void ShowInterstitialAd()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            interstitialAd.Show();
        }
        else
        {
            Debug.Log("Interstitial ad is not ready yet");
        }
    }
}
