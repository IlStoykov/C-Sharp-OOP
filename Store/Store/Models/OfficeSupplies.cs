using Store.Models.Contracts;
using Store.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Store.Models
{
    public abstract class OfficeSupplies : IOfficeSupplies
    {        
        private double price;               

        public OfficeSupplies(string manufacturer, bool isPackage, double price, int productNumber )
        {           
            Manufacturer = manufacturer;
            IsPackage = isPackage;
            Price = price;
            ProductNumber = productNumber;
            
        }            

        public abstract string Color { get; }
        public string Manufacturer { get; private set; }

        public bool IsPackage { get; private set; }

        public double Price { 
            get => price;
            private set{
                if (IsPackage) {
                    price = value * 10; 
                }
                price = value;
            }            
        }
        public int ProductNumber { get; private set; }
    }
}
