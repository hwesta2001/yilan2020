using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeadCanvas : MonoBehaviour
{
    public static HeadCanvas headCanvas;

    private new Transform camera;
    public List<TextMeshProUGUI> puanList = new List<TextMeshProUGUI>();
    public GameObject puanText;
    [SerializeField] Transform target;
    void Awake()
    {
        headCanvas = this;

        camera = Camera.main.transform;

    }

    // Update is called once per frame
    void Update()
    {

        //  transform.rotation = Quaternion.LookRotation(transform.position - camera.position);

        //Vector3 v = camera.position - transform.position;
        //v.z = 0.0f;
        //transform.LookAt(camera.position - v);

        transform.LookAt(camera);
        transform.Rotate(0, 180, 0);

        Vector3 pos = target.position + Vector3.up * target.localScale.y * 1.5f;
        transform.position = pos;
    }



    public void PuanYaz(int puan)
    {
        if (puanList.Count > 0)
        {
            for (int i = 0; i < puanList.Count; i++)
            {

                if (!puanList[i].gameObject.activeInHierarchy)
                {
                    
                    
                    puanList[i].text = puan.ToString();
                    puanList[i].gameObject.SetActive(true);

                    Vector3 moveP = new Vector3(0, 3, 0);

                    puanList[i].transform.DOLocalMove(moveP, 1)
                                         .SetEase(Ease.OutExpo)
                                         .OnComplete(() => ResetPos(puanList[i].gameObject));
                    break;
                }
            }

        }

        if (allDeactive())
        {
            GameObject go = Instantiate(puanText, transform);
            go.SetActive(false);
            puanList.Add(go.GetComponent<TextMeshProUGUI>());
        }

        bool allDeactive()
        {

            bool response = true;

            for (int x = 0; x < puanList.Count; x++)
            {
                if (!puanList[x].gameObject.activeInHierarchy)
                {
                    response = false;
                    break;
                }
            }

            return response;
        }

    }

    void ResetPos(GameObject gom)
    {
        gom.SetActive(false);
        gom.transform.localPosition = Vector3.zero;
    }
}
