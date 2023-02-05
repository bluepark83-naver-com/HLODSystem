using System;
using System.Linq;
using Unity.HLODSystem.Streaming;
using Unity.HLODSystem.Utils;
using Unity.HLODSystem.SpaceManager;
using UnityEditor;
using UnityEngine;

namespace Unity.HLODSystem
{
    public partial class TerrainHLODEditor
    {
        static class Styles
        {
            public static GUIContent SourceText = new GUIContent("Source");
            public static GUIContent DestoryTerrainText = new GUIContent("Destroy terrain", "Destory original terrain when build time.");
            public static GUIContent GenerateButtonEnable = new GUIContent("Generate", "Generate a HLOD mesh.");
            public static GUIContent GenerateButtonExists = new GUIContent("Generate", "HLOD already generated.");
            public static GUIContent DestroyButtonEnable = new GUIContent("Destroy", "Destroy a HLOD mesh.");
            public static GUIContent DestroyButtonNotExists = new GUIContent("Destroy", "You need to generate HLOD before the destroy.");
            
            public static int[] TextureSizes = {
                4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048, 4096
            };
            public static string[] TextureSizeStrings;
            
            public static GUIStyle RedTextColor = new GUIStyle();

            static Styles()
            {
                TextureSizeStrings = new string[TextureSizes.Length];
                
                for (var i = 0; i < TextureSizes.Length; ++i)
                {
                    TextureSizeStrings[i] = TextureSizes[i].ToString();
                }
                
                RedTextColor.normal.textColor = Color.red;
            }
        }        
    }

}