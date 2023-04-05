using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using System.Collections;

public class FaceBookAn : MonoBehaviour
{
    public static FaceBookAn instance;
    private static int failAmount;
    private void Awake()
    {
        instance = this;

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
        FB.LogAppEvent("Start Level", 1);
        StartCoroutine("PeriodicSend");
    }
    IEnumerator PeriodicSend()
    {

        yield return new WaitForSeconds(30);

        while (true)
        {
            FB.LogAppEvent("Snake Size", Main.main.sizeCount);
            yield return new WaitForSeconds(30);
        }
    }

    public void FailSender()
    {
        failAmount++;
        FB.LogAppEvent("Fail Amount", failAmount);
        FB.LogAppEvent("Snake Size at Fail", Main.main.sizeCount);
    }


}
