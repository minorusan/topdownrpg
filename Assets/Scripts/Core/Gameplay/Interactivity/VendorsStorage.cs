using Core.Inventory;
using System.Collections.Generic;
using System.Linq;

namespace Core.Gameplay.Interactivity
{
    public class Vendor
    {
        private readonly string _vendorid;
        private readonly string[] _itemIds;
        private readonly EItemType _demandedType;
        private List<AItemBase> _items;

        public string Vendorid
        {
            get
            {
                return _vendorid;
            }
        }

        public List<AItemBase> Items
        {
            get
            {
                return _items;
            }
        }

        public Vendor(string _vendorid, string[] _itemIds, EItemType _demandedType)
        {
            this._vendorid = _vendorid;
            this._itemIds = _itemIds;
            this._demandedType = _demandedType;
        }

        public void RemoveItem(string id)
        {
            _items.Remove(_items.Find(item=>item.ItemID == id));
        }

        public void AddItem(string id)
        {
            _items.Add(ItemsData.GetItemById(id));
        }

        private void InitItems()
        {
            foreach (var itemId in _itemIds)
            {
                _items.Add(ItemsData.GetItemById(itemId));
            }
        }


    }


    internal static class VendorsStorage
    {
        private static List<Vendor> _vendors = new List<Vendor>();

        static VendorsStorage()
        {
            _vendors.Add(new Vendor("vendor.id.sara", new[]{ "genericitem.id.nails", "genericitem.id.nails", "genericitem.id.nails",
            "genericitem.id.nails", "genericitem.id.nails", "receiptitem.id.diary", "receiptitem.id.diary", "hungeritem.id.banana",
            "hungeritem.id.banana","genericitem.id.chain", "genericitem.id.nippers"}, EItemType.Trap));
        }

        public static Vendor GetVendor(string vendorId)
        {
            return _vendors.First(v=>v.Vendorid == vendorId);
        }
    }
}