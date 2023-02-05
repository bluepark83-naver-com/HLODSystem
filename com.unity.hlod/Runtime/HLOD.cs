using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Unity.HLODSystem
{
    public partial class HLOD : MonoBehaviour, ISerializationCallbackReceiver, IGeneratedResourceManager
    {
        public const string HLODLayerStr = "HLOD";

        [SerializeField] float m_ChunkSize = 30.0f;
        [SerializeField] float m_LODDistance = 0.3f;
        [SerializeField] float m_CullDistance = 0.01f;
        [SerializeField] float m_MinObjectSize = 0.0f;


        [SerializeField] string m_SpaceSplitterTypeStr;
        [SerializeField] string m_BatcherTypeStr;        //< unity serializer is not support serialization with System.Type
                                                //< So, we should convert to string to store value.
        [SerializeField] string m_SimplifierTypeStr;
        [SerializeField] string m_StreamingTypeStr;
        [SerializeField] string m_UserDataSerializerTypeStr;

        [SerializeField] SerializableDynamicObject m_SpaceSplitterOptions = new SerializableDynamicObject();
        [SerializeField] SerializableDynamicObject m_SimplifierOptions = new SerializableDynamicObject();
        [SerializeField] SerializableDynamicObject m_BatcherOptions = new SerializableDynamicObject();
        [SerializeField] SerializableDynamicObject m_StreamingOptions = new SerializableDynamicObject();
        
        [SerializeField] List<Object> m_generatedObjects = new List<Object>();
        [SerializeField] List<GameObject> m_convertedPrefabObjects = new List<GameObject>();


        public float ChunkSize => m_ChunkSize;
        public float LODDistance => m_LODDistance;
        public float CullDistance => m_CullDistance;

        public Type SpaceSplitterType { set; get; }
        public Type BatcherType { set; get; }
        public Type SimplifierType { set; get; }
        public Type StreamingType { set; get; }
        public Type UserDataSerializerType { set; get; }

        public SerializableDynamicObject SpaceSplitterOptions => m_SpaceSplitterOptions;
        public SerializableDynamicObject BatcherOptions => m_BatcherOptions;
        public SerializableDynamicObject StreamingOptions => m_StreamingOptions;
        public SerializableDynamicObject SimplifierOptions => m_SimplifierOptions;

        public float MinObjectSize => m_MinObjectSize;

        public Bounds GetBounds()
        {
            var ret = new Bounds();
            var renderers = GetComponentsInChildren<Renderer>();
            if (renderers.Length == 0)
            {
                ret.center = Vector3.zero;
                ret.size = Vector3.zero;
                return ret;
            }

            var bounds = Utils.BoundsUtils.CalcLocalBounds(renderers[0], transform);
            for (var i = 1; i < renderers.Length; ++i)
            {
                bounds.Encapsulate(Utils.BoundsUtils.CalcLocalBounds(renderers[i], transform));
            }

            ret.center = bounds.center;
            var max = Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z);
            ret.size = new Vector3(max, max, max);  

            return ret;
        }

    

        public void OnBeforeSerialize()
        {
            if (SpaceSplitterType != null)
                m_SpaceSplitterTypeStr = SpaceSplitterType.AssemblyQualifiedName;
            if ( BatcherType != null )
                m_BatcherTypeStr = BatcherType.AssemblyQualifiedName;
            if (SimplifierType != null)
                m_SimplifierTypeStr = SimplifierType.AssemblyQualifiedName;
            if (StreamingType != null)
                m_StreamingTypeStr = StreamingType.AssemblyQualifiedName;
            if (UserDataSerializerType != null)
                m_UserDataSerializerTypeStr = UserDataSerializerType.AssemblyQualifiedName;
        }

        public void OnAfterDeserialize()
        {
            if (string.IsNullOrEmpty(m_SpaceSplitterTypeStr))
            {
                SpaceSplitterType = null;
            }
            else
            {
                SpaceSplitterType = Type.GetType(m_SpaceSplitterTypeStr);
            }
            
            if (string.IsNullOrEmpty(m_BatcherTypeStr))
            {
                BatcherType = null;
            }
            else
            {
                BatcherType = Type.GetType(m_BatcherTypeStr);
            }

            if (string.IsNullOrEmpty(m_SimplifierTypeStr))
            {
                SimplifierType = null;
            }
            else
            {
                SimplifierType = Type.GetType(m_SimplifierTypeStr);
            }

            if (string.IsNullOrEmpty(m_StreamingTypeStr))
            {
                StreamingType = null;
            }
            else
            {
                StreamingType = Type.GetType(m_StreamingTypeStr);
            }

            if (string.IsNullOrEmpty(m_UserDataSerializerTypeStr))
            {
                UserDataSerializerType = null;
            }
            else
            {
                UserDataSerializerType = Type.GetType(m_UserDataSerializerTypeStr);
            }
            
        }

        public void AddGeneratedResource(Object obj)
        {
            m_generatedObjects.Add(obj);
        }

        public bool IsGeneratedResource(Object obj)
        {
            return m_generatedObjects.Contains(obj);
        }

        public void AddConvertedPrefabResource(GameObject obj)
        {
            m_convertedPrefabObjects.Add(obj);
        }
    }

}