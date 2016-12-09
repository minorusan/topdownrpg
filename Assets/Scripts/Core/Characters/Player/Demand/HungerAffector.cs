using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace Core.Characters.Player.Demand
{
	[RequireComponent (typeof(PlayerBehaviour))]
	[RequireComponent (typeof(StressAffector))]
	public class HungerAffector : DemandAffector
	{
		#region PRIVATE

		private int kSpeedCoeficient = 700;
		private float kMinimumTickSpeed = 0.2f;
		private float kNervesDecreaseCoef = 0.1f;

		private PlayerBehaviour _player;
		private StressAffector _stressAffector;

		#endregion

		public int Hunger = 100;
		public float HungerTickTime = 2f;
		public Image HungerBar;

		private void Start ()
		{
			Debug.Assert (HungerBar != null, "HungerAffector::HungerBar was not assigned.");
			_player = GetComponent<PlayerBehaviour> ();
			_stressAffector = GetComponent<StressAffector> ();
			StartCoroutine (HungerTick ());
		}

		private IEnumerator HungerTick ()
		{
			while (true)
			{
				if (Hunger > 1)
				{
					Hunger--;
					UpdateBar ();
					_player.MovementSpeed -= (float)Hunger / kSpeedCoeficient;

					if (_stressAffector.NervesTickTime > kMinimumTickSpeed)
					{
						_stressAffector.NervesTickTime -= kNervesDecreaseCoef;
					}
				}

				yield return new WaitForSeconds (HungerTickTime);
			}
		}


	}
}

