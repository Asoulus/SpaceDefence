using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField]
    private float _movementSpeed  = 50f;
    [SerializeField]
    private float _scrollSpeed = 50f;
    [SerializeField]
    private float _minY = 20f;
    [SerializeField]
    private float _maxY = 80f;
    [SerializeField]
    private float _minX = 20f;
    [SerializeField]
    private float _maxX = 80f;
    [SerializeField]
    private float _minZ = 20f;
    [SerializeField]
    private float _maxZ = 80f;

    private GameManager _gameManager;

    private bool _doMovement = true;


    private void Start()
    {
        _gameManager = GameManager.GetInstance();
    }

    public void Update()
    {
        if (_gameManager.IsGameOver())
        {
            _doMovement = false;
        }

        if (!_doMovement)
            return;

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * _movementSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * _movementSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * _movementSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * _movementSpeed * Time.deltaTime, Space.World);
        }      

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;
        pos.y -= scroll * 1000 * _scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, _minY, _maxY);
        pos.x = Mathf.Clamp(pos.x, _minX, _maxX);
        pos.z = Mathf.Clamp(pos.z, _minZ, _maxZ);

        transform.position = pos;
    }   
}
