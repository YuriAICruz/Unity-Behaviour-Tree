﻿using System.Collections.Generic;
using Graphene.BehaviourTree.Actions;
using Graphene.BehaviourTree.Composites;
using Graphene.BehaviourTree.Conditions;
using UnityEngine;

namespace Graphene.BehaviourTree.Examples
{
    public class MoveUIExample : MonoBehaviour
    {
        public Vector3 Limits;

        private Behaviour _tree;
        private Blackboard _blackboard;

        void Awake()
        {
            _blackboard = new Blackboard();
            _tree = new Behaviour();

            _tree.root = new Priority(
                new List<Node>
                {
                    new Sequence(new List<Node>()
                    {
                        new IsMouseOver(),
                        new MemorySequence(new List<Node>()
                        {
                            new ChangeColor(Color.red),
                            new Wait(0.5f),
                            new ChangePosition(),
                            new ChangeColor(Color.blue)
                        })
                    }),
                    new ChangeColor(Color.blue),
                });
        }

        void Update()
        {
            _tree.Tick(this.gameObject, _blackboard);
        }
    }
}