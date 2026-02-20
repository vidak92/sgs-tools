using UnityEngine;
#if UNITY_EDITOR
using SGSTools.Util;
using UnityEditor;
#endif

namespace SGSTools.Common
{
    [System.Serializable]
    public class FloatRange
    {
        public float Min;
        public float Max;

        public FloatRange()
        {
            Min = 0f;
            Max = 0f;
        }
        
        public FloatRange(float min, float max)
        {
            Min = min;
            Max = max;
        }

        public float GetRandomValue()
        {
            return Random.Range(Min, Max);
        }

        public float GetValueAt(float t)
        {
            var value = Mathf.Lerp(Min, Max, t);
            return GetClampedValue(value);
        }

        public float GetClampedValue(float value)
        {
            return Mathf.Clamp(value, Min, Max);
        }
        
        public float GetInverseValue(float value)
        {
            var t = Mathf.InverseLerp(Min, Max, value);
            t = Mathf.Clamp01(t);
            return t;
        }
    }
    
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(FloatRange))]
    public class MinMaxFloatDrawer : FieldPairDrawer
    {
        protected override string Field1Name { get; } = nameof(FloatRange.Min);
        protected override string Field2Name { get; } = nameof(FloatRange.Max);
        
        protected override float FieldLabelWidth { get; } = 45f;
        protected override string Field1Label { get; } = "Min";
        protected override string Field2Label { get; } = "Max";
    }
#endif
}