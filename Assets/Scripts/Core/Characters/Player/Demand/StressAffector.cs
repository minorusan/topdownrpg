using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace Core.Characters.Player.Demand
{
	[RequireComponent (typeof(HungerAffector))]
	[RequireComponent (typeof(PlayerBehaviour))]
	public class StressAffector : MonoBehaviour
	{
		#region PRIVATE

		private PlayerBehaviour _player;
		private HungerAffector _hungerAffector;

		private int kSpeedCoeficient = 700;
		private float kMinimumTickSpeed = 0.2f;
		private float kDecreaseCoef = 0.1f;

		#endregion

		public int Stress = 100;
		public float NervesTickTime = 2f;
		public Image NervesBar;

		private void Start ()
		{
			Debug.Assert (NervesBar != null, "HungerAffector::HungerBar was not assigned.");
			_player = GetComponent<PlayerBehaviour> ();
			_hungerAffector = GetComponent<HungerAffector> ();
			StartCoroutine (StressTick ());
		}

		private IEnumerator StressTick ()
		{
			while (true)
			{
				if (Stress < 100)
				{
					Stress += 1;
					float coef = (float)Stress / 100;
					NervesBar.fillAmount = coef;

					if (_hungerAffector.HungerTickTime > kMinimumTickSpeed)
					{
						_hungerAffector.HungerTickTime -= kDecreaseCoef;
					}
				}
				yield return new WaitForSeconds (NervesTickTime);
			}
		}
	}
}

