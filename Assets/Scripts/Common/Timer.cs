using System;
using UnityEngine;

namespace SGSTools.Common
{
    [Serializable]
    public struct Timer
    {
        [field: SerializeField]
        public float Duration { get; private set; }
        public float Time { get; private set; }

        public bool IsDone => Time <= 0f;

        public void Init(float duration) // @TODO reset param?
        {
            Duration = duration;
            Time = 0f;
        }

        public void Reset(float duration = -1, bool additive = false)
        {
            if (duration > 0f)
            {
                Duration = duration;
            }
            
            if (additive)
            {
                Time += Duration;
            }
            else
            {
                Time = Duration;
            }
        }

        public bool Update(float dt)
        {
            if (IsDone)
            {
                return false;
            }
            Time -= dt;
            return IsDone;
        }
    }
}