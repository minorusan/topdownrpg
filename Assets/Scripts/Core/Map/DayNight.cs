using UnityEngine;
using UnityEngine.UI;

namespace Core.Map
{
    public enum EDayTime
    {
        Day,
        Night
    }

    public class DayNight : MonoBehaviour
    {
        private SpriteRenderer[] _renderers;
        private float _time = 1;

        public float TimeRate;
        public EDayTime State = EDayTime.Night;

        public Image Sun;
        public Image Moon;

        // Use this for initialization
        void Start()
        {
            _renderers = FindObjectsOfType<SpriteRenderer>();
        }

        void Update()
        {
            switch (State)
            {
                case EDayTime.Day:
                    {
                        _time += TimeRate;
                      
                        if (_time >= 1.0f)
                        {
                            State = EDayTime.Night;
                        }
                        break;
                    }
                case EDayTime.Night:
                    {
                        _time -= TimeRate;

                        if (_time <= 0.0f)
                        {
                            State = EDayTime.Day;
                        }
                        break;
                    }
            }

            for (int i = 0; i < _renderers.Length; i++)
            {
                var value = Mathf.Clamp(_time, 0.5f, 1f);
                _renderers[i].color = new Color(value, value, value);
            }

            Moon.color = new Color(1f, 1f, 1f, 1f - _time);
            Sun.color = new Color(1f, 1f, 1f, _time);
        }
    }
}

