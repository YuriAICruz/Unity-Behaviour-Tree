namespace Graphene.BehaviourTree.Conditions
{
    public class CheckHP : Condition
    {
        public int lowLimit;

        public CheckHP() { }

        public CheckHP(int lowHp)
        {
            lowLimit = lowHp;
        }
        public override NodeStates Tick(Tick tick)
        {
            base.Tick(tick);

            if (tick.blackboard.Get((int)InternalKeys.Hp, tick.tree.id).value == null) return NodeStates.Failure;

            var hp = (int)tick.blackboard.Get((int)InternalKeys.Hp, tick.tree.id).value;

            if (hp <= lowLimit)
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