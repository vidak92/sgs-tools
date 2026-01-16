using SGSTools.Util;
using SGSTools.Common;
using UnityEngine;
using UnityEngine.Rendering;

namespace CyberPuck
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class QuadMeshRenderer : MonoBehaviour
    {
        [SerializeField] private Material _material;
        [SerializeField] [SortingLayer] private int _sortingLayerID;
        [SerializeField] private int _sortingOrder;

        public Material Material
        {
            get => _material;
            set
            {
                _material = value;
                MeshRenderer.material = _material;
            }
        }

        public int SortingLayerID
        {
            get => _sortingLayerID;
            set
            {
                _sortingLayerID = value;
                MeshRenderer.sortingLayerID = value;
            }
        }
        public int SortingOrder
        {
            get => _sortingOrder;
            set
            {
                _sortingOrder = value;
                MeshRenderer.sortingOrder = value;
            }
        }

        public MeshFilter MeshFilter { get; private set; }
        public MeshRenderer MeshRenderer { get; private set; }

        public float Width => transform.localScale.x;
        public float Height => transform.localScale.y;
        
        private void Awake()
        {
            UpdateMeshComponents();
        }
        
        private void Reset()
        {
            UpdateMeshComponents();
        }

        private void OnValidate()
        {
            UpdateMeshComponents();
        }
        
        public void UpdateMeshComponents()
        {
            if (MeshFilter == null)
            {
                MeshFilter = GetComponent<MeshFilter>();
            }
            if (MeshRenderer == null)
            {
                MeshRenderer = GetComponent<MeshRenderer>();
            }
            if (MeshFilter.sharedMesh == null)
            {
                MeshFilter.sharedMesh = MeshUtils.GetQuadMesh();
            }

            MeshRenderer.material = Material;
            MeshRenderer.sortingLayerID = SortingLayerID;
            MeshRenderer.sortingOrder = SortingOrder;

            MeshRenderer.shadowCastingMode = ShadowCastingMode.Off;
            MeshRenderer.receiveShadows = false;
            MeshRenderer.lightProbeUsage = LightProbeUsage.Off;
            MeshRenderer.reflectionProbeUsage = ReflectionProbeUsage.Off;
        }

        public void SetLineVisualizerData(Vector3 point1, Vector3 point2, float radius)
        {
            var midpoint = (point1 + point2) / 2f;
            var distance = Vector3.Distance(point1, point2);
            var direction = point2 - point1;
            var angle = MathfExt.VectorXYToAngle(direction) * Mathf.Rad2Deg;
            var height = radius * 2f;
            var width = distance + height;

            transform.position = midpoint;
            transform.SetLocalScaleXY(width, height);
            transform.SetRotationZ(angle);
        }
    }
}