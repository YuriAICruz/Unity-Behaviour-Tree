using UnityEngine;

namespace Graphene.BehaviourTree.Actions
{
    public class DebugLog: Action
    {
        public string text;
        public DebugLog() { }
        public DebugLog(string text)
        {
            this.text = text;
        }

        public override void Open(Tick tick)
        {
            base.Open(tick);
        }

        public override NodeStates Tick(Tick tick)
        {
            base.Tick(tick);

            Debug.Log(text);
            
            return NodeStates.Success;
        }
    }
}