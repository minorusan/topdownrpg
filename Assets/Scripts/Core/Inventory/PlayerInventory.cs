using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Core.Inventory;
using System;


namespace Core.Inventory
{

	public class PlayerInventory
	{
		public const int kMaxInventoryCapacity = 27;
		private List<AItemBase> _items;
		private static PlayerInventory _instance;

		public event Action InventoryChanged;

		public static PlayerInventory Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new PlayerInventory ();
				}
				return _instance;
			}
		}

		private PlayerInventory ()
		{
			_items = new List<AItemBase> ();
		}

		public bool TryAddItemToInventory (AItemBase item)
		{
			if (_items.Count < kMaxInventoryCapacity)
			{
				_items.Add (item);
				Debug.Log (item.ItemID + " was added to inventory.");
				InventoryChanged ();
				return true;
			}
			return false;
		}

		public void RemoveItemFromInventory (string item)
		{
			var index = _items.FindIndex (i => i.ItemID == item);
			_items.RemoveAt (index);
			Debug.Log (item + " was removed from inventory.");
			InventoryChanged ();
		}

		public AItemBase[] GetItems ()
		{
			return _items.ToArray ();
		}

		public AItemBase TryGetItemAtIndex (int index)
		{
			if (index < _items.Count)
			{
				return _items [index];
			}
			return null;
		}
	}
}

