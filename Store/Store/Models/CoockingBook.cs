using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Models
{
    public class CoockingBook : Book
    {        
        public CoockingBook(string author, string title, double price, int productNumber) : base(author, title, price, productNumber)
        {
        }
    }
}
