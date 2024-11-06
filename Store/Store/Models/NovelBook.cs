using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Models
{
    public class NovelBook : Book
    {        
        public NovelBook(string author, string title, double price, int productNumber) : base(author, title, price, productNumber)
        {
        }
    }
}
