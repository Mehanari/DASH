using UnityEngine;

namespace Src.GroundChecking
{
    public class CircleGroundChecker : GroundChecker
    {
        [SerializeField] private float radius;
        [SerializeField] private LayerMask groundLayer;
        private RaycastHit2D[] _hits = new RaycastHit2D[1];
    
        public override bool IsGrounded()
        {
            var count = Physics2D.CircleCastNonAlloc(transform.position, radius, Vector2.one, _hits, 1000f, groundLayer);
            return count > 0;
        }

        private void OnDrawGizmosSelected()
        {
            if (IsGrounded())
            {
                Gizmos.color = Color.green;
            }
            else
            {
                Gizmos.color = Color.red;
            }
            Gizmos.DrawSphere(transform.position, radius);
        }
    }
}
