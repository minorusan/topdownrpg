using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Core.Characters.Player
{
    [RequireComponent(typeof(HungerAffector))]
    [RequireComponent(typeof(Player))]
    public class StressAffector : MonoBehaviour
    {
        private Player _player;
        private HungerAffector _hungerAffector;

        private int kSpeedCoeficient = 700;

        public int Stress = 100;
        public float NervesTickTime = 2f;
        public Image NervesBar;

        private void Start()
        {
            Debug.Assert(NervesBar != null, "HungerAffector::HungerBar was not assigned.");
            _player = GetComponent<Player>();
            _hungerAffector = GetComponent<HungerAffector>();
            StartCoroutine(StressTick());
        }

        private IEnumerator StressTick()
        {
            while (true)
            {
                if (Stress < 100)
                {
                    Stress += 1;
                    float coef = (float)Stress / 100;
                    NervesBar.fillAmount = coef;

                    if (_hungerAffector.HungerTickTime > 0.1f)
                    {
                        _hungerAffector.HungerTickTime -= 0.1f;
                    }
                }
                yield return new WaitForSeconds(NervesTickTime);
            }
        }
    }
}

