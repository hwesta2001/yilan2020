using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using TMPro;

public class GameControl : MonoBehaviour
{
    [SerializeField] GameObject controlCanvas, menuCanvas;
    [SerializeField] GameObject reloadButton;
    [SerializeField] TextMeshProUGUI timerText,endTimerText;

    [HideInInspector]
    public bool paused=true;
    public static GameControl gameControl;
    int timer;
    private void Awake()
    {
        gameControl = this;
    }

    private void Start()
    {
        
        timer = 150;
        timerText.text = "TIME : " + timer.ToString();
        Invoke("InitTimer", 2f);
        ResumeGame();
    }

    public void PauseGame()
    {
        StopCoroutine("Timer");
        controlCanvas.SetActive(false);
        menuCanvas.SetActive(true);
        paused = true;
        Time.timeScale = 0;
    }


    public void ResumeGame()
    {
        Time.timeScale = 1;
        menuCanvas.SetActive(false);
        controlCanvas.SetActive(true);
        paused = false;
    }

    public void ReloadGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    void InitTimer()
    {
        StartCoroutine("Timer");
    }

    IEnumerator Timer()
    {
        while (true)
        {
            TimeCount();
            yield return new WaitForSeconds(1);
        }
    }



    void TimeCount()
    {
        timer -= 1;
        timerText.text = "TIME : " + timer.ToString();

        if (timer <= 0)
        {
            PauseGame();
            Main.main.win = true;
            Main.main.EndScore();
        }

        if (timer == 75)
        {
            timerText.color = Color.white;
        }
        if (timer == 25)
        {
            timerText.color = Color.red;
        }
    }


    public void EndTimer()
    {
        StopCoroutine("Timer");
        int endTime = timer - 150;
        endTimerText.text = endTime.ToString();
    }

}
