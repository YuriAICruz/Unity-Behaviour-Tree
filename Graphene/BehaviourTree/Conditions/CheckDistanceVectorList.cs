using System.Collections.Generic;
using UnityEngine;

namespace Graphene.BehaviourTree.Conditions
{
    public class CheckDistanceVectorList : Condition
    {
        public float distance;
        public int _targetId;

        public CheckDistanceVectorList(float distance, int targetId)
        {
            this.distance = distance;
            this._targetId = targetId;
        }

        public override NodeStates Tick(Tick tick)
        {
            if (tick.blackboard.Get(_targetId, tick.tree.id).value == null) return NodeStates.Failure;

            var transform = tick.target.transform;
            var targets = (List<Vector3>)tick.blackboard.Get(_targetId, tick.tree.id).value;

            foreach (var target in targets)
            {
                if (Vector3.Distance(transform.position, target) <= distance)
                {
                    tick.blackboard.Set((int)InternalKeys.TargetPosition, target, tick.tree.id);
                    return NodeStates.Success;
                }
            }
            return NodeStates.Failure;
        }
    }
}