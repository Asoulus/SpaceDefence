using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game
{
    public class ObjectRotation : Script
    {       
        public float rotationDegrees = 250f;

        public override void OnUpdate()
        {
            Quaternion rotation = Quaternion.Euler(0, rotationDegrees * Time.DeltaTime, 0);
            this.Actor.AddMovement(new Vector3(0, 0, 0), rotation);       
        }
    }
}
