using System.Collections.Generic;
using UnityEngine;

namespace Graphene.BehaviourTree
{
    public class Tick
    {
        public Tick()
        {
            Initialize();
        }

        public List<Node> openNodes;
        public int nodeCount;
        public Blackboard blackboard;
        public GameObject target;
        public Behaviour tree;

        protected virtual void Initialize()
        {
            tree = null;
            openNodes = new List<Node>();
            nodeCount = 0;
            target = null;
            blackboard = null;
        }

        public void EnterNode(Node node)
        {
            this.nodeCount++;
            this.openNodes.Add(node);
        }

        public void OpenNode(Node node) { }

        public void TickNode(Node node) { }

        public void CloseNode(Node node)
        {
            this.openNodes.Remove(node);
        }

        public void ExitNode(Node node) { }
    }
}