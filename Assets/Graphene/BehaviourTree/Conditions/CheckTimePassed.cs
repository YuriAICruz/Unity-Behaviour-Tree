using UnityEngine;

namespace Graphene.BehaviourTree.Conditions
{
    public class CheckTimePassed : Condition
    {
        private readonly float _time;
        private readonly int _currentTimeId;

        public CheckTimePassed(float time, int currentTimeId)
        {
            _time = time;
            _currentTimeId = currentTimeId;
        }

        public override NodeStates Tick(Tick tick)
        {
            var value = tick.blackboard.Get(_currentTimeId, tick.tree.id).value;
            if (value == null) return NodeStates.Failure;

            var time = (float) value;

            if (Time.time - time > _time)
            {
                return NodeStates.Success;
            }
            
            return NodeStates.Failure;
        }
    }
}