using System.Collections.Generic;

namespace Graphene.BehaviourTree.Composites
{
    public class MemoryPriority : Composite
    {
        public MemoryPriority(List<Node> children) : base(children) { }

        public override void Open(Tick tick)
        {
            base.Open(tick);
            tick.blackboard.Set((int)InternalKeys.RunningChild, 0, tick.tree.id, this.id);
        }

        public override NodeStates Tick(Tick tick)
        {
            var child = (int)tick.blackboard.Get((int)InternalKeys.RunningChild, tick.tree.id, this.id).value;

            for (var i=child; i<this.children.Count; i++) {
                var status = this.children[i].Execute(tick);
 
                if (status != NodeStates.Failure) {
                    if (status == NodeStates.Running) {
                        tick.blackboard.Set((int)InternalKeys.RunningChild, i, tick.tree.id, this.id);
                    }
                    return status;
                }
            }
            return NodeStates.Failure;
        }
    }
}