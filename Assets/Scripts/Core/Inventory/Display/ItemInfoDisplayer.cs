using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Utilities.UI;
using Core.Inventory;
using System.Linq;
using Core.Utilities;


namespace Utils.UI
{
	public class ItemInfoDisplayer : MonoBehaviour, IPointerClickHandler
	{
		#region Private

		private const string kItemDisplaySpritesPath = "Sprites/Items/Display/";
		private Image _displayImage;

		private AItemBase _currentItem;
		private Text _descriptionText;

		#endregion

		public Button CreateButton;
		public Button DeleteButton;

		#region Monobehaviour

		public void Start ()
		{
			_displayImage = GetComponentInChildren <Image> ();
			CreateButton = GetComponentInChildren <Button> ();
			_descriptionText = GetComponentInChildren <Text> ();
			_descriptionText.gameObject.SetActive (false);
			CreateButton.gameObject.SetActive (false);
			DeleteButton.gameObject.SetActive (false);
			_displayImage.gameObject.SetActive (false);
		}

		public void Reset ()
		{
			_currentItem = null;
			var items = Inventrory.GetInventoryItems ();
		
			foreach (var item in items)
			{
				item.ToggleOutline (false);
			}
		}

		#endregion

		public void DisplayReceiptForItem (string itemId)
		{
			
			var image = Resources.Load <Sprite> (string.Format (kItemDisplaySpritesPath + itemId));
			_currentItem = ItemsData.GetItemById (itemId);
			_displayImage.gameObject.SetActive (true);
			_displayImage.sprite = image;
			_descriptionText.gameObject.SetActive (true);
			_descriptionText.text = ItemIDStorage.GetDescriptionForItem (itemId);
			var requiredItemsCount = 0;
			DeleteButton.gameObject.SetActive (true);
			DehighlightAllItems ();
			if (_currentItem.EItemType == EItemType.Receipt)
			{
				var receipt = ItemsData.GetReceiptById (itemId);
				CreateButton.gameObject.SetActive (true);
				HighlightItems (receipt);
				for (int i = 0; i < receipt.RequiredItems.Length; i++)
				{
					if (Inventrory.GetInventoryItems ().Any (item => item.ItemID == receipt.RequiredItems [i]))
					{
						requiredItemsCount++;
					}
				}

				CreateButton.interactable = receipt.RequiredItems.Length <= requiredItemsCount;
			}
			else
			{
				CreateButton.gameObject.SetActive (false);
			}
		}

		public void CraftItem ()
		{
			var receipt = ItemsData.GetReceiptById (_currentItem.ItemID);
			for (int i = 0; i < receipt.RequiredItems.Length; i++)
			{
				PlayerInventory.Instance.RemoveItemFromInventory (receipt.RequiredItems [i]);
			}

			_displayImage.gameObject.SetActive (false);
			CreateButton.gameObject.SetActive (false);
			DeleteButton.gameObject.SetActive (false);
			_descriptionText.gameObject.SetActive (false);
			BeginCraftingWithCallback ();
		}

		private void HighlightItems (AReceiptItemBase receipt)
		{
			foreach (var item in Inventrory.GetInventoryItems ())
			{
				if (receipt.RequiredItems.Any (i => i == item.ItemID))
				{
					item.ToggleOutline (true);
				}
				else
				{
					item.ToggleOutline (false);
				}
			}
		}

		private void BeginCraftingWithCallback ()
		{
			var receipt = ItemsData.GetReceiptById (_currentItem.ItemID);
			var item = ItemsData.GetItems ().Find (i => i.ItemID == receipt.ResultingItemId);
			var sprite = InventoryImagesLoader.GetImageForItem (item.EItemType, item.ItemID);
			ProcessBarController.StartProcessWithCompletion (receipt.RequiredTime, sprite, () =>
			{
				PlayerInventory.Instance.TryAddItemToInventory (item);
			});
		}

		#region IPointerClickHandler implementation

		public void OnPointerClick (PointerEventData eventData)
		{
			_displayImage.gameObject.SetActive (false);
	
			DehighlightAllItems ();
		}

		public void OnDeleteButton ()
		{
			PlayerInventory.Instance.RemoveItemFromInventory (_currentItem.ItemID);
			_displayImage.gameObject.SetActive (false);
			DeleteButton.gameObject.SetActive (false);
			_descriptionText.gameObject.SetActive (false);
			DehighlightAllItems ();
		}

		private void DehighlightAllItems ()
		{
			var items = Inventrory.GetInventoryItems ();

			var itemsExceptCurrent = items.Where (i => i != null && i.ItemID != _currentItem.ItemID).ToList ();
			foreach (var item in itemsExceptCurrent)
			{
				item.ToggleOutline (false);
			}
		}

		#endregion
	}
}

