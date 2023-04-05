using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.AI;
using DG.Tweening;

public class Main : MonoBehaviour
{
    public static Main main;
    public float cubeControlOrani;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI winScoreText,loseScoreText,winH,loseH;
    [SerializeField] GameObject winPanel, losePanel;


    [HideInInspector]
    public int Score;
    [HideInInspector]
    public int propCount;
    [HideInInspector]
    public List<GameObject> pool = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> spawners = new List<GameObject>();
    [HideInInspector]
    public List<Transform> AracDest = new List<Transform>();
    [HideInInspector]
    public int sizeCount;
    private int junkCount = 0;

    public bool win;


    public Material[] materials;


    private void Awake()
    {
        main = this;
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("spawner"))
        {
            spawners.Add(obj);
            
        }

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("AracDestination"))
        {

            AracDest.Add(obj.transform);
            obj.GetComponent<MeshRenderer>().enabled = false;
        }


    }

    private void Start()
    {
        win = false;
        ScoreText.text = "Score : " + Score.ToString();
        sizeCount = 1;

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Arac"))
        {

            ReLocate(obj);
        }



    }


    public void ChangeScore(int puan)
    {
        Score += puan;
        ScoreText.text = "Score : " + Score.ToString();
        HeadCanvas.headCanvas.PuanYaz(puan); // puan kafada cıkacak görseli


    }

    public void ChangePropCount(int an)
    {
        propCount += an;
        junkCount += 1;
        if (junkCount == 30)
        {
            Hareket.hareket.AddBodyPart();
            junkCount = 0;
        }
    }

    public void AddToPool (GameObject go)
    {
        if (!pool.Contains(go))
        {
            pool.Add(go);
        }
    }

    public void FromPoolSpwan()
    {
        if (pool.Count > 0 )
        {
            int i = Random.Range(0, pool.Count);
            GameObject go = pool[i];
            if (!go.activeInHierarchy)
            {
                go.SetActive(true);
                ReLocate(go);
                pool.Remove(go);

            }

        }
    }

    public void ReLocate(GameObject go)
    {
        int i = Random.Range(0, spawners.Count);
        Vector3 spawnPoint = spawners[i].GetComponent<Renderer>().bounds.center;
        go.GetComponent<NavMeshAgent>().Warp(spawnPoint);
        ReRenkArac(go);
    }



    void ReRenkArac(GameObject arac)
    {


        Renderer rend = arac.transform.Find("Govde").GetComponent<Renderer>();
        rend.material = materials[Random.Range(0, materials.Length)];
    }


    public void EndScore()
    {
        winScoreText.text = $"{Score}";
        loseScoreText.text = $"{Score}";
        int hs = Random.Range(Score, Score * 2);
        winH.text = $"{hs}";
        loseH.text = $"{hs}";
        if (win)
        {
            winPanel.SetActive(true);
            losePanel.SetActive(false);
        }
        else
        {

            winPanel.SetActive(false);
            losePanel.SetActive(true);
            GameControl.gameControl.EndTimer();
        }



    }


}
