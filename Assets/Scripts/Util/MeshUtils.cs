using System.Collections.Generic;
using UnityEngine;

namespace SGSTools.Util
{
    public static class MeshUtils
    {
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