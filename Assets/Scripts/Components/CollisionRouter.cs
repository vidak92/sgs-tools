using System;
using UnityEngine;

public class CollisionRouter : MonoBehaviour
{
    public Action<Collider2D> ActionOnTriggerEnter2D;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        ActionOnTriggerEnter2D?.Invoke(col);
    }
}
