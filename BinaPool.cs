using System.Collections.Generic;
using UnityEngine;

public class BinaPool : MonoBehaviour
{
    public GameObject PatlamaEfect, CatlamaEfect, DumanEffect, TallEffect;

    public static BinaPool instance;
    [SerializeField] private GameObject kupPrefab;
    [SerializeField] int kupPoolSize=100;

    [Range(0f, 15f)]
    public float yesilRenkHizi;
    public List<GameObject> kupler = new List<GameObject>();

    public Color32[] colors;

    private void Awake()
    {
        instance = this;

        for (int i = 0; i < kupPoolSize; i++)
        {

            NewKupAdd();


        }
    }

    public void NewKupAdd()
    {

        GameObject go = Instantiate(kupPrefab, transform.position, Quaternion.identity, transform);
        go.SetActive(false);
        go.GetComponent<Rigidbody>().isKinematic = true;
        kupler.Add(go);

    }

    public void KupCikar(Vector3 point, int cubeAdet, int colorCode)
    {
        for (int a = 0; a < cubeAdet; a++)
        {
            for (int i = 0; i < kupler.Count; i++)
            {

                if (BinaPool.instance.kupler.Count > 0)
                {
                    

                    if (!kupler[i].activeInHierarchy)
                    {
                        GameObject go = kupler[i];
                        go.transform.position = point + Random.insideUnitSphere * 2;
                        go.transform.GetComponent<Renderer>().material.color = colors[colorCode];
                        go.SetActive(true);
                        go.transform.localScale = Vector3.one * ((float)cubeAdet /(float)(cubeAdet-1));
                        go.GetComponent<Rigidbody>().isKinematic = false;
                        go.GetComponent<Rigidbody>().AddForce(Random.onUnitSphere * 70f);
                        break;
                    }



                }
            }

            if (allDeactive())
            {
                NewKupAdd();
            }

            bool allDeactive()
            {

                bool response = true;

                for (int x = 0; x < kupler.Count; x++)
                {
                    if (!kupler[x].gameObject.activeInHierarchy)
                    {
                        response = false;
                        break;
                    }
                }

                return response;
            }
        }
    }


}