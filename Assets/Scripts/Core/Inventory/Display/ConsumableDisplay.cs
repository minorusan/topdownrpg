using UnityEngine;
using System.Linq;
using System.Collections.Generic;


namespace Core.Inventory.Display
{
	public abstract class ConsumableDisplay : MonoBehaviour
	{
		#region Protected

		protected AConsumableBase _selectedConsumable;

		#endregion

		public bool RequiresInitialisation = true;

		public AConsumableBase.EDemand AffectsDemand;
		public HungerDecreasers HungerDecreaserType;
		public StressDecreasers StressDecreaserType;

		private void Start ()
		{
			InitConsumable ();
		}

		public void InitConsumable ()
		{
			if (!RequiresInitialisation)
			{
				return;
			}

			string selectedItemId = "";
			List<AConsumableBase> consumables = new List<AConsumableBase> ();
			switch (AffectsDemand)
			{
			case AConsumableBase.EDemand.Hunger:
				{
					selectedItemId = ItemIDStorage.GetHungerDecreaserID (HungerDecreaserType);
					consumables = ItemsData.GetConsumablesOfType<HungerDecreaser> ();
					break;
				}
			case AConsumableBase.EDemand.Stress:
				{
					selectedItemId = ItemIDStorage.GetStressDecreaserID (StressDecreaserType);
					consumables = ItemsData.GetConsumablesOfType<StressDecreaser> ();
					break;
				}
			}
			if (consumables.Count > 0)
			{
				_selectedConsumable = consumables.First (c => c.ItemID == selectedItemId);
				//Debug.Assert (_selectedConsumable. != null, "error for " + gameObject.name);
				ApplyImage ();
			}
		}

		public abstract void ApplyImage ();
	}
}

