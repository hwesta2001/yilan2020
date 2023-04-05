using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideSome : MonoBehaviour
{
    public static CollideSome instance;
    public int puan;
    [SerializeField]
    private Animator anim;
    public bool deathNow;
    private GameObject parentEffect;
    [SerializeField]
    private GameObject aracPatlamaPrefab;
    public List<ParticleSystem> efektPool = new List<ParticleSystem>();

    private void Awake()
    {
        instance = this;
        parentEffect = new GameObject("Effects");
        for (int i=0; i < efektPool.Count; i++)
        {

            GameObject go = Instantiate(aracPatlamaPrefab, transform.position, Quaternion.Euler(90,0,0),parentEffect.transform);
            efektPool[i] = go.GetComponent<ParticleSystem>();
            efektPool[i].gameObject.SetActive(false);
            efektPool[i].transform.localPosition = Vector3.zero;

        }

    }

    private void Start()
    {
        deathNow = false;
    }

    void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("BodyPart") && Hareket.hareket.canMove)
        {
            //öldür burda.
            StartCoroutine("DeathAnimation");

        }
        if (other.CompareTag("inwissWall") && Hareket.hareket.canMove)
        {
            //öldür burda.
            StartCoroutine("DeathAnimation");

        }

        if (other.CompareTag("Building"))
        {

            if (!other.GetComponent<BinaYikilma>().yaniyor)
            {
                //öldür burda.
                StartCoroutine("DeathAnimation");
            }
            else
            {
                other.GetComponent<BinaYikilma>().SonPatlama();
                other.GetComponent<BinaYikilma>().KupCik(other.transform.position + Vector3.up * transform.localScale.y);
            }
        }



        if (other.CompareTag("prop"))
        {
            Main.main.ChangePropCount(1);
            Destroy(other.gameObject);
            Main.main.ChangeScore(puan -5);
            EatSome();
            EfektOynat(other.transform,0.5f);

        }

        if (other.CompareTag("Arac"))
        {
            Main.main.ChangeScore(puan);
            GameObject go = other.gameObject;
            go.SetActive(false);
            Main.main.AddToPool(go);
            StartCoroutine("SObj");
            Hareket.hareket.AddBodyPart();
            EatSome();
            EfektOynat(other.transform,0.8f);
        }

        if (other.CompareTag("kup"))
        {
            if (other.GetComponent<KupAnim>().eatable == true)
            {
                Main.main.ChangeScore(puan + 5);


                other.gameObject.SetActive(false);
                other.GetComponent<Rigidbody>().isKinematic = true;
                Hareket.hareket.AddBodyPart();
                EatSome();
                EfektOynat(other.transform,.7f);
            }
        }



    }
    IEnumerator SObj()
    {
        float i = Random.Range(0.5f, 3f);
        yield return new WaitForSeconds(i);
        Main.main.FromPoolSpwan();

    }

    private void EatSome()
    {
        anim.SetBool("eating", true);
    }

    public void EatingEnds()
    {
        anim.SetBool("eating", false);
    }


    public void EfektOynat(Transform tr, float size)
    {
        if (efektPool.Count > 0)
        {
            for(int i=0; i < efektPool.Count; i++)
            {

                if (!efektPool[i].gameObject.activeInHierarchy)
                {
                    efektPool[i].transform.position = tr.position + Vector3.up * 3;
                    efektPool[i].gameObject.SetActive(true);
                    efektPool[i].transform.localScale *= size;
                    return;
                }

            }
        }
        if (allDeactive())
        {
            GameObject go = Instantiate(aracPatlamaPrefab, transform.position, Quaternion.Euler(90, 0, 0), parentEffect.transform);
            go.gameObject.SetActive(false);
            go.transform.localPosition = Vector3.zero;
            efektPool.Add(go.GetComponent<ParticleSystem>());
        }
        bool allDeactive()
        {

            bool response = true;

            for (int i = 0; i < efektPool.Count; i++)
            {
                if (!efektPool[i].gameObject.activeInHierarchy)
                {
                    response = false;
                    break;
                }
            }

            return response;
        }

    }

    IEnumerator DeathAnimation()
    {
        if (!deathNow)
        {
            deathNow = true;
            Main.main.EndScore();
            GASend.instance.FailSender();
            FaceBookAn.instance.FailSender();
            Hareket.hareket.movementSpeed = 0;
            Hareket.hareket.AllPartDeathEffect();
            yield return new WaitForSeconds(2f);
            GameControl.gameControl.PauseGame();
            yield return null;

        }
    }

}