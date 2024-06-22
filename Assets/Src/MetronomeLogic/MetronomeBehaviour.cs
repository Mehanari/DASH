using UnityEngine;

namespace Src.MetronomeLogic
{
    public class MetronomeBehaviour : MonoBehaviour
    {
        private Metronome _metronome;

        public void Init(Metronome metronome)
        {
            _metronome = metronome;
        }

        private void FixedUpdate()
        {
            _metronome.Update(Time.fixedDeltaTime);
        }
    }
}