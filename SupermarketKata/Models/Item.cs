using System;

namespace SupermarketKata.Models
{
    public class Item
    {
        public string StockKeepingUnit { get; set; }
        public decimal Price { get; set; }
        public int? SpecialOfferQuantity { get; set; }
        public decimal? SpecialOfferPrice { get; set; }
        public bool HasSpecialOffer => SpecialOfferQuantity > 0 && SpecialOfferPrice > 0;

        public Item(string stockKeepingUnit, decimal price) : this(stockKeepingUnit, price, 0, 0)
        {
        } 

        public Item(string stockKeepingUnit, decimal price, int specialOfferQuantity, decimal specialOfferPrice)
        {
            if (price <= 0) throw new ArgumentOutOfRangeException(nameof(price));
            if (specialOfferQuantity < 0) throw new ArgumentOutOfRangeException(nameof(specialOfferQuantity));
            if (specialOfferPrice < 0) throw new ArgumentOutOfRangeException(nameof(specialOfferPrice));
            StockKeepingUnit = stockKeepingUnit;
            Price = price;
            SpecialOfferQuantity = specialOfferQuantity;
            SpecialOfferPrice = specialOfferPrice;
        }
    }
}