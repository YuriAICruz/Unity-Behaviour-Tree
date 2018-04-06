namespace Graphene.BehaviourTree.Actions
{
    public class CallSystemAction: Action
    {
        private int _actionId;

        public CallSystemAction(int actionId)
        {
            this._actionId = actionId;
        }
        public override NodeStates Tick(Tick tick)
        {
            base.Tick(tick);

            if (tick.blackboard.Get(_actionId, tick.tree.id).value == null) return NodeStates.Failure;

            var action = (System.Action)tick.blackboard.Get(_actionId, tick.tree.id).value;

            action.Invoke();

            return NodeStates.Success;
        }
    }
}