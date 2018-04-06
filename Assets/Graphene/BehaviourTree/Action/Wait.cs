using UnityEngine;

namespace Graphene.BehaviourTree.Action {
    public class Wait : Action {
        public Wait (float endTime) {
            this.endTime = endTime;
        }

        public float endTime;

        public override void Open (Tick tick) {
            base.Open (tick);

            var startTime = Time.timeSinceLevelLoad;
            tick.blackboard.Set ((int) InternalKeys.StartTime, startTime, id, this.id);
        }

        public override NodeStates Tick (Tick tick) {
            base.Tick (tick);

            var currTime = Time.timeSinceLevelLoad;
            var startTime = (float) tick.blackboard.Get ((int) InternalKeys.StartTime, id, this.id).value;

            if (currTime - startTime > endTime) {
                return NodeStates.Success;
            }

            return NodeStates.Running;
        }
    }
}