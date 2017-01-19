using Core.Inventory;

using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Image)]
    public class TradeItemUI : MonoBehaviour
    {
        private Image _image;

        // Use this for initialization
        void OnEnable()
        {
            _image = GetComponent<Image>();
        }

        public void InitWithItem(AItemBase item)
        {
            _image.sprite = InventoryImagesLoader.GetImageForItem(item.EItemType, item.ItemID);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
