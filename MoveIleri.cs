using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveIleri : MonoBehaviour
{
    void Update()
    {
        transform.Translate(transform.forward * 1 * Time.smoothDeltaTime, Space.World);
    }
}
