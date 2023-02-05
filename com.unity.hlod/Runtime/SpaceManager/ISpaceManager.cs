using UnityEngine;

namespace Unity.HLODSystem.SpaceManager
{
    public interface ISpaceManager
    {
        void UpdateCamera(Transform hlodTransform, Camera cam);

        bool IsHigh(float lodDistance, in Bounds bounds);

        bool IsCull(float cullDistance, in Bounds bounds);

        float GetDistanceSqure(in Bounds bounds);
    }
}