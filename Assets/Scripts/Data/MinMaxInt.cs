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
        // TODO convert to regular field
        [field: SerializeField] public int MinValue { get; private set; }
        [field: SerializeField] public int MaxValue { get; private set; }

        public float GetRandomValue()
        {
            return Random.Range(MinValue, MaxValue);
        }
    }
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(MinMaxInt))]
    public class MinMaxIntDrawer : FieldPairDrawer
    {
        protected override string Field1Name { get; } = nameof(MinMaxInt.MinValue);
        protected override string Field2Name { get; } = nameof(MinMaxInt.MaxValue);
        
        protected override float FieldLabelWidth { get; } = 45f;
        protected override string Field1Label { get; } = "Min";
        protected override string Field2Label { get; } = "Max";
    }
#endif
}