using UnityEngine;
using System.Collections;
using Core.Characters.Player.Demand;


namespace Core.Inventory
{
	public enum HungerDecreasers
	{
		Meat,
		Banana
	}

	public enum StressDecreasers
	{
		Vodka,
		Heroin
	}

	public class HungerDecreaser : AConsumableBase
	{
		public HungerDecreaser (string itemId, EDemand affector, int effectValue) : base (itemId, affector, effectValue)
		{
		}
	}

	public class StressDecreaser : AConsumableBase
	{
		public StressDecreaser (string itemId, EDemand affector, int effectValue) : base (itemId, affector, effectValue)
		{
		}
	}
}

