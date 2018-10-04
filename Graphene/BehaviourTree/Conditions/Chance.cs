namespace Graphene.BehaviourTree.Conditions
{
    public class Chance : Condition
    {
        private float percentage;
        public Chance(float percentage)
        {
            this.percentage = percentage;
        }

        public override NodeStates Tick(Tick tick)
        {
            var rnd = new System.Random();
            var res = rnd.NextDouble();

            if(res <= percentage)
                return NodeStates.Success;

            return NodeStates.Failure;
        }
    }
}