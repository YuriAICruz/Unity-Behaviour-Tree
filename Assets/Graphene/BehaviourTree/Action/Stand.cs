namespace Graphene.BehaviourTree.Action
{
    public class Stand : Action
    {
        public override NodeStates Tick(Tick tick)
        {
            base.Tick(tick);
            
            return NodeStates.Success;
        }
    }
}