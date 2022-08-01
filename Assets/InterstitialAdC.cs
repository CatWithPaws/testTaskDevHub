using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class InterstitialAdC : MonoBehaviour
{
    public CubeGenerator cubeGenerator;
    private InterstitialAd interstitial;
    private AdRequest request;

    public void Start()
    {
        cubeGenerator.Every20CubesEvent += DoAd;
        
    }

    private void DoAd()
    {
        RequestInterstitial();
         
        // Load the interstitial with the request.

    }

    private void OpenAd(object sender, EventArgs args)
    {
        interstitial.Show();
        print("asdasdasd");
    }

    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);
        interstitial.OnAdClosed += ResumeGame;
        interstitial.OnAdClosed += DeleteAd;
         interstitial.OnAdOpening += PauseGame;
        interstitial.OnAdLoaded += OpenAd;
        request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
    }


    private void PauseGame(object sender, EventArgs args)
    {
        Time.timeScale = 0;
    }

    private void ResumeGame(object sender, EventArgs args)
    {
        Time.timeScale = 1;
    }
    private void DeleteAd(object sender, EventArgs args)
    {
        interstitial.Destroy();
    }
}
