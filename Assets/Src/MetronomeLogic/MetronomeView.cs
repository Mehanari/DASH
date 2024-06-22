using System.Collections.Generic;
using UnityEngine;

namespace Src.MetronomeLogic
{
    public class MetronomeView : MonoBehaviour
    {
        [SerializeField] private GameObject beatPrefab;
        [SerializeField] private RectTransform start;
        [SerializeField] private RectTransform end;
        [SerializeField] private float beatsSpeed;
        [SerializeField] private float interval = 5f;
        [SerializeField] private RectTransform hitZoneIndicator;
        private List<GameObject> _beats = new List<GameObject>();
        private Metronome _metronome;

        public void Init(Metronome metronome)
        {
            _metronome = metronome;
            var hitZoneIndicatorGameObject = hitZoneIndicator.gameObject;
            var hitZoneIndicatorTransform = hitZoneIndicatorGameObject.transform.localScale;
            hitZoneIndicatorGameObject.transform.localScale = new Vector3(hitZoneIndicatorTransform.x,
                hitZoneIndicatorTransform.y * beatsSpeed * _metronome.HitInterval, hitZoneIndicatorTransform.z);
        }
        

        private void Update()
        {
            foreach (var beat in _beats)
            {
                Destroy(beat);
            }
            var moments = _metronome.GetBeatsMomentsForNextInterval(interval);
            var endPos = end.position;
            foreach (var moment in moments)
            {
                var yPos = endPos.y + moment * beatsSpeed;
                var xPos = start.position.x;
                var beat = Instantiate(beatPrefab, new Vector2(xPos, yPos), Quaternion.identity, transform);
                _beats.Add(beat);
            }
        }
    }
}
