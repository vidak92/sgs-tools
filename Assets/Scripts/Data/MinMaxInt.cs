using UnityEngine;
#if UNITY_EDITOR
using MijanTools.Util;
using UnityEditor;
#endif

namespace MijanTools.Data
{
    [System.Serializable]
    public class MinMaxInt
    {
        public int Min;
        public int Max;

        public float GetRandomValue()
        {
            return Random.Range(Min, Max);
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