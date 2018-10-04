namespace Graphene.BehaviourTree.Actions
{
    public class ClearData : Action
    {
        public int _nodeId;

        public ClearData(int nodeId)
        {
            this._nodeId = nodeId;
        }
        public override NodeStates Tick(Tick tick)
        {
           tick.blackboard.Set(_nodeId, null, tick.tree.id);
           return NodeStates.Success;
        }
    }
}