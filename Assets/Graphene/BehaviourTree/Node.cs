using System;
using System.Collections.Generic;

namespace Graphene.BehaviourTree
{
    public enum InternalKeys{
        IsOpen = 99990,
        StartTime = 99991,
        OpenNodes = 99992,
        NodeCount = 99993,
        RunningChild = 99994,
        Seed = 99995,
        Transform = 99996,
        Hp = 99997,
        IsDead = 99998
    }
    public abstract class Node
    {
        public Guid id;
        public List<Node> children;

        protected virtual void Initialize(List<Node> children) { }

        public virtual NodeStates Execute(Tick tick)
        {
            return NodeStates.Error;
        }

        public virtual void Enter(Tick tick) { }
        public virtual void Open(Tick tick) { }
        public virtual NodeStates Tick(Tick tick)
        {
            return NodeStates.Error;
        }
        public virtual void Close(Tick tick) { }
        public virtual void Exit(Tick tick) { }
    }
}