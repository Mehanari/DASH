using System.Collections.Generic;
using UnityEngine;

namespace Src.MetronomeLogic
{
    [CreateAssetMenu(fileName = "MetronomeConfig", menuName = "Configs/MetronomeConfig", order = 0)]
    public class MetronomeConfig : ScriptableObject
    {
        [SerializeField] private List<float> beatsIntervalsSeconds;
        [SerializeField] private float hitInterval;
        public List<float> BeatsIntervalsSeconds => beatsIntervalsSeconds;
        public float HitInterval => hitInterval;
    }
}