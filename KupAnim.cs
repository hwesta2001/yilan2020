using DG.Tweening;
using System.Collections;
using UnityEngine;

public class KupAnim : MonoBehaviour
{
    Rigidbody rb;
    bool once = false;
    public bool eatable= false;
    Vector3 pos;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        
        if (gameObject.activeInHierarchy)
        {

            StartCoroutine("OtoDeaktiv", 8);
        }
    }
    // Update is called once per frame
    void Update()
    {
        float y = transform.position.y;
        if (y < 0.99 * transform.localScale.x)
        {
           
            if (!once)
            {
                RotKup();
            }
        }


        if(once)
        {
            Vector3 newE = Vector3.zero;
            newE += Vector3.up * Time.time * 150;
            transform.eulerAngles = newE;
        }
    }


    void RotKup()
    {
        once = true;
        eatable = true;
        pos = transform.position;
        rb.isKinematic = true;
        transform.position = pos;
        transform.DOMoveY(2.3f, 1).SetEase(Ease.InSine).SetLoops(-1, LoopType.Yoyo).SetId("kupM");

        StartCoroutine("OtoDeaktiv", Random.Range(5, 7));

    }



    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag("Arac"))
        {


            rb.isKinematic = false;
            Vector3 pat = Random.onUnitSphere * 10;
            rb.AddForce(pat,ForceMode.Impulse);

            once = false;



        }

        if (collision.transform.CompareTag("plane"))
        {

            if (!once)
            {
                RotKup();
                
            }


        }




        }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("kup"))
        {
            if (collision.gameObject != gameObject)
            {
                once = false;
                rb.isKinematic = false;
                Vector3 pat = Random.onUnitSphere * 5;
                rb.AddForce(pat, ForceMode.Impulse);


            }
        }

        if (collision.transform.CompareTag("plane"))
        {

            if (!once)
            {
                RotKup();

            }


        }



    }

    IEnumerator SetScale(float sec)
    {
        Vector3 setScale = transform.localScale;
        yield return new WaitForSeconds(sec);
        transform.DOScale(Vector3.zero,1).SetEase(Ease.InBounce).OnComplete(() => transform.localScale=setScale);
        yield break;
    }


    IEnumerator OtoDeaktiv(float sec)
    {
        StartCoroutine("SetScale", sec - 1 );
        yield return new WaitForSeconds(sec);
        eatable = false;
        DOTween.Kill("kupM");
        gameObject.SetActive(false);
        once = false;
        yield break;
    }

    //IEnumerator StartRot(float sec)
    //{
    //    yield return new WaitForSeconds(sec);
    //    RotKup();


    //}
}
