namespace Graphene.BehaviourTree.Conditions
{
    public class CheckIsDead : Condition
    {
        public override NodeStates Tick(Tick tick)
        {
            base.Tick(tick);

            if (tick.blackboard.Get((int)InternalKeys.IsDead, tick.tree.id).value == null) return NodeStates.Failure;

            var isDead = (bool)tick.blackboard.Get((int)InternalKeys.IsDead, tick.tree.id).value;

            if (isDead)
            {
                return NodeStates.Failure;
            }
            else
            {
                return NodeStates.Success;
            }
        }
    }
}