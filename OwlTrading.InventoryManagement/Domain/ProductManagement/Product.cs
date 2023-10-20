using OwlTrading.InventoryManagement.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwlTrading.InventoryManagement.Domain.ProductManagement
{
    public partial class Product
    {
        private int id;
        private string name = string.Empty;
        private string? description;
        private decimal stockThreshold;
        private int amountInStock;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value.Length > 15 ? value[..15] : value;
            }
        }

        public string? Description
        {
            get
            {
                description = string.Empty; return description;
            }
            set
            {
                if (value == null)

                    description = string.Empty;

                else
                    description = value.Length > 250 ? value[..250] : value;
            }
        }

        public decimal StockThreshold
        {
            get { return stockThreshold; }
            set
            {
                if (value > 0)
                    stockThreshold = value;
            }
        }

        public int AmountInStock { get { return amountInStock; } set { amountInStock = value; UpdateLowStock(); } }

        public ItemType Type { get; set; }

        public Price Price { get; set; }

        public bool IsBelowStockThreshold { get; private set; }

        //Can be derived in classes


        public Product(int id, string name, string? description, ItemType type, Price price, int stockThreshold)
        {
            Id = id;
            Name = name;
            Description = description;
            Type = type;
            Price = price;
            StockThreshold = stockThreshold;
        }
    }
}
