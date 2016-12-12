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
				InventoryChanged ();
				return true;
			}
			return false;
		}

		public void RemoveItemFromInventory (AItemBase item)
		{
			_items.Remove (item);
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

