using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game
{
    public class ScaleUpOverLifeTime : Script
    {
        public float scaleAmout = 0.01f;

        public override void OnUpdate()
        {
            Actor.Scale = Actor.Scale + new Vector3(scaleAmout, scaleAmout, scaleAmout);
        }
    }
}
