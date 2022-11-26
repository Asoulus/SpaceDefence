using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladesRotation : MonoBehaviour
{
    [SerializeField]
    private float _rotationDegrees = 250f;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(0, _rotationDegrees * Time.deltaTime, 0); 
    }

}
