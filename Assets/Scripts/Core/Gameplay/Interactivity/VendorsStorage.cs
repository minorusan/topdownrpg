using Core.Inventory;

namespace Core.Gameplay.Interactivity
{
    internal static class VendorsStorage
    {
        public static AItemBase[] GetVendorInventory(string vendorId)
        {
            return PlayerInventory.Instance.GetItems();
        }
    }
}