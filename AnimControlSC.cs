using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimControlSC : MonoBehaviour
{
    public void EatingEnd()
    {
        CollideSome.instance.EatingEnds();
    }
}