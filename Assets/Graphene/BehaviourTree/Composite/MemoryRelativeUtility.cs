using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Graphene.BehaviourTree.Composite
{
    public static class UtilityWeightMath
    {
        /*
        public static float GetWeight(ContitionsGroup conds, CreatureBase tgCreature)
        {
            int sum = 0;

            if (conds.Conditions - (int) tgCreature.Conditions == 0)
            {
                sum += 1;
            }
            else
            {
                Scripts.Views.Creature.Conditions enu = (Scripts.Views.Creature.Conditions) Enum.Parse(typeof(Scripts.Views.Creature.Conditions), conds.Conditions.ToString());

                if ((tgCreature.Conditions & enu) != 0)
                {
                    sum += 1;
                }
            }
            if (conds.Mind - (int) tgCreature.Mind == 0)
            {
                sum += 1;
            }
            else
            {
                Mind enu = (Mind)Enum.Parse(typeof(Mind), conds.Mind.ToString());

                if ((tgCreature.Mind & enu) != 0)
                {
                    sum += 1;
                }
            }

            States enuS = (States)Enum.Parse(typeof(States), conds.State.ToString());

            if ((tgCreature.State & enuS) != 0)
            {
                sum += 1;
            }
            else
            {
                sum = 0;
            }

            return sum + conds.Buff;
        }
        /**/
    }

    public class ContitionsGroup
    {
        public int Conditions, Mind, State;
        public float Buff = 0;
    }
    
    public class MemoryRelativeUtility : Composite
    {
        private List<float> weight;
        private List<ContitionsGroup> contitionsGroup;
        private string name;
        private bool hasPriorityList;

        public MemoryRelativeUtility(List<Node> children, string name) : base(children)
        {
            this.name = name;
            hasPriorityList = false;
        }
        public MemoryRelativeUtility(List<Node> children, List<ContitionsGroup> contitionsGroup, string name) : base(children)
        {
            this.name = name;
            this.contitionsGroup = contitionsGroup;
            hasPriorityList = false;
        }
        public MemoryRelativeUtility(List<Node> children, List<float> weight, string name) : base(children)
        {
            this.name = name;
            this.weight = weight;
            hasPriorityList = true;
        }

        public override void Open(Tick tick)
        {
            base.Open(tick);
            tick.blackboard.Set((int)InternalKeys.RunningChild, -1, tick.tree.id, this.id);
        }

        public override NodeStates Tick(Tick tick)
        {
            if(children.Count <=0) return NodeStates.Failure;

            var child = (int)tick.blackboard.Get((int)InternalKeys.RunningChild, tick.tree.id, this.id).value;

            System.Random rnd;

            if (tick.blackboard.Get((int)InternalKeys.Seed, tick.tree.id).value != null)
            {
                rnd = new System.Random((int)tick.blackboard.Get((int)InternalKeys.Seed, tick.tree.id).value);
            }
            else
            {
                rnd = new System.Random();
            }

            var i = child >= 0 ? child : 0;

            var mem = this.children[i];

            if (hasPriorityList)
                weight = children.Shuffle(weight);
            else
            {
                Debug.LogError("Auto priority list not supported by now");
            }

            i = children.FindIndex(x=> x == mem);

            var n = this.children.Count;
            while (true)
            {
                if (child == i)
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

                        tick.blackboard.Set((int)InternalKeys.RunningChild, -1, tick.tree.id, this.id);
                        continue;
                    }

                    var status = this.children[i].Execute(tick);

                    if (status == NodeStates.Success)
                    {
                        tick.blackboard.Set((int)InternalKeys.RunningChild, -1, tick.tree.id, this.id);

                        return status;
                    }

                    return status;
                }
                else
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

                    if (probability < weight[i] / max)
                    {
                        var status = this.children[i].Execute(tick);

                        if (status == NodeStates.Running)
                        {
                            tick.blackboard.Set((int)InternalKeys.RunningChild, i, tick.tree.id, this.id);
                        }

                        return status;
                    }
                }

                i++;
                if (i >= n)
                {
                    i = 0;
                }
            }

            return NodeStates.Failure;
        }

        private List<float> CalculateWeight(Tick tick)
        {
            var wh = new List<float>();
            /*
            var tg = (CreatureBase)tick.blackboard.Get("creature", tick.tree.id, tick.tree.id).value;
            var hasConditions = contitionsGroup.Count > 0;

            for (int i = 0, n = hasConditions ? contitionsGroup.Count : children.Count; i < n; i++)
            {
                if (hasConditions)
                {
                    wh.Add(UtilityWeightMath.GetWeight(contitionsGroup[i], tg));
                }
                else
                {
                    wh.Add(1);
                }
            }
            /**/
            return wh;
        }

        float Probability(int index)
        {
            return weight[index]/(float)children.Count;
        }
    }
}