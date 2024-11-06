
using Store.Core;
using Store.Core.Contracts;

namespace Store
{

    public class StartUp
    {
        static void Main(string[] args) {
            IEngine engine = new Engine();
            engine.Run();
        }
    }
}

