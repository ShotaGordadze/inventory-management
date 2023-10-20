using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwlTrading.InventoryManagement.Domain.ProductManagement
{
    public partial class Product
    {
        private void Log(string message)
        {
            Console.WriteLine(message);
        }

        // May need fix
        public void ChangeStockThreshold(decimal newStockThreshold)
        {
            if (newStockThreshold > 0)
            {
                StockThreshold = newStockThreshold;
            }

            UpdateLowStock();
        }

        public void UpdateLowStock()
        {
            if (amountInStock < stockThreshold)
                IsBelowStockThreshold = true;
            else
                IsBelowStockThreshold = false;
        }

        public string DisplayShortDetails()
        {
            return $"ID {Id}: {Name} - {AmountInStock} items in the stock";
        }

        public string DisplayFullDetails()
        {
            StringBuilder sb = new();

            _ = sb.Append($"{Id}: {Name}, {description}, {Price}, {AmountInStock} items(s) int stock");

            if (IsBelowStockThreshold)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write( " !!!STOCK LOW!!! " );
                Console.ResetColor();
            }

            return sb.ToString();
        }

        public void IncreaseStock()
        {
            AmountInStock++;

            UpdateLowStock();
        }

        public void IncreaseStock(int amount)
        {
            if (amount > 0)
                AmountInStock += amount;

            UpdateLowStock();
        }

        public void DecreaseStock()
        {
            if (AmountInStock > 0)
            {
                AmountInStock--;
            }
            else
                Log($"Out of stock - {Id}: {Name}");

            UpdateLowStock();
        }

        public void DecreaseStock(int amount)
        {
            if (amount > 0 && AmountInStock - amount >= 0)
                AmountInStock -= amount;
            else
            {
                Log($"Not enough {Name} in stock");
            }

            UpdateLowStock();
        }
    }
}
