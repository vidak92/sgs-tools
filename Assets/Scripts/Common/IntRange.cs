using UnityEngine;
#if UNITY_EDITOR
using SGSTools.Util;
using UnityEditor;
#endif

namespace SGSTools.Common
{
    [System.Serializable]
    public class IntRange
    {
        public int Min;
        public int Max;

        public IntRange()
        {
            Min = 0;
            Max = 0;
        }

        public IntRange(int min, int max)
        {
            Min = min;
            Max = max;
        }

        public int GetRandomValue()
        {
            return Random.Range(Min, Max + 1); // max inclusive
        }
        
        public int GetValueAt(float t)
        {
            var floatValue = Mathf.Lerp(Min, Max, t);
            var value = Mathf.RoundToInt(floatValue);
            return GetClampedValue(value);
        }

        public int GetClampedValue(int value)
        {
            return Mathf.Clamp(value, Min, Max);
        }
        
        public float GetInverseValue(int value)
        {
            var t = Mathf.InverseLerp(Min, Max, value);
            t = Mathf.Clamp01(t);
            return t;
        }
    }
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(IntRange))]
    public class MinMaxIntDrawer : FieldPairDrawer
    {
        protected override string Field1Name { get; } = nameof(IntRange.Min);
        protected override string Field2Name { get; } = nameof(IntRange.Max);
        
        protected override float FieldLabelWidth { get; } = 45f;
        protected override string Field1Label { get; } = "Min";
        protected override string Field2Label { get; } = "Max";
    }
#endif
}