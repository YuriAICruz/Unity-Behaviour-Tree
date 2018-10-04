namespace Graphene.BehaviourTree.Actions
{
    public class CallSystemActionMemory: Action
    {
        private int _actionId;

        public CallSystemActionMemory(int actionId)
        {
            this._actionId = actionId;
        }
        public override NodeStates Tick(Tick tick)
        {
            base.Tick(tick);

            if (tick.blackboard.Get(_actionId, tick.tree.id).value == null) return NodeStates.Failure;

            var action = (Behaviour.NodeResponseAction)tick.blackboard.Get(_actionId, tick.tree.id).value;

            var res = action.Invoke();

            return res;
        }
    }
}