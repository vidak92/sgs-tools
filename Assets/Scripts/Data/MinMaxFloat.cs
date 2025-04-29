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
        public float Min;
        public float Max;

        public MinMaxFloat()
        {
            Min = 0f;
            Max = 0f;
        }
        
        public MinMaxFloat(float min, float max)
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
            // TODO clamp t?
            return Mathf.Lerp(Min, Max, t);
        }

        public float GetClampedValue(float value)
        {
            return Mathf.Clamp(value, Min, Max);
        }
    }
    
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(MinMaxFloat))]
    public class MinMaxFloatDrawer : FieldPairDrawer
    {
        protected override string Field1Name { get; } = nameof(MinMaxFloat.Min);
        protected override string Field2Name { get; } = nameof(MinMaxFloat.Max);
        
        protected override float FieldLabelWidth { get; } = 45f;
        protected override string Field1Label { get; } = "Min";
        protected override string Field2Label { get; } = "Max";
    }
#endif
}