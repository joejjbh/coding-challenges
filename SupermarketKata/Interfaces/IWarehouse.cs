using SupermarketKata.Models;

namespace SupermarketKata.Interfaces
{
    public interface IWarehouse
    {
        Item GetItem(string stockKeepingUnit);
        void SaveItem(Item item);
        void UpdateItem(Item item);
        decimal GetItemSubTotal(string sku, int quantity);
    }
}