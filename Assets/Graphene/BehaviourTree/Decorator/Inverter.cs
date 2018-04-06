﻿using System.Collections.Generic;

namespace Graphene.BehaviourTree.Decorators
{
    public class Inverter : Decorator
    {
        public Inverter(List<Node> children) : base(children) { }

        public override NodeStates Tick(Tick tick)
        {
            var child = this.children[0];

            if (children == null || children.Count == 0)
            {
                return NodeStates.Error;
            }

            var status = child.Execute(tick);

            if (status == NodeStates.Success)
                status = NodeStates.Failure;
            else if (status == NodeStates.Failure)
                status = NodeStates.Success;

            return status;
        }
    }
}