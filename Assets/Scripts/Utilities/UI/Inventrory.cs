using UnityEngine;
using System.Collections;
using Core.Inventory;
using Core.Inventory.Display;
using System.Collections.Generic;


public class Inventrory : MonoBehaviour
{
	private int maxItemsPerRow;
	private Transform[] _rows;
	private List<GameObject> _instantiatedObjects = new List<GameObject> ();

	public GameObject ItemPrefab;

	private void Start ()
	{
		_rows = new RectTransform[transform.childCount];
		for (int i = 0; i < transform.childCount; i++)
		{
			_rows [i] = transform.GetChild (i);
		}
		maxItemsPerRow = PlayerInventory.kMaxInventoryCapacity / _rows.GetLength (0);
		PlayerInventory.Instance.InventoryChanged += OnPlayerInventoryChanged;
		transform.parent.gameObject.SetActive (false);
	}

	private void OnPlayerInventoryChanged ()
	{
		ClearInventorylist ();
		var items = PlayerInventory.Instance.GetItems ();
		var rowIndex = 0;

		for (int i = 0; i < items.Length; i++)
		{
			if (_rows [rowIndex].childCount < maxItemsPerRow)
			{
				InstantiateAtRowIndex (rowIndex, items [i]);
			}
			else
			{
				rowIndex++;
				InstantiateAtRowIndex (rowIndex, items [i]);
			}
		}
	}

	private void ClearInventorylist ()
	{
		foreach (var prefab in _instantiatedObjects)
		{
			Destroy (prefab);
		}
		for (int i = 0; i < _rows.Length; i++)
		{
			_rows [i].DetachChildren ();
		}

		_instantiatedObjects.Clear ();
	}

	private void InstantiateAtRowIndex (int rowIndex, AItemBase item)
	{
		var prefab = Instantiate (ItemPrefab);
		prefab.GetComponent <ConsumableUI> ().SetItem ((AConsumableBase)item);
		prefab.transform.parent = _rows [rowIndex].transform;
		prefab.transform.localPosition = Vector3.zero;
		prefab.transform.localScale = Vector3.one;
		_instantiatedObjects.Add (prefab);
	}

	private void OnDestroy ()
	{
		PlayerInventory.Instance.InventoryChanged -= OnPlayerInventoryChanged;
	}
		
}
