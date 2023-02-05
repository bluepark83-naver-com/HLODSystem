using UnityEngine;

namespace Unity.HLODSystem.SpaceManager
{
    public class QuadTreeSpaceManager : ISpaceManager
    {
        float _preRelative;
        Vector3 _camPosition;

        public void UpdateCamera(Transform hlodTransform, Camera cam)
        {
            if (cam.orthographic)
            {
                _preRelative = 0.5f / cam.orthographicSize;
            }
            else
            {
                var halfAngle = Mathf.Tan(Mathf.Deg2Rad * cam.fieldOfView * 0.5F);
                _preRelative = 0.5f / halfAngle;
            }

            _preRelative = _preRelative * QualitySettings.lodBias;
            _camPosition = hlodTransform.worldToLocalMatrix.MultiplyPoint(cam.transform.position);
        }

        public bool IsHigh(float lodDistance, in Bounds bounds)
        {
            //float distance = 1.0f;
            //if (cam.orthographic == false)

            var distance = GetDistance(bounds.center, _camPosition);
            var relativeHeight = bounds.size.x * _preRelative / distance;
            return relativeHeight > lodDistance;
        }

        public float GetDistanceSqure(in Bounds bounds)
        {
            var x = bounds.center.x - _camPosition.x;
            var z = bounds.center.z - _camPosition.z;

            var square = x * x + z * z;
            return square;
        }

        public bool IsCull(float cullDistance, in Bounds bounds)
        {
            var distance = GetDistance(bounds.center, _camPosition);

            var relativeHeight = bounds.size.x * _preRelative / distance;
            return relativeHeight < cullDistance;
        }

        float GetDistance(in Vector3 boundsPos, in Vector3 camPos)
        {
            var x = boundsPos.x - camPos.x;
            var z = boundsPos.z - camPos.z;
            var square = x * x + z * z;
            return Mathf.Sqrt(square);
        }
    }
}