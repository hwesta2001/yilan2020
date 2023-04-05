using UnityEngine;
using UnityEngine.AI;
public class AracNavHaraket : MonoBehaviour
{
    NavMeshAgent agent;
    Transform dest;
	
    private float speed = 25f;
    private bool wake;
    private float delayTime;

    [SerializeField] Transform govde;
    private Vector3 previousPosition;
    private float donSpeed;
    private void Awake()
    {
        wake = false;
        agent = GetComponent<NavMeshAgent>();
        govde = transform.Find("Govde");
    }
    private void OnEnable()
    {
        if (wake)
        {
            delayTime = 2;
        }
        else
        {

            delayTime = 5;
        }

        Invoke("RotaSec", delayTime);
    }

    private void RotaSec()
    {
        if (!wake)
        {
            wake = true;
        }

        agent.enabled = true;
        int i = Random.Range(0, Main.main.AracDest.Count);
        dest = Main.main.AracDest[i];
        Vector3 dir = dest.position;
        RotayaGit(dir);
        donSpeed = 10;

    }

    private void RotayaGit( Vector3 dir)
    {
        
        float rSpeed = Random.Range(speed -12f, speed);
        agent.avoidancePriority = Random.Range(10, 50);
        agent.speed = rSpeed;
        if(gameObject && agent)
        {
            agent.SetDestination(dir);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AracDestination"))
        {
            agent.enabled = false;
            RotaSec();
        }

        if (other.CompareTag("BodyPart"))
        {
            Uzaklas(other.transform, agent);
        }

        //if (other.CompareTag("Arac"))
        //{
        //    donSpeed = .1f;
        //    agent.ResetPath();

        //}


    }

    private void Update()
    {
        if (agent.enabled==true)
        {
            if (agent.remainingDistance < 0.02f)
            {
                RotaSec();
            }
        }

        
        DikeyeDon(previousPosition, govde);
    }


        void DikeyeDon(Vector3 toTar, Transform itself)
        {

            Vector3 aimingDir = itself.position - toTar;
            float angle = -Mathf.Atan2(aimingDir.z, aimingDir.x) * Mathf.Rad2Deg + 90.0f;
            angle = Mathf.Round(angle / 90.0f) * 90.0f;
            Quaternion qTo = Quaternion.AngleAxis(angle, Vector3.up);
            itself.rotation = Quaternion.Lerp(itself.rotation, qTo, donSpeed * Time.deltaTime);
            previousPosition = transform.position;
        }



    void Uzaklas(Transform target, NavMeshAgent ag)
    {

            Vector3 dif = transform.position - target.position;
            Vector3 newTargetPos = transform.position + dif;

            ag.SetDestination(newTargetPos);

    }

}
