namespace Graphene.BehaviourTree.Conditions
{
    public class CheckDataNotNull : Condition
    {
        public int dataId;

        public CheckDataNotNull(int dataId)
        {
            this.dataId = dataId;
        }

        public override NodeStates Tick(Tick tick)
        {
            if (tick.blackboard.Get(dataId, tick.tree.id).value == null) return NodeStates.Failure;

            return NodeStates.Success;
        }
    }
}