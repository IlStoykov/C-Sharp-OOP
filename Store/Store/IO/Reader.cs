using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.IO.Contracts;

namespace Store.IO
{
    public class Reader : IReader
    {
        public string ReadLine() => Console.ReadLine();

    }
}
