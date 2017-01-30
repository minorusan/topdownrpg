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

        public Material Sprites;
        public Light SunShine;

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
                      
                        if (_time >= 2.0f)
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
            var value = Mathf.Clamp(_time, 0.3f, 1f);
            Sprites.SetColor("_Color", new Color(value, value, value));
            SunShine.intensity = _time;
            Moon.color = new Color(1f, 1f, 1f, 1f - _time);
            Sun.color = new Color(1f, 1f, 1f, _time);
        }
    }
}

