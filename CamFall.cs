using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFall : MonoBehaviour
{
    [SerializeField]
    Vector3 offset;
    [SerializeField] Transform target;
    // Update is called once per frame
    void Update()
    {

        transform.position = target.position + offset;
    }

    private void LateUpdate()
    {
        Vector3 rot = target.position - transform.position;


        transform.rotation = Quaternion.FromToRotation(Vector3.forward, rot);
    }
}
