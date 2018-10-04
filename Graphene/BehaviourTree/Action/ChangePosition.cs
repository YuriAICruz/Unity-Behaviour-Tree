using System;
using UnityEngine;

namespace Graphene.BehaviourTree.Actions
{
    public class ChangePosition : Action
    {
        public override NodeStates Tick(Tick tick)
        {
            var rnd = new System.Random();

            tick.target.transform.localPosition = new Vector3((float)(Math.Floor(rnd.NextDouble() * 800)),(float)(Math.Floor(rnd.NextDouble() * 600)),0);

            return NodeStates.Success;
        }
    }
}