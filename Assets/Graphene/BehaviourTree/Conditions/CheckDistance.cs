using UnityEngine;

namespace Graphene.BehaviourTree.Conditions
{
    public class CheckDistance : Condition
    {
        public float distance;
        public int _targetId;

        public CheckDistance(float distance, int targetId)
        {
            this.distance = distance;
            this._targetId = targetId;
        }

        public override NodeStates Tick(Tick tick)
        {
            if (tick.blackboard.Get((int)InternalKeys.Transform, tick.tree.id).value == null) return NodeStates.Failure;
            if (tick.blackboard.Get(_targetId, tick.tree.id).value == null) return NodeStates.Failure;

            var transform = (Transform)tick.blackboard.Get((int)InternalKeys.Transform, tick.tree.id).value;
            var target = (Transform)tick.blackboard.Get(_targetId, tick.tree.id).value;

            if (Vector3.Distance(transform.position, target.transform.position) <= distance)
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