using UnityEngine;

namespace Src.GroundChecking
{
    public abstract class GroundChecker : MonoBehaviour
    {
        public abstract bool IsGrounded();
    }
}