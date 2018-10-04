using System.Collections.Generic;
using Graphene.BehaviourTree.Nodes;

namespace Graphene.BehaviourTree.Decorators
{
    public class Decorator : BaseNode
    {
        public Decorator(List<Node> children) : base(children) { }
    }
}