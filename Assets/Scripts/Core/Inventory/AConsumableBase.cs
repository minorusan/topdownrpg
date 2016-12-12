using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Core.Characters.Player.Demand;


namespace Core.Inventory
{
	[Serializable]
	public class AConsumableBase : AItemBase
	{
		public enum EDemand
		{
			Hunger,
			Stress
		}

		private EDemand _selectedDemand;
		private DemandAffector _affector;

		public AConsumableBase (string itemId, EDemand affector, int effectValue) : base (itemId)
		{
			_selectedDemand = affector;
			SetAction (IncrementAffectorByValue (effectValue));
		}

		private Action IncrementAffectorByValue (int value)
		{
			return () =>
			{
				if (_affector == null)
				{
					InitAffector ();
				}
				_affector.DemandState += value;
				_affector.DemandTickTime = DemandAffector.DefaultTickTime;
			};
		}

		private void InitAffector ()
		{
			switch (_selectedDemand)
			{
			case EDemand.Hunger:
				{
					_affector = GameObject.FindObjectOfType <HungerAffector> ();
					break;
				}
			case EDemand.Stress:
				{
					_affector = GameObject.FindObjectOfType <StressAffector> ();
					break;
				}
			}
		}
	}
}

