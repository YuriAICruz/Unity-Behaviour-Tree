namespace Graphene.BehaviourTree.Conditions
{
    public class CheckAudition : Condition
    {
        public override NodeStates Tick(Tick tick)
        {
            return NodeStates.Success;
        }
    }
}