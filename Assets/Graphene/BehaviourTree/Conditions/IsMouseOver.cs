﻿using UnityEngine;

namespace Graphene.BehaviourTree.Conditions
{
    public class IsMouseOver : Condition
    {
        public override NodeStates Tick(Tick tick)
        {
            if (Input.GetMouseButton(0))
            {
                return NodeStates.Success;
            }
            else
            {
                return NodeStates.Failure;
            }
        }
    }
}