using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game
{
    public class Waypoints : Script
    {
        public static Actor[] points;

        public override void OnStart()
        {
            points = new Actor[Actor.ChildrenCount];

            for (int i = 0; i < points.Length; i++)
            {
                points[i] = Actor.GetChild(i);
            }
        }
    }
    
    
}
