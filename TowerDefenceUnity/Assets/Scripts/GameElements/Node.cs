using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    [SerializeField]
    private Color _hoverColor;
    [SerializeField]
    private Vector3 _positionOffset;

    [Header("Optional")] 
    [SerializeField]
    private GameObject _turret = null;


    private Color _startColor;
    private Renderer _rend;
    private BuildManager _buildManager;


    void Start()
    {
        _rend = GetComponent<Renderer>();
        _buildManager = BuildManager.GetInstance();
        _startColor = _rend.material.color;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + _positionOffset;
    }

    public void SetTurret(GameObject turret)
    {
        _turret = turret;
    }

    private void OnMouseEnter()
    {
        if (!_buildManager.CanBuild)
            return;

        _rend.material.color = _hoverColor;       
    }

    private void OnMouseExit()
    {
        _rend.material.color = _startColor;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!_buildManager.CanBuild)
            return;

        if (_turret != null)
        {
            Debug.Log("Cant build on this node. TODO: display that info");
            return;
        }

        _buildManager.BuildTurretOnNode(this);
    }
}
