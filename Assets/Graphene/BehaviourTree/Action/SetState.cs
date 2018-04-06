namespace Graphene.BehaviourTree.Action
{
    public class SetState : Action
    {
        public override NodeStates Tick(Tick tick)
        {
           return NodeStates.Success;
        }
    }
}