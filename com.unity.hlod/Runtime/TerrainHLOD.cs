using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Unity.HLODSystem
{
    public class TerrainHLOD : MonoBehaviour, ISerializationCallbackReceiver, IGeneratedResourceManager
    {
        [SerializeField] string m_SimplifierTypeStr = "";
        [SerializeField] private string m_StreamingTypeStr = "";

        [SerializeField] private TerrainData m_TerrainData;
        [SerializeField] private bool m_DestroyTerrain = true;
        [SerializeField] private float m_ChunkSize = 30.0f;
        [SerializeField] private int m_BorderVertexCount = 256;
        [SerializeField] private float m_LODDistance = 0.3f;
        [SerializeField] private float m_CullDistance = 0.01f;
        [SerializeField] private SerializableDynamicObject m_SimplifierOptions = new SerializableDynamicObject();
        [SerializeField] private SerializableDynamicObject m_StreamingOptions = new SerializableDynamicObject();

        [SerializeField] private string m_materialGUID = "";
        [SerializeField] private string m_materialLowGUID = "";
        [SerializeField] private int m_textureSize = 64;

        [SerializeField] private bool m_useNormal = false;
        [SerializeField] private bool m_useMask = false;

        [SerializeField] private string m_albedoPropertyName = "";
        [SerializeField] private string m_normalPropertyName = "";
        [SerializeField] private string m_maskPropertyName = "";
        
        [SerializeField]
        private List<Object> m_generatedObjects = new List<Object>();
        [SerializeField]
        private List<GameObject> m_convertedPrefabObjects = new List<GameObject>();
        
        public Type SimplifierType { set; get; }

        public Type StreamingType { set; get; }

        public TerrainData TerrainData
        {
            set => m_TerrainData = value;
            get => m_TerrainData;
        }
        public bool DestroyTerrain => m_DestroyTerrain;

        public float ChunkSize
        {
            get => m_ChunkSize;
            set => m_ChunkSize = value;
        }

        public int BorderVertexCount
        {
            get => m_BorderVertexCount;
            set => m_BorderVertexCount = value;
        }

        public float LODDistance
        {
            get => m_LODDistance;
            set => m_LODDistance = value;
        }

        public float CullDistance => m_CullDistance;

        public SerializableDynamicObject SimplifierOptions => m_SimplifierOptions;

        public SerializableDynamicObject StreamingOptions => m_StreamingOptions;

        public int TextureSize
        {
            set => m_textureSize = value;
            get => m_textureSize;
        }

        public string MaterialGUID
        {
            set => m_materialGUID = value;
            get => m_materialGUID;
        }

        public string MaterialLowGUID
        {
            set => m_materialLowGUID = value;
            get => m_materialLowGUID;
        }

        public bool UseNormal
        {
            set => m_useNormal = value;
            get => m_useNormal;
        }

        public bool UseMask
        {
            set => m_useMask = value;
            get => m_useMask;
        }

        public string AlbedoPropertyName
        {
            set => m_albedoPropertyName = value;
            get => m_albedoPropertyName;
        }

        public string NormalPropertyName
        {
            set => m_normalPropertyName = value;
            get => m_normalPropertyName;
        }

        public string MaskPropertyName
        {
            set => m_maskPropertyName = value;
            get => m_maskPropertyName;
        }
        
        public List<Object> GeneratedObjects => m_generatedObjects;

        public List<GameObject> ConvertedPrefabObjects => m_convertedPrefabObjects;

        public void OnBeforeSerialize()
        {
            if (SimplifierType != null)
                m_SimplifierTypeStr = SimplifierType.AssemblyQualifiedName;
            if (StreamingType != null)
                m_StreamingTypeStr = StreamingType.AssemblyQualifiedName;
        }

        public void OnAfterDeserialize()
        {
            SimplifierType = string.IsNullOrEmpty(m_SimplifierTypeStr) ? null : Type.GetType(m_SimplifierTypeStr);

            StreamingType = string.IsNullOrEmpty(m_StreamingTypeStr) ? null : Type.GetType(m_StreamingTypeStr);
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
        
        public Bounds GetBounds()
        {
            return m_TerrainData == null ? new Bounds() : new Bounds(m_TerrainData.size * 0.5f, m_TerrainData.size);
        }

    }
}