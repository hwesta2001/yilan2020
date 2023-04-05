using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateKafa : MonoBehaviour
{
    public float _speed=330f;
    private Quaternion _targetRotation;
    private Vector3 _touchPos = Vector3.zero;
   // private Vector3 _touchPosDelta = Vector3.zero;



    private Vector3 DistanceVectorBetweenTwoPoints(Vector3 first, Vector3 second)
    {
        return new Vector3(second.x - first.x, 0, second.y - first.y);
    }


    private void Update()
    {
       // transform.Translate(Vector3.forward * _speed * Time.deltaTime);


        if (Input.GetMouseButtonDown(0))
        {
            _touchPos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {

            float speed = _speed - Main.main.sizeCount;

            if (DistanceVectorBetweenTwoPoints(_touchPos, Input.mousePosition) != Vector3.zero)
                _targetRotation = Quaternion.LookRotation(DistanceVectorBetweenTwoPoints(_touchPos, Input.mousePosition));

            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, speed * Time.deltaTime);

        }
    }


}