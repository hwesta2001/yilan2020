using System.Collections;
using DG.Tweening;
using UnityEngine;

public class BinaYikilma : MonoBehaviour
{
   
    public int cubeAdet;
    float maxCube;
    [SerializeField] int colorCode;
    Rigidbody rb;
    new MeshRenderer renderer;
    public float meshSize;
    float yx;
    Color startColor;
    Color yesilRenk;
    bool isMoving = false;
    public bool yaniyor = false;
    
    GameObject BinaPatlamaEfect, BinaCatlaEfect, BinaDumanEfect, TallPatlama;

    private void Awake()
    {
        
        rb = GetComponent<Rigidbody>();
        renderer = GetComponent<MeshRenderer>();

        startColor = Color.white;

        gameObject.layer = 0;
    }

    private void Start()
    {
        yesilRenk = BinaPool.instance.colors[0];
        yx = 1;

        BinaPatlamaEfect = BinaPool.instance.PatlamaEfect;
        BinaCatlaEfect = BinaPool.instance.CatlamaEfect;
        BinaDumanEfect = BinaPool.instance.DumanEffect;
        TallPatlama = BinaPool.instance.TallEffect;
        
        cubeAdet = Mathf.CeilToInt(GetComponent<MeshFilter>().mesh.bounds.size.z *.25f);
        meshSize = GetComponent<MeshFilter>().mesh.bounds.size.z;
        cubeAdet = Mathf.Clamp(cubeAdet, 2, 7);

        Vector3 point = transform.position;
        point.z = transform.position.z - meshSize * .4f;

        rb.centerOfMass = point;

        rb.mass = 0.001f;
    }

    private void Update()
    {
        maxCube = Main.main.sizeCount * Main.main.cubeControlOrani;
        if (meshSize <= maxCube)
        {
            yaniyor = true;
            RenkYanSon(yesilRenk, yx);

        }


    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!isMoving)
        {
            if (collision.CompareTag("BodyPart"))
            {

                if (yaniyor)
                {
                    Devril(rb);
                }
                //else
                //{
                //    Vector3 dir = transform.position - collision.transform.position;
                    
                //    collision.transform.DOMove(dir.normalized*2, .01f).SetEase(Ease.InQuart);
                //}

            }else if (collision.CompareTag("Building"))
            {
                if (collision != gameObject)
                {

                    float x = collision.GetComponent<BinaYikilma>().meshSize;
                    if (meshSize <= x)
                    {
                        
                            Devril(rb);
                        
                    }

                }
            }

        }

    }

    void Devril(Rigidbody rbdy)
    {
        rbdy.isKinematic = false;
        rbdy.useGravity = true;
        gameObject.layer = 8;
        yesilRenk = Color.yellow;
        yx = 3f;

        isMoving = true;
        StartCoroutine("Patlama");
    }


    IEnumerator Patlama()
    {

        //catlaklar efecti

        GameObject go1 = Instantiate(BinaCatlaEfect, transform.position + Vector3.up * transform.localScale.y * .5f, Quaternion.identity, transform);
        go1.transform.localScale = Vector3.one * 25;
        go1.GetComponent<ParticleSystem>().Play();

        yield return new WaitForSeconds(1);
        GameObject go3 = Instantiate(BinaDumanEfect, transform.position, Quaternion.identity, transform);
        go3.transform.localScale = Vector3.one * 3;
        go3.GetComponent<ParticleSystem>().Play();

        // duman vs


        yx = 10f;
        yesilRenk = Color.red;

        yield return new WaitForSeconds(3);

        //patlama efecti simdi

        SonPatlama();
        KupCik(renderer.bounds.center);



        yield return null;
    }

    public void KupCik(Vector3 point)
    {

        BinaPool.instance.KupCikar(point, cubeAdet, colorCode);

    }
    void RenkYanSon(Color renk, float x)
    {
        
        float t = Mathf.Sin(Time.realtimeSinceStartup * 6 * x);
        renderer.material.color = Color.Lerp(startColor, renk, t);
    }

    public void SonPatlama()
    {
        GameObject go1 = Instantiate(TallPatlama, transform.position, Quaternion.identity);
        go1.transform.localScale = Vector3.one * 8;
        GameObject go2 = Instantiate(BinaPatlamaEfect, transform.position + Vector3.up * transform.localScale.y * .1f, Quaternion.identity);
        go2.transform.localScale = Vector3.one * 8;
        go2.GetComponent<ParticleSystem>().Play();
        Destroy(go1, 1.2f);
        Destroy(go2, 1f);

        Destroy(gameObject,.1f);
    }

}
