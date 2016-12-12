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

		private void Start ()
		{
			_player = GetComponent<PlayerBehaviour> ();
			_stressAffector = GetComponent<StressAffector> ();
			StartCoroutine (HungerTick ());
		}

		private IEnumerator HungerTick ()
		{
			while (true)
			{
				if (DemandState > 1)
				{
					DemandState--;
				
					_player.MovementSpeed -= (float)DemandState / kSpeedCoeficient;

					if (_stressAffector.DemandTickTime > kMinimumTickSpeed)
					{
						_stressAffector.DemandTickTime -= kNervesDecreaseCoef;
					}
				}

				yield return new WaitForSeconds (DemandTickTime);
			}
		}


	}
}

