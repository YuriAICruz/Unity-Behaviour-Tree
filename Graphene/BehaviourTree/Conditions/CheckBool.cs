namespace Graphene.BehaviourTree.Conditions
{
    public class CheckBool : Condition
    {
        public int _dataId;

        public CheckBool(int dataId)
        {
            this._dataId = dataId;
        }

        public override NodeStates Tick(Tick tick)
        {
            if (tick.blackboard.Get(_dataId, tick.tree.id).value == null) return NodeStates.Failure;
            var chk = (bool) tick.blackboard.Get(_dataId, tick.tree.id).value;

            if (chk)
                return NodeStates.Success;

            return NodeStates.Failure;
        }
    }
}