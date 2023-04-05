using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfektBitir : MonoBehaviour
{
    ParticleSystem ps;
    Vector3 initScale;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
        initScale = transform.localScale;
    }

    private void Update()
    {
        if (ps.gameObject.activeInHierarchy)
        {
            if (!ps.IsAlive())
            {
                gameObject.SetActive(false);
                gameObject.transform.localPosition = Vector3.zero;
                gameObject.transform.localScale = initScale;
            }
        }
    }


}
