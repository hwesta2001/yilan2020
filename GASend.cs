using System.Collections;
using GameAnalyticsSDK;
using UnityEngine;

public class GASend : MonoBehaviour
{
    private static bool created = false;
    public static GASend instance;
    private static int failAmount;
    private void Awake()
    {
        instance = this;
        if (!created)
        {
            failAmount = 0;
            DontDestroyOnLoad(gameObject);
            created = true;
            Debug.Log("Awake: " + gameObject);
        }
    }
    void Start()
    {
        GameAnalytics.Initialize();
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Level");
        // InvokeRepeating("PeriodicSend", 30f, 30f);
        StartCoroutine("PeriodicSend");
    }




    IEnumerator PeriodicSend()
    {

        yield return new WaitForSeconds(30);

        while(true)
        {
            // GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Level", "Snake Size", Main.main.sizeCount);
            GameAnalytics.NewDesignEvent("Snake Size", Main.main.sizeCount);
            yield return new WaitForSeconds(30);
        }
    }

    public void FailSender() 
    {
        failAmount++;
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "Level", "Fail Amount", failAmount);
        GameAnalytics.NewDesignEvent("Snake Size at Fail", Main.main.sizeCount);
       // GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "Level", "Snake Size at Fail", Main.main.sizeCount);
    }
}
