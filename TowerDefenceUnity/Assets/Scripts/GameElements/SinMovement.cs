using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinMovement : MonoBehaviour
{
    [SerializeField]
    private float _frequency = 5f;
    [SerializeField]
    private float _magnitude = 5f;
    [SerializeField]
    private float _offset = 0f;

    private Vector3 _startingPosition;

    private void Start()
    {
        _startingPosition = transform.position + new Vector3(0, 0.15f, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = _startingPosition + transform.up * Mathf.Sin(Time.time * _frequency + _offset) * _magnitude;
    }

}
