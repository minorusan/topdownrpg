using UnityEngine;


namespace Core.Inventory.Display
{
	[RequireComponent (typeof(SpriteRenderer))]
	public class ReceiptBehaviour : MonoBehaviour
	{
		private SpriteRenderer _selfRenderer;

		public string ReceiptId;

		void Start ()
		{
			_selfRenderer = GetComponent <SpriteRenderer> ();
			_selfRenderer.sprite = InventoryImagesLoader.GetImageForItem (EItemType.Receipt, ReceiptId);
		}

		private void OnTriggerEnter2D (Collider2D col)
		{
			PlayerInventory.Instance.TryAddItemToInventory (ItemsData.GetReceiptById (ReceiptId));
			gameObject.SetActive (false);
		}
	}
}
