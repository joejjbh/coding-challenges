using System;
using SupermarketKata.Interfaces;
using System.Collections.Generic;
using SupermarketKata.Models;

namespace SupermarketKata
{
    public class Warehouse : IWarehouse
    {
        protected internal IDictionary<string, Item> Items;
        protected internal string SkuNotBlankError => "Sku can not be blank";
        protected internal string PriceGreaterThanZeroError => "Price must be greater than 0";
        protected internal string OfferQuantityGreaterThanZeroError => "Special offer quantity must be greater than 0";
        protected internal string OfferPriceGreaterThanZeroError => "Special offer price must be greater than 0";
        protected internal string SkuAlreadyExistsError => "Sku already exists";
        protected internal string ItemDoesNotExistError => "Item given does not exist";

        public Warehouse(IDictionary<string, Item> items)
        {
            Items = items ?? throw new ArgumentNullException(nameof(items));
        }

        public Item GetItem(string stockKeepingUnit)
        {
            return Items.ContainsKey(stockKeepingUnit) ? Items[stockKeepingUnit] : null;
        }

        public void SaveItem(Item item)
        {
            if (GetItem(item.StockKeepingUnit) != null) throw new SupermarketKataException(SkuAlreadyExistsError);

            Items.Add(item.StockKeepingUnit, item);
        }

        public void UpdateItem(Item item)
        {
            if (GetItem(item.StockKeepingUnit) == null) throw new SupermarketKataException(ItemDoesNotExistError);
            Items[item.StockKeepingUnit] = item;
        }

        public decimal GetItemSubTotal(string sku, int quantity)
        {
            var item = Items[sku];

            if (!item.HasSpecialOffer) return item.Price * quantity;

            var specialOfferBundles = quantity / item.SpecialOfferQuantity;
            var normalPricedItems = quantity - specialOfferBundles * item.SpecialOfferQuantity;
            return (decimal)(specialOfferBundles * item.SpecialOfferPrice + normalPricedItems * item.Price);
        }
    }
}
