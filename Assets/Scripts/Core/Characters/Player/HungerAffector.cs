using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Core.Characters.Player
{
    [RequireComponent(typeof(Player))]
    [RequireComponent(typeof(StressAffector))]
    public class HungerAffector : MonoBehaviour
    {
        private int kSpeedCoeficient = 700;

        private Player _player;
        private StressAffector _stressAffector;
  
        public int Hunger = 100;
        public float HungerTickTime = 2f;
        public Image HungerBar;

        private void Start()
        {
            Debug.Assert(HungerBar != null, "HungerAffector::HungerBar was not assigned.");
            _player = GetComponent<Player>();
            _stressAffector = GetComponent<StressAffector>();
            StartCoroutine(HungerTick());
        }

        private IEnumerator HungerTick()
        {
            while (true)
            {
                if (Hunger > 1)
                {
                    Hunger -= 1;
                    float coef = (float)Hunger / 100;
                    HungerBar.fillAmount = coef;
                    _player.MovementSpeed -= (float)Hunger / kSpeedCoeficient;

                    if (_stressAffector.NervesTickTime > 0.2f)
                    {
                        _stressAffector.NervesTickTime -= 0.1f;
                    }
                }
                yield return new WaitForSeconds(HungerTickTime);
            }
        }
    }
}

