using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.HLODSystem
{
    [Serializable]
    public class HLODTreeNodeContainer
    {
        [SerializeField] List<HLODTreeNode> m_treeNodes = new();

        public int Count => m_treeNodes.Count;

        /**
         * @return node id
         */
        public int Add(HLODTreeNode node)
        {
            var id = m_treeNodes.Count;
            m_treeNodes.Add(node);

            return id;
        }

        public void Remove(int id)
        {
            
        }

        public void Remove(HLODTreeNode node)
        {
        }

        
        public HLODTreeNode Get(int id)
        {
            return m_treeNodes[id];
        }
    }
}