using Src.MetronomeLogic;
using UnityEngine;

namespace Src.SceneInitializers
{
    public class SampleSceneInitializer : MonoBehaviour
    {
        [SerializeField] private MetronomeConfig metronomeConfig;
        [SerializeField] private MetronomeView metronomeView;
        [SerializeField] private MetronomeBehaviour metronomeBehaviour;
        [SerializeField] private VerticalMovementController verticalMovementController;
        private Metronome _metronome;
        
        private void Start()
        {
            _metronome = new Metronome(metronomeConfig.BeatsIntervalsSeconds, metronomeConfig.HitInterval);
            metronomeView.Init(_metronome);
            metronomeBehaviour.Init(_metronome);
            verticalMovementController.Init(_metronome);
        }
    }
}