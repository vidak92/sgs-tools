using System.Collections.Generic;
using MijanTools.Components;
using UnityEngine;

namespace MijanTools.Util
{
    public class DebugDraw : MonoBehaviour
    {
        enum QuadShapeType
        {
            Circle,
            Rect,
            Line
        }
        
        private struct DrawLineData
        {
            public Vector3 Point1;
            public Vector3 Point2;
            public Color Color;
            public int SortOrder;

            public DrawLineData(Vector3 point1, Vector3 point2, Color color, int sortOrder)
            {
                Point1 = point1;
                Point2 = point2;
                Color = color;
                SortOrder = sortOrder;
            }
        }
        
        private struct DrawRectData
        {
            public Vector3 Center;
            public Vector2 Size;
            public Color Color;
            public int SortOrder;

            public DrawRectData(Vector3 center, Vector2 size, Color color, int sortOrder)
            {
                Center = center;
                Size = size;
                Color = color;
                SortOrder = sortOrder;
            }
        }

        private struct DrawCircleData
        {
            public Vector3 Center;
            public float Radius;
            public Color Color;
            public int SortOrder;

            public DrawCircleData(Vector3 center, float radius, Color color, int sortOrder)
            {
                Center = center;
                Radius = radius;
                Color = color;
                SortOrder = sortOrder;
            }
        }

        public static class Settings
        {
            public static float LineWidth = 0.1f;
            public static string SortLayerName = "Default";
            public static int StartingSortOrder = 1000;
            public static Color DefaultColor = Color.white;
        }

        private static DebugDraw _instance;

        public static bool IsEnabled = false;

        private const string _quadShapeShaderName = "Custom/QuadShape";
        private Mesh _quadMesh;
        private Material _quadShapeMaterial;
        private ObjectPool<QuadShape> _quadShapePool;
        private const int _initialCapacity = 100;
        
        private Transform _activeObjectsParent;

        private List<DrawLineData> _linesToDraw;
        private List<DrawCircleData> _circlesToDraw;
        private List<DrawRectData> _rectsToDraw;
        private int _totalDrawn;

        private void LateUpdate()
        {
            _quadShapePool.ReturnAllActiveObjects();

            // update visibility
            _activeObjectsParent.gameObject.SetActive(IsEnabled);

            // draw lines
            foreach (var drawLineData in _linesToDraw)
            {
                var p1 = drawLineData.Point1;
                var p2 = drawLineData.Point2;
                var center = (p1 + p2) / 2f;
                var direction = p2 - p1;
                var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                
                var quadShape = _quadShapePool.Get();
                quadShape.transform.parent = _activeObjectsParent;
                quadShape.gameObject.name = "QuadShape_Line";
                quadShape.transform.position = center;
                quadShape.transform.rotation = Quaternion.Euler(0f, 0f, angle);
                quadShape.transform.localScale = new Vector3(direction.magnitude, Settings.LineWidth, 1f);

                var mesh = quadShape.MeshFilter.mesh;
                var shapeDataUV = new Vector3((int)QuadShapeType.Line, 0f, 0f);
                mesh.SetUVs(1, new List<Vector3>
                {
                    shapeDataUV,
                    shapeDataUV,
                    shapeDataUV,
                    shapeDataUV,
                });

                var color = drawLineData.Color;
                var colorUV = new Vector4(color.r, color.g, color.b, color.a);
                mesh.SetUVs(2, new List<Vector4>
                {
                    colorUV,
                    colorUV,
                    colorUV,
                    colorUV,
                });

                var sortOrder = Settings.StartingSortOrder + drawLineData.SortOrder;
                quadShape.MeshRenderer.sortingLayerName = Settings.SortLayerName;
                quadShape.MeshRenderer.sortingOrder = sortOrder;
            }
            _linesToDraw.Clear();

            // draw circles
            foreach (var drawCircleData in _circlesToDraw)
            {
                var scale = drawCircleData.Radius * 2f + Settings.LineWidth;
                var normalizedLineWidth = Settings.LineWidth / scale;
                
                var quadShape = _quadShapePool.Get();
                quadShape.transform.parent = _activeObjectsParent;
                quadShape.gameObject.name = "QuadShape_Circle";
                quadShape.transform.position = drawCircleData.Center + Vector3.back;
                quadShape.transform.rotation = Quaternion.identity;
                quadShape.transform.localScale = Vector3.one * scale;

                var mesh = quadShape.MeshFilter.mesh;
                var shapeDataUV = new Vector3((int)QuadShapeType.Circle, normalizedLineWidth, 0f);
                mesh.SetUVs(1, new List<Vector3>
                {
                    shapeDataUV,
                    shapeDataUV,
                    shapeDataUV,
                    shapeDataUV,
                });
                
                var color = drawCircleData.Color;
                var colorUV = new Vector4(color.r, color.g, color.b, color.a);
                mesh.SetUVs(2, new List<Vector4>
                {
                    colorUV,
                    colorUV,
                    colorUV,
                    colorUV,
                });
                
                var sortOrder = Settings.StartingSortOrder + drawCircleData.SortOrder;
                quadShape.MeshRenderer.sortingLayerName = Settings.SortLayerName;
                quadShape.MeshRenderer.sortingOrder = sortOrder;
            }
            _circlesToDraw.Clear();
            
            // draw rects
            foreach (var drawRectData in _rectsToDraw)
            {
                var size = drawRectData.Size;
                var lineWidth = Settings.LineWidth;
                var normalizedLineWidth = new Vector2(lineWidth / size.x, lineWidth / size.y);

                var quadShape = _quadShapePool.Get();
                quadShape.transform.parent = _activeObjectsParent;
                quadShape.gameObject.name = "QuadShape_Rect";
                quadShape.transform.position = drawRectData.Center;
                quadShape.transform.rotation = Quaternion.identity;
                quadShape.transform.localScale = new Vector3(drawRectData.Size.x + lineWidth, drawRectData.Size.y, 1f);
                
                var mesh = quadShape.MeshFilter.mesh;
                var shapeDataUV = new Vector3((int)QuadShapeType.Rect, normalizedLineWidth.x, normalizedLineWidth.y);
                mesh.SetUVs(1, new List<Vector3>
                {
                    shapeDataUV,
                    shapeDataUV,
                    shapeDataUV,
                    shapeDataUV,
                });
                
                var color = drawRectData.Color;
                var colorUV = new Vector4(color.r, color.g, color.b, color.a);
                mesh.SetUVs(2, new List<Vector4>
                {
                    colorUV,
                    colorUV,
                    colorUV,
                    colorUV,
                });
                
                var sortOrder = Settings.StartingSortOrder + drawRectData.SortOrder;
                quadShape.MeshRenderer.sortingLayerName = Settings.SortLayerName;
                quadShape.MeshRenderer.sortingOrder = sortOrder;
                
            }
            _rectsToDraw.Clear();
        }

        private static void Init()
        {
            if (_instance == null)
            {
                _instance = new GameObject("DebugDraw").AddComponent<DebugDraw>();
                _instance.InitState();
                DontDestroyOnLoad(_instance.gameObject);
            }
        }

        private void InitState()
        {
            _linesToDraw = new List<DrawLineData>();
            _circlesToDraw = new List<DrawCircleData>();
            _rectsToDraw = new List<DrawRectData>();

            _quadMesh = new Mesh();
            _quadMesh.vertices = new Vector3[]
            {
                new Vector3(-0.5f, -0.5f, 0f),
                new Vector3(0.5f, -0.5f, 0f),
                new Vector3(-0.5f, 0.5f, 0f),
                new Vector3(0.5f, 0.5f, 0f),
            };
            _quadMesh.triangles = new int[]
            {
                2,
                1,
                0,
                3,
                1,
                2,
            };
            _quadMesh.uv = new Vector2[]
            {
                new Vector2(0f, 0f),
                new Vector2(1f, 0f),
                new Vector2(0f, 1f),
                new Vector2(1f, 1f),
            };
            _quadMesh.normals = new Vector3[]
            {
                new Vector3(0f, 0f, -1f),
                new Vector3(0f, 0f, -1f),
                new Vector3(0f, 0f, -1f),
                new Vector3(0f, 0f, -1f),
            };
            _quadMesh.tangents = new Vector4[]
            {
                new Vector4(1f, 0f, 0f, -1f),
                new Vector4(1f, 0f, 0f, -1f),
                new Vector4(1f, 0f, 0f, -1f),
                new Vector4(1f, 0f, 0f, -1f),
            };
            
            var shader = Shader.Find(_quadShapeShaderName);
            if (shader == null)
            {
                Debug.LogError($"{nameof(DebugDraw)}: Cannot find shader {_quadShapeShaderName}");
            }
            _quadShapeMaterial = new Material(shader);
            
            var quadShapeObject = new GameObject("QuadShapePrefab");
            quadShapeObject.SetActive(false);
            quadShapeObject.transform.parent = _instance.transform;
            
            var quadShape = quadShapeObject.AddComponent<QuadShape>();
            var meshFilter = quadShapeObject.AddComponent<MeshFilter>();
            meshFilter.sharedMesh = _quadMesh;
            quadShape.MeshFilter = meshFilter;
            
            var meshRenderer = quadShapeObject.AddComponent<MeshRenderer>();
            meshRenderer.sharedMaterial = _quadShapeMaterial;
            meshRenderer.sortingLayerName = Settings.SortLayerName;
            meshRenderer.sortingOrder = Settings.StartingSortOrder;
            quadShape.MeshRenderer = meshRenderer;
            
            _quadShapePool = ObjectPool<QuadShape>.CreateWithGameObject(quadShape, _initialCapacity, "DebugDraw_QuadShapePool");

            var activeObjectsParentObject = new GameObject("ActiveObjects");
            activeObjectsParentObject.transform.parent = _instance.transform;
            _activeObjectsParent = activeObjectsParentObject.transform;

        }

        public static void DrawLine(Vector3 p1, Vector3 p2)
        {
            DrawLine(p1, p2, Settings.DefaultColor);
        }
        
        public static void DrawLine(Vector3 p1, Vector3 p2, Color color)
        {
            Init();

            _instance._linesToDraw.Add(new DrawLineData(p1, p2, color, _instance._totalDrawn));
            _instance._totalDrawn++;
        }

        public static void DrawCircle(Vector3 center, float radius)
        {
            DrawCircle(center, radius, Settings.DefaultColor);
        }

        public static void DrawCircle(Vector3 center, float radius, Color color)
        {
            Init();

            _instance._circlesToDraw.Add(new DrawCircleData(center, radius, color, _instance._totalDrawn));
            _instance._totalDrawn++;
        }

        public static void DrawRect(Vector3 center, Vector2 size)
        {
            DrawRect(center, size, Settings.DefaultColor);
        }

        public static void DrawRect(Vector3 center, Vector2 size, Color color)
        {
            Init();

            _instance._rectsToDraw.Add(new DrawRectData(center, size, color, _instance._totalDrawn));
            _instance._totalDrawn++;
        }
    }
}