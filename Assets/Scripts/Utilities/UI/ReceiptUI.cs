using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace Core.Inventory.Display
{
	[RequireComponent (typeof(Image))]
	public class ReceiptUI : MonoBehaviour, IPointerClickHandler
	{
		private Image _selfRenderer;

		public string ReceiptId;

		private void Start ()
		{
			_selfRenderer = GetComponent <Image> ();
			_selfRenderer.sprite = InventoryImagesLoader.GetImageForItem (EItemType.Receipt, ReceiptId);
		}

		#region IPointerClickHandler implementation

		public void OnPointerClick (PointerEventData eventData)
		{
			var item = ItemsData.GetReceiptById (ReceiptId);
			item.PerformAction ();
		}

		#endregion
	}
}
