using UnityEngine;

namespace MijanTools.Data
{
    [System.Serializable]
    public class MinMaxFloat
    {
        [field: SerializeField] public float MinValue { get; private set; }
        [field: SerializeField] public float MaxValue { get; private set; }

        public float GetRandomValue()
        {
            return Random.Range(MinValue, MaxValue);
        }
    }
}