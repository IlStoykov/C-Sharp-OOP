using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Models
{
    public class Pencil : OfficeSupplies
    {
        private string[] pencilColors = new string[] { "red", "blue", "black", "green", "yellow", "purple", "brown" };
        Random randomIndex = new Random();
        public Pencil(string manufacturer, bool isPackage, double price, int productNumber) : base(manufacturer, isPackage, price, productNumber)
        {
            Color = pencilColors[randomIndex.Next(pencilColors.Length)];
        }
        public override string Color{ get; }
    }
}
