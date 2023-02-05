using System.Collections.Generic;
using Unity.HLODSystem.Streaming;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Unity.HLODSystem
{
    public partial class HLOD
    {
#if UNITY_EDITOR
        public List<Object> GeneratedObjects => m_generatedObjects;

        public List<GameObject> ConvertedPrefabObjects => m_convertedPrefabObjects;

        public List<HLODControllerBase> GetHLODControllerBases()
        {
            var controllerBases = new List<HLODControllerBase>();

            foreach (var obj in m_generatedObjects)
            {
                var controllerBase = obj as HLODControllerBase;
                if (controllerBase != null)
                    controllerBases.Add(controllerBase);
            }

            //if controller base doesn't exists in the generated objects, it was created from old version.
            //so adding controller base manually.
            if (controllerBases.Count != 0)
                return controllerBases;
            
            var controller = GetComponent<HLODControllerBase>();
            if (controller != null)
            {
                controllerBases.Add(controller);
            }

            return controllerBases;
        }
#endif
    }
}