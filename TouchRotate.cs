using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchRotate : MonoBehaviour
{
    [SerializeField] 
    private FixedTouchField TouchField;
    [SerializeField]
    private float rotSpeed=3;

    Vector2 touchD;
    float mx=0, my=0;

    [SerializeField]
    Transform target;
    [SerializeField] float of = 4;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TouchField.gameObject.activeInHierarchy)
        {
            if (TouchField.Pressed)
            {
                RotateThis();
            }
            else
            {
                float offset = of + target.localScale.z * .1f;
                transform.position = target.position + target.forward * offset;
            }
        }
    }





    void RotateThis()
    {
        touchD = TouchField.TouchDist;

        mx = touchD.x;
        my = touchD.y ;


        Vector3 pos = new Vector3(mx, 0, my);
        pos = transform.rotation * pos;
        pos *= rotSpeed;
        transform.Translate(pos, Space.World);

        Vector3 lookPos = transform.position - target.position;
        lookPos.y = 0;
        target.rotation = Quaternion.LookRotation(lookPos);



        if (Vector3.Distance(transform.position, target.position) < of)
        {
            float offset = of + target.localScale.z * .1f;
            transform.position = target.position + target.forward * offset;

        }

    }
}
