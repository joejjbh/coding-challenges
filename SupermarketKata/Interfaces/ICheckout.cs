namespace SupermarketKata.Interfaces
{
    public interface ICheckout
    {
        void Scan(string stockKeepingUnit);
        decimal GetTotalPrice();
    }
}
