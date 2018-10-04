using System.Collections.Generic;
using UnityEngine;

namespace Graphene.BehaviourTree.Composites
{
    public class MemorySequence : Composite
    {
        public MemorySequence(List<Node> children) : base(children) { }

        public override void Open(Tick tick)
        {
            base.Open(tick);

            tick.blackboard.Set((int)InternalKeys.RunningChild, 0, tick.tree.id, this.id);
        }

        public override NodeStates Tick(Tick tick)
        {
            var child = (int)tick.blackboard.Get((int)InternalKeys.RunningChild, tick.tree.id, this.id).value;

            for (int i = child, n = this.children.Count; i < n; i++) 
            {
                var status = this.children[i].Execute(tick);

                if (status != NodeStates.Success)
                {
                    if (status == NodeStates.Running)
                    {
                        tick.blackboard.Set((int)InternalKeys.RunningChild, i, tick.tree.id, this.id);
                    }
                    return status;
                }
                tick.blackboard.Set((int)InternalKeys.RunningChild, i + 1, tick.tree.id, this.id);
            }
 
            return NodeStates.Success;
        }
    }
}