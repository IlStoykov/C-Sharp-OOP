using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Models
{
    public class Pen : OfficeSupplies
    {
        public Pen(string manufacturer, bool isPackage, double price) : base(manufacturer, isPackage, price)
        {
        }
    }
}
