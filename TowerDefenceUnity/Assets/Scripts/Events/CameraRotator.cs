using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField]
    private float _speed = 5f;

    void Update()
    {
        transform.Rotate(0, _speed * Time.deltaTime, 0);
    }
}
