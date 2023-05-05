using UnityEngine;

namespace MijanTools.Data
{
    [System.Serializable]
    public class MinMaxInt
    {
        [field: SerializeField] public int MinValue { get; private set; }
        [field: SerializeField] public int MaxValue { get; private set; }

        public float GetRandomValue()
        {
            return Random.Range(MinValue, MaxValue);
        }
    }
}