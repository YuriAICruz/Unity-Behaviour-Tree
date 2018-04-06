using System.Collections.Generic;
using Graphene.BehaviourTree.Nodes;

namespace Graphene.BehaviourTree.Composite
{
    public class Composite : BaseNode
    {
        public Composite(List<Node> children) : base(children) { }
    }
}