using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Models
{
    public class Pencil : OfficeSupplies
    {
        public Pencil(string manufacturer, bool isPackage, double price) : base(manufacturer, isPackage, price)
        {
        }
    }
}
