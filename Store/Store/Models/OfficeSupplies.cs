using Store.Models.Contracts;
using Store.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Store.Models
{
    public abstract class OfficeSupplies : IProduct
    {        
        private double price;
        
        public OfficeSupplies(string manufacturer, string isPackage, double price, int productNumber )
        {
            Origin = manufacturer;            
            Price = price;
            ProductNumber = productNumber;
            TitleIspackage = isPackage;
        }            

        public abstract string Color { get; }
        public string Origin { get; private set; }
        public string TitleIspackage { get; private set; }

        public double Price { 
            get => price;
            private set{
                if (TitleIspackage == "yes") {
                    price = value * 10; 
                }
                price = value;
            }            
        }
        public int ProductNumber { get; private set; }
    }
}
