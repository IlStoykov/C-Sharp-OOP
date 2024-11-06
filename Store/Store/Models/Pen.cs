    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;

    namespace Store.Models
    {
        public class Pen : OfficeSupplies
        {            
            private string[] penColors = new string[] { "red", "blue", "black" };
            private static Random randomIndex = new Random();
           public Pen(string manufacturer, bool isPackage, double price, int productNumber) : base(manufacturer, isPackage, price, productNumber)
            {
            Color = penColors[randomIndex.Next(penColors.Length)];
            }
            public override string Color{get;}
        }
    }
