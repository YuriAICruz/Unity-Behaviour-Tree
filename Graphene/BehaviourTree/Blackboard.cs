using System;
using System.Collections.Generic;
using UnityEngine;

namespace Graphene.BehaviourTree
{
    public class Blackboard
    {
        public class Memory
        {
            public Memory()
            {
                
            }
            public Memory(int key)
            {
                this.key = key;
                value = null;
            }

            public int key;
            public object value;

            public void Set(int key, object value)
            {
                this.key = key;
                this.value = value;
            }
        }

        private Memory _baseMemory ;
        private Dictionary<Guid, List<Memory>> _treeMemory ;

        public Blackboard()
        {
            //Init here
            Initialize();
        }
        protected virtual void Initialize()
        {
            _baseMemory = new Memory();
            _treeMemory = new Dictionary<Guid, List<Memory>>();
        }

        public void Set(int key, object value, Guid treeScope, Guid nodeScope)
        {
            var memory = GetMemory(key, treeScope, nodeScope);
            memory.Set(key, value);
        }
        public void Set(int key, object value, Guid treeScope)
        {
            var memory = GetMemory(key, treeScope, Guid.Empty);
            memory.Set(key, value);
        }

        public Memory Get(int key, Guid treeScope, Guid nodeScope)
        {
            var memory = GetMemory(key, treeScope, nodeScope);

            if (memory.key == key)
                return memory;
            else
            {
                Debug.LogError("Notfound: " + key);
                return memory;
            }
        }
        public Memory Get(int key, Guid treeScope)
        {
            var memory = GetMemory(key, treeScope, Guid.Empty);

            if (memory.key == key)
                return memory;
            else
            {
                Debug.LogError("Notfound: " + key);
                return memory;
            }
        }

        protected Memory GetTreeMemory(int key, Guid treeScope)
        {
            if (!_treeMemory.ContainsKey(treeScope))
            {
                //Debug.Log("Adding " + treeScope);
                _treeMemory.Add(treeScope, new List<Memory>());
            }
            if(!_treeMemory[treeScope].Exists(x => x.key == key))
            {
                //Debug.Log("Adding on " + treeScope + " key: " + key);
                _treeMemory[treeScope].Add(new Memory(key));
            }

            return _treeMemory[treeScope].Find(x => x.key == key);
        }

        protected Memory GetNodeMemory(int key, Dictionary<Guid, List<Memory>> treeMemory, Guid nodeScope)
        {
            if (!treeMemory.ContainsKey(nodeScope))
            {
                //Debug.Log("Adding " + nodeScope);
                treeMemory.Add(nodeScope, new List<Memory>());
            }
            else if (!_treeMemory[nodeScope].Exists(x => x.key == key))
            {
                treeMemory[nodeScope].Add(new Memory(key));
            }

            return treeMemory[nodeScope].Find(x => x.key == key);
        }

        protected Memory GetMemory(int key, Guid treeScope, Guid nodeScope)
        {
            var memory = GetTreeMemory(key, treeScope);

            if (nodeScope != null && _treeMemory.ContainsKey(nodeScope))
            {
                memory = GetNodeMemory(key, _treeMemory, nodeScope);
            }
 
            return memory;
        }
    }
}