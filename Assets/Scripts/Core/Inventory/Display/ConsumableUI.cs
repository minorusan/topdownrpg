using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace  Core.Inventory.Display
{
	[RequireComponent (typeof(Image))]
	public class ConsumableUI : ConsumableDisplay, IPointerClickHandler
	{
		private Image _image;

		public override void ApplyImage ()
		{
			_image = GetComponent <Image> ();
			_image.sprite = InventoryImagesLoader.GetConsumableSprite (_selectedConsumable.ItemID);
		}

		public void SetItem (AConsumableBase item)
		{
			_selectedConsumable = item;
			ApplyImage ();
		}

		#region IPointerClickHandler implementation

		public void OnPointerClick (PointerEventData eventData)
		{
			_selectedConsumable.PerformAction ();
			PlayerInventory.Instance.RemoveItemFromInventory (_selectedConsumable);
		}

		#endregion
	}
}

