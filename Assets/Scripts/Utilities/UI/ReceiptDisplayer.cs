using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace Utils.UI
{
	public class ReceiptDisplayer : MonoBehaviour, IPointerClickHandler
	{
		private const string kReceiptImagesPath = "Sprites/Items/Receipts/Display/";
		private Image _displayImage;

		public void Start ()
		{
			_displayImage = GetComponentInChildren <Image> ();
			_displayImage.gameObject.SetActive (false);
		}

		public void DisplayReceiptForItem (string itemId)
		{
			var image = Resources.Load <Sprite> (string.Format (kReceiptImagesPath + itemId));

			_displayImage.gameObject.SetActive (true);
			_displayImage.sprite = image;
		}

		#region IPointerClickHandler implementation

		public void OnPointerClick (PointerEventData eventData)
		{
			_displayImage.gameObject.SetActive (false);
		}

		#endregion
	}
}

