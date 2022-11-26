using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game
{
    public class CameraMovement : Script
    {
        [Limit(0, 100), Tooltip("Camera movement speed factor")]
        public float MoveSpeed { get; set; } = 4;

        [Tooltip("Camera rotation smoothing factor")]
        public float CameraSmoothing { get; set; } = 20.0f;

        private float pitch;
        private float yaw;
        private float _scroll;

        [Header("Attirbutes")]
        public float minY = -700f;
        public float maxY = 0;
        public float minX = -1000f;
        public float maxX = 1000f;
        public float minZ = -1000f;
        public float maxZ = 1000f;

        public override void OnStart()
        {
            var initialEulerAngles = Actor.Orientation.EulerAngles;
            pitch = initialEulerAngles.X;
            yaw = initialEulerAngles.Y;
        }

        public override void OnUpdate()
        {
            //Screen.CursorVisible = false;
            //Screen.CursorLock = CursorLockMode.Locked;

            Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            pitch = Mathf.Clamp(pitch + mouseDelta.Y, -88, 88);
            yaw += mouseDelta.X;

            
        }

        public override void OnFixedUpdate()
        {
            var camTrans = Actor.Transform;
            var camPos = Actor.Position;
            var camFactor = Mathf.Saturate(CameraSmoothing * Time.DeltaTime);

            //camTrans.Orientation = Quaternion.Lerp(camTrans.Orientation, Quaternion.Euler(pitch, yaw, 0), camFactor);

            var inputH = Input.GetAxis("Horizontal");
            var inputV = Input.GetAxis("Vertical");
            var scroll = Input.GetAxis("Scroll") * 2;
            var move = new Vector3(inputH, scroll, inputV);      
            move.Normalize();
            move = camTrans.TransformDirection(move);

            camTrans.Translation += move * MoveSpeed;

            Actor.Transform = camTrans;
            //-----
            Vector3 pos = Actor.Position;
            pos.Y = Mathf.Clamp(pos.Y, minY, maxY);
            pos.X = Mathf.Clamp(pos.X, minX, maxX);
            pos.Z = Mathf.Clamp(pos.Z, minZ, maxZ);

            Actor.Position = pos;

            //Actor.Position.X = Mathf.Clamp(Actor.Position.X, minX, maxX);
        }
    }
}
