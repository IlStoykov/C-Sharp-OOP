using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Models
{
    public class NovelBook : Book
    {
        private const string BookType = "NovelBook";
        public NovelBook(string author, string title, double price, string genre) : base(author, title, price, BookType)
        {
        }
    }
}
