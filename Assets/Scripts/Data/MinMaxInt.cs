using UnityEngine;
#if UNITY_EDITOR
using SGSTools.Util;
using UnityEditor;
#endif

namespace SGSTools.Data
{
    [System.Serializable]
    public class MinMaxInt
    {
        public int Min;
        public int Max;

        public int GetRandomValue()
        {
            return Random.Range(Min, Max + 1); // max inclusive
        }
        
        public float GetValueAt(float t)
        {
            var value = Mathf.Lerp(Min, Max, t);
            value = GetClampedValue(value);
            return value;
        }

        public float GetClampedValue(float value)
        {
            return Mathf.Clamp(value, Min, Max);
        }
    }
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(MinMaxInt))]
    public class MinMaxIntDrawer : FieldPairDrawer
    {
        protected override string Field1Name { get; } = nameof(MinMaxInt.Min);
        protected override string Field2Name { get; } = nameof(MinMaxInt.Max);
        
        protected override float FieldLabelWidth { get; } = 45f;
        protected override string Field1Label { get; } = "Min";
        protected override string Field2Label { get; } = "Max";
    }
#endif
}