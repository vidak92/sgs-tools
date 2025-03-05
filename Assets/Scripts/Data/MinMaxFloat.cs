using UnityEngine;
#if UNITY_EDITOR
using MijanTools.Util;
using UnityEditor;
#endif

namespace MijanTools.Data
{
    [System.Serializable]
    public class MinMaxFloat
    {
        // TODO convert to regular field
        [field: SerializeField] public float MinValue { get; set; }
        [field: SerializeField] public float MaxValue { get; set; }

        public float GetRandomValue()
        {
            return Random.Range(MinValue, MaxValue);
        }

        public float GetValueAt(float t)
        {
            // TODO clamp t?
            return Mathf.Lerp(MinValue, MaxValue, t);
        }

        public float ClampValue(float value)
        {
            return Mathf.Clamp(value, MinValue, MaxValue);
        }
    }
    
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(MinMaxFloat))]
    public class MinMaxFloatDrawer : FieldPairDrawer
    {
        protected override string Field1Name { get; } = nameof(MinMaxFloat.MinValue);
        protected override string Field2Name { get; } = nameof(MinMaxFloat.MaxValue);
        
        protected override float FieldLabelWidth { get; } = 45f;
        protected override string Field1Label { get; } = "Min";
        protected override string Field2Label { get; } = "Max";
    }
#endif
}