using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Graphene.BehaviourTree
{
    public enum NodeStates
    {
        Success = 0,
        Failure = 1,
        Running = 2,
        Error,
        Null
    }

    public class Behaviour
    {
        public delegate NodeStates NodeResponseAction();
        
        private const string HexDigits = "0123456789abcdef";

        public Guid id;
        public Node root;

        public Behaviour()
        {
            Initialize();
        }

        public static Guid CreateUID()
        {
            var uid = Guid.NewGuid();
            
            return uid;
        }

        protected virtual void Initialize()
        {
            id = CreateUID();
            root = null;
        }

        public void Tick(GameObject target, Blackboard blackboard)
        {
            var tick = new Tick();
            tick.target = target;
            tick.blackboard = blackboard;
            tick.tree = this;

            /* TICK NODE */
            this.root.Execute(tick);

            /* CLOSE NODES FROM LAST TICK, IF NEEDED */
            List<Node> lastOpenNodes;
            lastOpenNodes = new List<Node>();

            try
            {
                lastOpenNodes = (List<Node>) blackboard.Get((int)InternalKeys.OpenNodes, id).value;
            }
            catch (Exception){ }

            var currOpenNodes = new List<Node>(tick.openNodes);

            // does not close if it is still open in this tick
            var start = 0;
            int n = 0;

            if (lastOpenNodes == null)
                n = currOpenNodes.Count;
            else
                n = Math.Min(lastOpenNodes.Count, currOpenNodes.Count);


            if (lastOpenNodes != null)
            {
                for (int i = 0; i < n; i++)
                {
                    start = i + 1;
                    if ((Node) lastOpenNodes[i] != currOpenNodes[i])
                    {
                        break;
                    }
                }
            }
            else
                start = currOpenNodes.Count;

            // close the nodes
            if (lastOpenNodes != null)
                for (var i = lastOpenNodes.Count - 1; i >= start; i--)
                {
                    lastOpenNodes[i].Close(tick);
                }

            /* POPULATE BLACKBOARD */
            blackboard.Set((int)InternalKeys.OpenNodes, currOpenNodes, id);
            blackboard.Set((int)InternalKeys.NodeCount, tick.nodeCount, id);
        }
    }
}