using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Advertisements;

public class DisplayInterstitial : MonoBehaviour, IUnityAdsShowListener, IUnityAdsLoadListener
{
    public string androidADUnitID;
    public string iOSADUnitID;

    private string adUnitID;

    private bool adsLoaded = false;

    void Awake()
    {
#if UNITY_IOS
        adUnitID = iOSADUnitID;
#elif UNITY_ANDROID
        adUnitID = androidADUnitID;
#endif
        LoadAD();
    }

    public void LoadAD()
    {
        Advertisement.Load(adUnitID, this);
    }

    public void ShowAD()
    {
        if (adsLoaded)
        {
            Debug.Log("show");
            Advertisement.Show(adUnitID, this);
        }
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        //ShowAD(); // Show Ad when loaded
        Debug.Log("Ad Loaded");
        adsLoaded = true;
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log("Failed to load Interstitial Ad");
        Debug.Log(error);
        Debug.Log(message);
    }

    public void OnUnityAdsShowClick(string placementId)
    {
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        LoadAD(); // load another ad as soon as one is shown
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        adsLoaded = false;
    }
}
