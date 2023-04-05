using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelocatePlayerInit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int i = Random.Range(0, Main.main.AracDest.Count);
        transform.position = Main.main.AracDest[i].position;
        transform.rotation = Main.main.AracDest[i].rotation;
    }

 
}
