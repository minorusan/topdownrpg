using UnityEngine;
using System.Collections;
using System;
using Utils.UI;


namespace Core.Inventory
{
	public class AReceiptItemBase : AItemBase
	{
		private readonly string[] _requiredItemsIdentifiers;

		public string[] RequiredItems
		{
			get
			{
				return (string[])_requiredItemsIdentifiers.Clone ();
			}
		}

		public AReceiptItemBase (string itemId, string[] requiredItemsIdentifiers) : base (itemId, EItemType.Receipt)
		{
			_requiredItemsIdentifiers = requiredItemsIdentifiers;
			SetAction (DisplayReceipt ());
		}

		private Action DisplayReceipt ()
		{
			return () =>
			{
				var receiptDisplayer = FindObjectOfType<ReceiptDisplayer> ();
				if (receiptDisplayer != null)
				{
					receiptDisplayer.DisplayReceiptForItem (ItemID);
				}
				else
				{
					Debug.LogError ("ReceiptDisplayer was not found.");
				}
			};
		}
	}
}

