using UnityEngine;

namespace Unity.HLODSystem.Streaming
{
    public partial class DefaultHLODController
    {
#if UNITY_EDITOR
        public override GameObject GetHighSceneObject(int id) => m_gameObjectList[id];
#endif
    }
}