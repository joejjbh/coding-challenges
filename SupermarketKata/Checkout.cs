using SupermarketKata.Interfaces;
using System;
using System.Collections.Generic;

namespace SupermarketKata
{
    public class Checkout : ICheckout
    {
        protected internal IWarehouse Warehouse;

        protected internal string SkuNotExistError => "Given item does not exist";

        protected internal IDictionary<string, int> ScannedItems { get; set; }

        public Checkout(IWarehouse warehouse)
        {
            Warehouse = warehouse ?? throw new ArgumentNullException(nameof(warehouse));
            ScannedItems = new Dictionary<string, int>();
        }

        public void Scan(string stockKeepingUnit)
        {
            if (ScannedItems.ContainsKey(stockKeepingUnit))
            {
                ScannedItems[stockKeepingUnit] += 1;
            }
            else
            {
                var item = Warehouse.GetItem(stockKeepingUnit);
                if (item == null) throw new SupermarketKataException(SkuNotExistError);
                ScannedItems.Add(stockKeepingUnit, 1);
            }
        }

        public decimal GetTotalPrice()
        {
            var total = 0m;

            foreach (var (sku, quantity) in ScannedItems)
            {
                total += Warehouse.GetItemSubTotal(sku, quantity);
            }
            return total;
        }
    }
}