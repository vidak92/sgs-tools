using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SGSTools.Util
{
    public static class MeshUtils
    {
        public const string ASSETS_FOLDER_PATH = "Packages/com.vidak-mijanovic.sgs-tools/Assets";
        public const string MESHES_SUBFOLDER_PATH = "/Meshes";
        public static readonly string QUAD_MESH_FOLDER_PATH = $"{ASSETS_FOLDER_PATH}{MESHES_SUBFOLDER_PATH}";
        public static readonly string QUAD_MESH_ASSET_PATH = $"{QUAD_MESH_FOLDER_PATH}/SGSQuad.asset";

        private static Mesh _quadMesh;

        public static Mesh GetQuadMesh()
        {
            if (_quadMesh == null)
            {
                _quadMesh = LoadQuadMeshAsset();
                if (_quadMesh == null)
                {
                    Debug.LogError($"SGSTools: Couldn't get quad mesh");
                }
            }
            return _quadMesh;
        }

        public static Mesh LoadQuadMeshAsset()
        {
            var mesh = AssetDatabase.LoadAssetAtPath<Mesh>(QUAD_MESH_ASSET_PATH);
            if (mesh == null)
            {
                Debug.LogError($"SGSTools: Couldn't load quad mesh asset from {QUAD_MESH_ASSET_PATH}");
            }
            return mesh;
        }

#if UNITY_EDITOR
        [MenuItem("SGS Tools/Mesh Utils/Generate Quad Mesh")]
        public static void GenerateAndSaveQuadMeshAsset()
        {
            var mesh = GenerateQuadMesh();
            
            if (!AssetDatabase.AssetPathExists(QUAD_MESH_FOLDER_PATH))
            {
                var guid = AssetDatabase.CreateFolder(ASSETS_FOLDER_PATH, MESHES_SUBFOLDER_PATH);
                if (string.IsNullOrEmpty(guid))
                {
                    Debug.LogError($"SGSTools: Couldn't create  new folder for quad mesh at {QUAD_MESH_FOLDER_PATH}");
                    return;
                }
                Debug.Log($"SGSTools: Created new folder for quad mesh at {QUAD_MESH_FOLDER_PATH}");
            }
            
            if (AssetDatabase.AssetPathExists(QUAD_MESH_ASSET_PATH))
            {
                Debug.LogWarning($"SGSTools: Overwriting existing quad mesh asset at {QUAD_MESH_ASSET_PATH}");
            }
            
            AssetDatabase.CreateAsset(mesh, QUAD_MESH_ASSET_PATH);
            // TODO check if asset was successfully created?
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log($"SGSTools: Saved quad mesh asset to {QUAD_MESH_ASSET_PATH}");
        }
#endif

        public static Mesh GenerateQuadMesh()
        {
            var quadMesh = new Mesh();
            quadMesh.vertices = new Vector3[]
            {
                new Vector3(-0.5f, -0.5f, 0f),
                new Vector3(0.5f, -0.5f, 0f),
                new Vector3(-0.5f, 0.5f, 0f),
                new Vector3(0.5f, 0.5f, 0f),
            };
            quadMesh.triangles = new int[]
            {
                2,
                1,
                0,
                3,
                1,
                2,
            };
            quadMesh.uv = new Vector2[]
            {
                new Vector2(0f, 0f),
                new Vector2(1f, 0f),
                new Vector2(0f, 1f),
                new Vector2(1f, 1f),
            };
            quadMesh.normals = new Vector3[]
            {
                new Vector3(0f, 0f, -1f),
                new Vector3(0f, 0f, -1f),
                new Vector3(0f, 0f, -1f),
                new Vector3(0f, 0f, -1f),
            };
            quadMesh.tangents = new Vector4[]
            {
                new Vector4(1f, 0f, 0f, -1f),
                new Vector4(1f, 0f, 0f, -1f),
                new Vector4(1f, 0f, 0f, -1f),
                new Vector4(1f, 0f, 0f, -1f),
            };
            return quadMesh;
        }

        public static void SetQuadMeshUVs(this Mesh quadMesh, int channel, Vector2 uv)
        {
            quadMesh.SetUVs(channel, new List<Vector2> { uv, uv, uv, uv });
        }
        
        public static void SetQuadMeshUVs(this Mesh quadMesh, int channel, Vector3 uv)
        {
            quadMesh.SetUVs(channel, new List<Vector3> { uv, uv, uv, uv });
        }
        
        public static void SetQuadMeshUVs(this Mesh quadMesh, int channel, Vector4 uv)
        {
            quadMesh.SetUVs(channel, new List<Vector4> { uv, uv, uv, uv });
        }
    }
}