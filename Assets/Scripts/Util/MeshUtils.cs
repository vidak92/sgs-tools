using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SGSTools.Util
{
    public static class MeshUtils
    {
        public const string QUAD_MESH_ASSET_NAME = "SGSQuad.asset";
        public const string QUAD_MESH_PACKAGE_FOLDER_PATH = "Packages/com.vidak-mijanovic.sgs-tools/Assets/Meshes";
        public const string QUAD_MESH_PROJECT_FOLDER_PATH = "Assets/Resources/SGS/Meshes";
        public static readonly string QUAD_MESH_PACKAGE_ASSET_PATH = $"{QUAD_MESH_PACKAGE_FOLDER_PATH}/{QUAD_MESH_ASSET_NAME}";
        public static readonly string QUAD_MESH_PROJECT_ASSET_PATH = $"{QUAD_MESH_PROJECT_FOLDER_PATH}/{QUAD_MESH_ASSET_NAME}";
        public static readonly string QUAD_MESH_RESOURCES_LOAD_PATH = $"SGS/Meshes/SGSQuad";

        private static Mesh _quadMesh;

        public static Mesh GetQuadMesh()
        {
            if (_quadMesh == null)
            {
                _quadMesh = Resources.Load<Mesh>(QUAD_MESH_RESOURCES_LOAD_PATH);
                if (_quadMesh == null)
                {
                    Debug.LogError($"SGSTools: Couldn't get quad mesh");
                }
            }
            return _quadMesh;
        }
        
#if UNITY_EDITOR
        [MenuItem("SGS Tools/Mesh Utils/Generate Quad Mesh")]
        public static bool CreateQuadMeshAsset()
        {
            var mesh = GenerateQuadMesh();
            
            var success = EditorUtils.CreateAssetIncludingFolderPath(mesh, QUAD_MESH_PACKAGE_FOLDER_PATH, QUAD_MESH_ASSET_NAME);
            if (!success)
            {
                Debug.LogError($"SGSTools: Couldn't create asset {QUAD_MESH_ASSET_NAME} in folder {QUAD_MESH_PACKAGE_FOLDER_PATH}");
                return false;
            }
            
            Debug.Log($"SGSTools: Saved quad mesh asset to {QUAD_MESH_PACKAGE_ASSET_PATH}");
            return true;
        }
        
        [MenuItem("SGS Tools/Mesh Utils/Generate Quad Mesh & Copy to Project")]
        public static bool CreateQuadMeshAssetAndCopyToProject()
        {
            var success = CreateQuadMeshAsset();
            if (!success)
            {
                return false;
            }
            
            success = EditorUtils.CreateAssetFolderPath(QUAD_MESH_PROJECT_FOLDER_PATH);
            if (!success)
            {
                Debug.LogError($"SGSTools: Couldn't create asset {QUAD_MESH_ASSET_NAME} in folder {QUAD_MESH_PROJECT_FOLDER_PATH}");
                return false;
            }

            success = AssetDatabase.CopyAsset(QUAD_MESH_PACKAGE_ASSET_PATH, QUAD_MESH_PROJECT_ASSET_PATH);
            if (!success)
            {
                Debug.LogError($"SGSTools: Failed to copy asset from {QUAD_MESH_PACKAGE_ASSET_PATH} to {QUAD_MESH_PROJECT_ASSET_PATH}");
                return false;
            }
            
            Debug.Log($"SGSTools: Saved quad mesh asset to {QUAD_MESH_PACKAGE_ASSET_PATH}, copied to {QUAD_MESH_PROJECT_ASSET_PATH}");
            return true;
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