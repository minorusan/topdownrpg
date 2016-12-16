using UnityEngine;


namespace Core.Inventory.Display
{
	[RequireComponent (typeof(SpriteRenderer))]
	public class GenericItemDisplay : MonoBehaviour
	{
		private SpriteRenderer _selfRenderer;

		public string ItemID;

		void Start ()
		{
			_selfRenderer = GetComponent <SpriteRenderer> ();
			_selfRenderer.sprite = InventoryImagesLoader.GetImageForItem (EItemType.Generic, ItemID);
		}

		private void OnTriggerEnter2D (Collider2D col)
		{
			PlayerInventory.Instance.TryAddItemToInventory (ItemsData.GetItemById (ItemID));
			gameObject.SetActive (false);
		}

		private void OnDrawGizmos ()
		{
			_selfRenderer = GetComponent <SpriteRenderer> ();
			_selfRenderer.sprite = InventoryImagesLoader.GetImageForItem (EItemType.Generic, ItemID);
		}
	}
}
