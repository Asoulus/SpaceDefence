using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game
{
    public class CameraRotator : Script
    {
        [Header("Attributes")]
        public float speed = 5f;

        public override void OnUpdate()
        {
            Quaternion rotation = Quaternion.Euler(0, speed * Time.DeltaTime, 0);
            this.Actor.AddMovement(new Vector3(0, 0, 0), rotation);
        }
    }
}
