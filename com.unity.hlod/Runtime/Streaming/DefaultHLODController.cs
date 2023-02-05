using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.HLODSystem.Streaming
{
    public partial class DefaultHLODController : HLODControllerBase
    {
        [SerializeField] List<GameObject> m_gameObjectList = new List<GameObject>();
        [SerializeField] List<GameObject> m_lowGameObjects = new List<GameObject>();
        

        public int AddHighObject(GameObject gameObject)
        {
            var id = m_gameObjectList.Count;
            m_gameObjectList.Add(gameObject);
            return id;
        }

        public int AddLowObject(GameObject gameObject)
        {
            var id = m_lowGameObjects.Count;
            m_lowGameObjects.Add(gameObject);
            return id;
        }
        public override int HighObjectCount => m_gameObjectList.Count;
        public override int LowObjectCount => m_lowGameObjects.Count;

        protected override void OnStart()
        {

        }

        protected override void OnStop()
        {

        }

        public override void Install()
        {
            for (int i = 0; i < m_gameObjectList.Count; ++i)
            {
                m_gameObjectList[i].SetActive(false);
            }
        }

        public override void LoadHighObject(int id, Action<GameObject> loadDoneCallback)
        {
            loadDoneCallback?.Invoke(m_gameObjectList[id]);
        }

        public override void LoadLowObject(int id, Action<GameObject> loadDoneCallback)
        {
            loadDoneCallback?.Invoke(m_lowGameObjects[id]);
        }

        public override void UnloadHighObject(int id)
        {
            m_gameObjectList[id].SetActive(false);
        }

        public override void UnloadLowObject(int id)
        {
            m_lowGameObjects[id].SetActive(false);
        }

    }

}