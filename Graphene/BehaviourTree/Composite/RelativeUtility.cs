using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Graphene.BehaviourTree.Composites
{
    public class RelativeUtility : Composite
    {
        private List<float> weight;

        public RelativeUtility(List<Node> children, List<float> weight) : base(children)
        {
            this.weight = weight;
        }
        
        public override NodeStates Tick(Tick tick)
        {
            System.Random rnd;

            if (tick.blackboard.Get((int)InternalKeys.Seed, tick.tree.id).value != null)
            {
                rnd = new System.Random((int)tick.blackboard.Get((int)InternalKeys.Seed, tick.tree.id).value);
            }
            else
            {
                rnd = new System.Random();
            }

            weight = children.Shuffle(weight);

            var i = 0;
            var n = this.children.Count;
            while (true)
            {
                if (weight[i] <= 0)
                {
                    i++;
                    if (i >= n)
                    {
                        i = 0;
                    }

                    if (weight.Max() <= 0)
                        break;

                    continue;
                }

                var probability = rnd.NextDouble();
                var max = weight.Max();

                if (probability < weight[i]/max)
                {
                    var status = this.children[i].Execute(tick);

                    return status;
                }

                i++;
                if (i >= n)
                {
                    i = 0;
                }
            }

            return NodeStates.Failure;
        }

        float Probability(int index)
        {
            return weight[index]/(float)children.Count;
        }
    }
}