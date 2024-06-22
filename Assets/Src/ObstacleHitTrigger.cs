using System;
using UnityEngine;

public class ObstacleHitTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask obstacleLayer;

    public event Action ObstacleHit;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision enter");
        if ((1<<other.gameObject.layer) == obstacleLayer)
        {
            Debug.Log("Layer is obstacle");
            ObstacleHit?.Invoke();
        }
    }
}
