namespace Graphene.BehaviourTree.Actions
{
    public class CallSystemActionWithResponse : Action
    {
        public delegate NodeStates ActionN();

        private int _actionId;

        public CallSystemActionWithResponse(int actionId)
        {
            this._actionId = actionId;
        }

        public override NodeStates Tick(Tick tick)
        {
            base.Tick(tick);

            if (tick.blackboard.Get(_actionId, tick.tree.id).value == null) return NodeStates.Failure;

            var action = (ActionN) tick.blackboard.Get(_actionId, tick.tree.id).value;

            if (action == null) return NodeStates.Failure;

            return action.Invoke();
        }
    }
}