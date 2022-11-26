using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game
{
    public class SinMovement : Script
    {
        public float frequency = 5f;
        public float magnitude = 5f;
        public float offset = 0f;
        public float offsetUp = 10f;

        private Vector3 _startingPosition;

        public override void OnStart()
        {
            _startingPosition = this.Actor.Parent.Position + new Vector3(0, offsetUp, 0);
        }
        public override void OnUpdate()
        {
            this.Actor.Position = _startingPosition + Transform.Up * Mathf.Sin(Time.GameTime * frequency + offset) * magnitude;
        }

    }
}
