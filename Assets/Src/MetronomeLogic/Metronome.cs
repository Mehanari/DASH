using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Src.MetronomeLogic
{
    public class Metronome
    {
        private List<float> _beatsIntervalsSeconds;
        private readonly float _hitInterval;
        private float _oneCycleTime;
        private float _elapsedCycleTime;
        private float _hitBlockingTime;
        
        public float HitInterval => _hitInterval;
        public event Action<float> Hit; 
    
        public Metronome(List<float> beatsIntervalsSeconds, float hitInterval)
        {
            _beatsIntervalsSeconds = beatsIntervalsSeconds;
            _oneCycleTime = _beatsIntervalsSeconds.Sum();
            _hitInterval = hitInterval;
        }

        public void Update(float deltaTime)
        {
            _hitBlockingTime -= deltaTime;
            if (_hitBlockingTime <= 0f)
            {
                _hitBlockingTime = 0f;
            }
            _elapsedCycleTime += deltaTime;
            if (_elapsedCycleTime >= _oneCycleTime)
            {
                _elapsedCycleTime -= _oneCycleTime;
            }
        }
    
        public List<float> GetBeatsMomentsForNextInterval(float seconds)
        {
            float time = seconds + _elapsedCycleTime;
            List<float> beatsMoments = new List<float>();
            var populating = true;
            while (populating)
            {
                foreach (var interval in _beatsIntervalsSeconds)
                {
                    if (time >= interval)
                    {                    
                        time -= interval;
                        var moment = seconds - time;
                        if (moment >= _hitBlockingTime)
                        {
                            beatsMoments.Add(moment);
                        }
                    }
                    else
                    {
                        populating = false;
                    }
                }
            }
        
            return beatsMoments;
        }

        //Returns value from 0 to 1,
        //where 0 means that beat was not in hit interval,
        //1 means that hit was exactly on beat.
        public float TryHit()
        {
            if (_hitBlockingTime > 0)
            {
                return 0;
            }
            var deltas = GetCurrentDeltas();
            var min = deltas.Min();
            var result = 0f;
            if (min < _hitInterval)
            {
                result = (_hitInterval - min) / _hitInterval;
                _hitBlockingTime = _hitInterval;
            }
            Hit?.Invoke(result);
            return result;
        }

        public List<float> GetCurrentDeltas()
        {
            var deltas = new List<float>();
            var moment = 0f;
            foreach (var interval in _beatsIntervalsSeconds)
            {
                moment += interval;
                var delta = moment - _elapsedCycleTime;
                if (delta >= 0)
                {
                    deltas.Add(delta);
                }
            }
            return deltas;
        }
    }
}