using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Models
{
    public class CoockingBook : Book
    {
        private const string BookType = "CookingBook";
        public CoockingBook(string author, string title, double price) : base(author, title, price, BookType)
        {
        }
    }
}
