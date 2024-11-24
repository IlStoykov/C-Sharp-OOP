
using Store.IO.Contracts;

namespace Store.IO
{
    public class Reader : IReader
    {
        public string ReadLine() => Console.ReadLine();

    }
}
