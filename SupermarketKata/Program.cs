using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Schema;
using SupermarketKata.Interfaces;
using SupermarketKata.Models;

namespace SupermarketKata
{
    public class Program
    {
        static void Main(string[] args)
        {

            var items = new Dictionary<string, Item>
            {
                {"A", new Item("A", 50m, 3, 130)},
                {"B", new Item("B", 30m, 2, 45)},
                {"C", new Item("C", 20)},
                {"D", new Item("D", 15)}
            };

            var warehouse = new Warehouse(items);
            var checkout = new Checkout(warehouse);

            Console.WriteLine("Items available");
            foreach (var (sku, item) in items)
            {
                if (item.HasSpecialOffer)
                {
                    Console.WriteLine($"{sku} : {item.Price} or {item.SpecialOfferQuantity} for {item.SpecialOfferPrice}");
                }
                else
                {
                    Console.WriteLine($"{sku} : {item.Price}");

                }
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Type scan to start scanning");
            Console.WriteLine("Type update to update item");

            string line;
            while ((line = Console.ReadLine()) != null)
            {
                try
                {
                    switch (line)
                    {
                        case "scan":
                            Scan(checkout);
                            break;
                        case "update":
                            {
                                Console.WriteLine("This functionality is not accessible from the program just yet");
                                break;
                            }
                        default:
                            checkout.Scan(line);
                            break;
                    }
                }
                catch (SupermarketKataException e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }

        private static void Scan(Checkout checkout)
        {
            Console.WriteLine("Start by scanning an item");
            Console.WriteLine("Enter 2 to get total cost");
            Console.WriteLine("Enter 3 to get scanned items");
            Console.WriteLine("Enter 4 to stopping scanning");

            string line;
            while ((line = Console.ReadLine()) != null)
            {
                try
                {
                    switch (line)
                    {
                        case "2":
                            Console.WriteLine($"Total cost: {checkout.GetTotalPrice()}");
                            break;
                        case "3":
                            {
                                Console.WriteLine("Scanned items:");
                                foreach (var scannedItem in checkout.ScannedItems)
                                {
                                    Console.WriteLine($"{scannedItem.Key} : {scannedItem.Value}");
                                }

                                break;
                            }
                        case "4":
                            break;
                        default:
                            checkout.Scan(line);
                            break;
                    }
                }
                catch (SupermarketKataException e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }
    }
}
