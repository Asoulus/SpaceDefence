using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game
{
    /// <summary>
    /// SubmenuAnimation Script.
    /// </summary>
    public class SubmenuAnimation : Script
    {
        private AnimationGraph _graph;
        public Animation Animation;

        public override void OnStart()
        {
            var animatedModel = Actor.As<AnimatedModel>();
            _graph = Content.CreateVirtualAsset<AnimationGraph>();
            _graph.InitAsAnimation(animatedModel.SkinnedModel, Animation);
            animatedModel.AnimationGraph = _graph;
        }

        public override void OnDestroy()
        {
            Destroy(this.Actor);
        }
    }
}
