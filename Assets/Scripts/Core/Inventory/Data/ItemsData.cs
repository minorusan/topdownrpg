using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine.Networking.NetworkSystem;
using Core.Characters.Player.Demand;
using System;


namespace Core.Inventory
{
	public static class ItemsData
	{
		private static Dictionary<Type, List<AConsumableBase>> _consumables = new Dictionary<Type, List<AConsumableBase>> ();
		private static List<AReceiptItemBase> _receipts = new List<AReceiptItemBase> ();

		static ItemsData ()
		{

			var items = new string[]{ "hungeritem.id.meat", "hungeritem.id.banana" };
			_receipts.Add (new AReceiptItemBase ("receiptitem.testitem", items));

			var hungerConsumablesList = new List<AConsumableBase> () { 
				new HungerDecreaser ("hungeritem.id.meat", AConsumableBase.EDemand.Hunger, 20),
				new HungerDecreaser ("hungeritem.id.banana", AConsumableBase.EDemand.Hunger, 20),

			};

			var stressConsumablesList = new List<AConsumableBase> () { 
				new StressDecreaser ("stressitem.id.vodka", AConsumableBase.EDemand.Stress, -20),
				new StressDecreaser ("stressitem.id.heroin", AConsumableBase.EDemand.Stress, -50),

			};
			_consumables.Add (typeof(HungerDecreaser), hungerConsumablesList);
			_consumables.Add (typeof(StressDecreaser), stressConsumablesList);
		}

		public static List<AConsumableBase> GetConsumablesOfType<T> () where T : AConsumableBase
		{
			return _consumables [typeof(T)];
		}

		public static AReceiptItemBase GetReceiptById (string receiptID)
		{
			return _receipts.Find (r => r.ItemID == receiptID);
		}
	}

	public static class ItemIDStorage
	{
		private static Dictionary<HungerDecreasers, string> hungerConsumablesIDs = new Dictionary<HungerDecreasers, string> ();
		private static Dictionary<StressDecreasers, string> stressConsumablesIDs = new Dictionary<StressDecreasers, string> ();

		static ItemIDStorage ()
		{
			hungerConsumablesIDs.Add (HungerDecreasers.Meat, "hungeritem.id.meat");
			hungerConsumablesIDs.Add (HungerDecreasers.Banana, "hungeritem.id.banana");

			stressConsumablesIDs.Add (StressDecreasers.Heroin, "stressitem.id.heroin");
			stressConsumablesIDs.Add (StressDecreasers.Vodka, "stressitem.id.vodka");
		}

		public static string GetHungerDecreaserID (HungerDecreasers decreaserType)
		{
			return hungerConsumablesIDs [decreaserType];
		}

		public static string GetStressDecreaserID (StressDecreasers decreaserType)
		{
			return stressConsumablesIDs [decreaserType];
		}
	}

}

