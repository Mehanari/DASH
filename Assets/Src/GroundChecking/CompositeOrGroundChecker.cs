using System.Collections.Generic;
using UnityEngine;

namespace Src.GroundChecking
{
    public class CompositeOrGroundChecker : GroundChecker
    {
        [SerializeField] private List<GroundChecker> checkers;
        
        public override bool IsGrounded()
        {
            foreach (var groundChecker in checkers)
            {
                if (groundChecker.IsGrounded())
                {
                    return true;
                }
            }

            return false;
        }
    }
}