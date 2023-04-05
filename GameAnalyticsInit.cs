using Facebook.Unity;
using GameAnalyticsSDK;
using UnityEngine;

public class GameAnalyticsInit : MonoBehaviour
{


    private void Awake()
    {
        

        if (!FB.IsInitialized)
        {
            FB.Init(() =>
            {
                if (FB.IsInitialized)
                    FB.ActivateApp();
                else
                    Debug.LogError("Couldn't initialize");
            },
            isGameShown =>
            {
                if (!isGameShown)
                    Time.timeScale = 0;
                else
                    Time.timeScale = 1;
            });
        }
        else
            FB.ActivateApp();
    }
    void Start()
    {
        GameAnalytics.Initialize();
    }

}
