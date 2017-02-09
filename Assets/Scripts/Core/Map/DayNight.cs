using System;
using UnityEngine;
using UnityEngine.UI;


namespace Core.Map
{
    public enum EDayTime
    {
        Morning,
        Day,
        Evening,
        Night,
    }

    [ExecuteInEditMode]
    public class DayNight : MonoBehaviour
    {
        public event Action<EDayTime> DayStateChanged;
        private SpriteRenderer[] _renderers;
        
        private float _time;

        public EDayTime State;
        

        [Header("Time periods")]
        public float DayLenght;
        public float MorningLength;
        public float EveningLength;
        public float NightLength;
        public float TimeRate = 0.001f;

        [Header("Symbols")]
        public Image SunImage;
        public Image MoonImage;
       

        // Use this for initialization
        void Start()
        {
            _renderers = FindObjectsOfType<SpriteRenderer>();
        }

        private void OnValidate()
        {
            if (DayStateChanged != null)
            {
                DayStateChanged(State);
            }
        }

        private void Update()
        {
            _time -= TimeRate;
            if (_time <= 0f)
            {
                ChangeDayState();
            }
        }

        public void ToggleLights(bool value)
        {
            if (!value)
            {
                State = EDayTime.Night;
            }
        }

        private void ChangeDayState()
        {
            switch (State)
            {
                case EDayTime.Morning:
                    {
                        _time = DayLenght;
                        State = EDayTime.Day;
                        break;
                   }
                case EDayTime.Day:
                    {
                        _time = EveningLength;
                        State = EDayTime.Evening;
                        break;
                    }
                case EDayTime.Evening:
                    {
                        _time = NightLength;
                        State = EDayTime.Night;
                        break;
                    }
                case EDayTime.Night:
                    {
                        _time = MorningLength;
                        State = EDayTime.Morning;
                        break;
                    }
            }
            if (DayStateChanged != null)
            {
                DayStateChanged(State);
            }
        }
    }
}

