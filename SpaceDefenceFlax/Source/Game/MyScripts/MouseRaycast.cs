using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game
{
    public class MouseRaycast : Script
    {
        public Material selectedMaterial;
        public Material nodeMaterial;
        public Material hoverMaterial;
        public Node _selectedNode = null;
        public Node _hoveredNode = null;

        private float _rayacstCountdown = .1f;
        /// <inheritdoc/>
        public override void OnUpdate()
        {
            if (_rayacstCountdown <= 0)
            {
                Raycast();
                _rayacstCountdown = .1f;
            }

            _rayacstCountdown -= Time.DeltaTime;

            if (Input.GetMouseButtonDown(MouseButton.Left))
            {
                var pos = Input.MousePosition;
                var ray = Camera.MainCamera.ConvertMouseToRay(pos);


                if (Physics.RayCast(ray.Position, ray.Direction, out RayCastHit hit, 3.40282347E+38F, 4294967295U, false))
                {

                    Actor tmp = hit.Collider.As<Actor>().Parent;
                    Node tmpNode = tmp.GetScript<Node>();

                    if (tmpNode)
                    {
                        if (_selectedNode == tmpNode)
                        {
                            tmpNode.Actor.As<StaticModel>().SetMaterial(0, nodeMaterial);
                            _selectedNode = null;
                            return;
                        }

                        _selectedNode = tmpNode;
                        tmp.As<StaticModel>().SetMaterial(0, selectedMaterial);

                        if (CanBuildTurretAtNode(_selectedNode))
                        {
                            _selectedNode.BuildMyTurret();
                        }
                    }
                }
            }
        }

        private bool CanBuildTurretAtNode(Node node)
        {
            if (node.Turret == null)
            {
                return true;
            }

            Debug.Log("TURRET HERE ALREADY");
            return false;
        }

        private void Raycast()
        {
            var pos = Input.MousePosition;
            var ray = Camera.MainCamera.ConvertMouseToRay(pos);

            if (Physics.RayCast(ray.Position, ray.Direction, out RayCastHit hit, 3.40282347E+38F, 4294967295U, false))
            {
                Actor tmp = hit.Collider.As<Actor>().Parent;
                Node tmpNode = tmp.GetScript<Node>();

                if (tmpNode)
                {
                    /*if (_selectedNode == null && _hoveredNode == _selectedNode)
                        return;*/

                    if (tmpNode.Turret != null)
                    {
                        RestoreMaterialToPreviousNode();
                        return;
                    }
                       

                    if (_hoveredNode != null &&_hoveredNode != tmpNode)
                    {
                        _hoveredNode.Actor.As<StaticModel>().SetMaterial(0, nodeMaterial);
                        _hoveredNode = null;
                    }

                    _hoveredNode = tmpNode;
                    tmp.As<StaticModel>().SetMaterial(0, hoverMaterial);
                }
                else //if there is no node script return color (no node detected)
                {
                    RestoreMaterialToPreviousNode();
                }
            }
        }

        private void RestoreMaterialToPreviousNode()
        {
            if (_hoveredNode != null)
            {
                _hoveredNode.Actor.As<StaticModel>().SetMaterial(0, nodeMaterial);
                _selectedNode = null;
            }
        }
    }
}
