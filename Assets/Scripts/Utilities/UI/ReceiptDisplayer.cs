using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Utilities.UI;
using Core.Inventory;
using System.Linq;


namespace Utils.UI
{
	public class ReceiptDisplayer : MonoBehaviour, IPointerClickHandler
	{
		private const string kReceiptImagesPath = "Sprites/Items/Receipts/Display/";
		private Image _displayImage;
		private Button _createButton;
		private float _timeCrafting;
		private float _requiredTime;
		private bool _crafting;
		private AReceiptItemBase _currentItem;

		public Image CraftBar;
		public Image CraftBarLogo;

		public void Start ()
		{
			_displayImage = GetComponentInChildren <Image> ();
			_createButton = GetComponentInChildren <Button> ();
			_displayImage.gameObject.SetActive (false);
		}

		private void Update ()
		{
			if (_crafting)
			{
				_timeCrafting += Time.deltaTime;
				if (_timeCrafting < _requiredTime)
				{
					CraftBar.fillAmount = _timeCrafting / _requiredTime;
				}
				else
				{
					var item = ItemsData.GetItems ().Find (i => i.ItemID == _currentItem.ResultingItemId);
					PlayerInventory.Instance.TryAddItemToInventory (item);
					CraftBar.gameObject.SetActive (false);
					_crafting = false;
				}
			}

		}

		public void DisplayReceiptForItem (string itemId)
		{
			var image = Resources.Load <Sprite> (string.Format (kReceiptImagesPath + itemId));
			_currentItem = ItemsData.GetReceiptById (itemId);
			_displayImage.gameObject.SetActive (true);
			_displayImage.sprite = image;

			var requiredItemsCount = 0;
			HighlightItems (_currentItem);
			for (int i = 0; i < _currentItem.RequiredItems.Length; i++)
			{
				if (Inventrory.GetInventoryItems ().Any (item => item.ItemID == _currentItem.RequiredItems [i]))
				{
					requiredItemsCount++;
				}
			}

			_createButton.interactable = _currentItem.RequiredItems.Length <= requiredItemsCount;
		}

		public void CraftItem ()
		{
			for (int i = 0; i < _currentItem.RequiredItems.Length; i++)
			{
				PlayerInventory.Instance.RemoveItemFromInventory (_currentItem.RequiredItems [i]);
			}
			var item = ItemsData.GetItems ().Find (i => i.ItemID == _currentItem.ResultingItemId);
			_requiredTime = _currentItem.RequiredTime;
			_crafting = true;
			_timeCrafting = 0f;
			CraftBar.fillAmount = 0f;
			CraftBarLogo.sprite = InventoryImagesLoader.GetImageForItem (item.EItemType, item.ItemID);
			CraftBar.gameObject.SetActive (true);
			_displayImage.gameObject.SetActive (false);
		}

		static void HighlightItems (AReceiptItemBase receipt)
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

		#region IPointerClickHandler implementation

		public void OnPointerClick (PointerEventData eventData)
		{
			_displayImage.gameObject.SetActive (false);
			foreach (var item in Inventrory.GetInventoryItems ())
			{
				item.ToggleOutline (false);
			}
		}

		#endregion
	}
}

