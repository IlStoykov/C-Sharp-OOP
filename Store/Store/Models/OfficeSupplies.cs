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
        private string checkPackage;

        public OfficeSupplies(string manufacturer, string package, double price, int productNumber )
        {           
            Manufacturer = manufacturer;            
            Price = price;
            ProductNumber = productNumber;
            checkPackage = package;            
        }            

        public abstract string Color { get; }
        public string Manufacturer { get; private set; }

       
        public double Price { 
            get => price;
            private set{
                if (checkPackage == "yes") {
                    price = value * 10; 
                }
                price = value;
            }            
        }
        public int ProductNumber { get; private set; }
    }
}
