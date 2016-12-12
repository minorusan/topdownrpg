using UnityEngine;
using System.Collections;
using System;


namespace Core.Inventory
{
	[Serializable]
	public class AItemBase:UnityEngine.Object
	{
		#region Private

		private readonly string _itemID;
		private  Action _itemAction;

		#endregion

		public string ItemID
		{
			get
			{
				return _itemID;
			}
		}

		public AItemBase (string itemId)
		{
			Debug.Assert (!string.IsNullOrEmpty (itemId), GetType ().ToString () + ":: no item ID was assigned");

			_itemID = itemId;
		}

		public void PerformAction ()
		{
			Debug.Assert (_itemAction != null, GetType ().ToString () + ":: no item action was assigned");
			_itemAction ();
		}

		protected void SetAction (Action action)
		{
			_itemAction = action;
		}
	}
}

