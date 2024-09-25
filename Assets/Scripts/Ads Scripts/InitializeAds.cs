using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class InitializeAds : MonoBehaviour, IUnityAdsInitializationListener
{
    public string androidGameID;
    public string iOSGameID;
    string gameID;

    public bool isTesting = true; // shows unity showcase ads

    void Awake()
    {
        InitAds();
    }

    public void InitAds()
    {
        // Using preprocessor directives to only load ads specific to the platform and block the rest before compile time
        #if UNITY_IOS
            gameID = iOSGameID;
        #elif UNITY_ANDROID
            gameID = androidGameID;
        #elif UNITY_EDITOR
            gameID = androidGameID; // test
        #endif

        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(gameID, isTesting, this);
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Ad Initialization Complete");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log("Ad Initialization Failed");
    }
}
