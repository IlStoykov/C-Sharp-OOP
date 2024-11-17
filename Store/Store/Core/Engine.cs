using Store.Core.Contracts;
using Store.IO;
using Store.IO.Contracts;

namespace Store.Core
{
    public class Engine : IEngine
    {
        private IReader reader;
        private IWriter writer;
        private IController controller;
        

        public Engine() {
            reader = new Reader();
            writer = new Writer();
            controller = new Controller();
        }
        public void Run()
        {
            while (true){
                string[] input = reader.ReadLine().Split(" ");
                if (input[0] == "exit") { 
                    Environment.Exit(0);
                }
                try { 
                    string result = string.Empty;
                    if (input[0] == "CreateStore")
                    {
                        string type = input[1];
                        string name = input[2];
                        result = controller.CreateStore(type, name);
                    }
                    else if (input[0] == "CreateProduct")
                    {
                        string productType = input[1];
                        string origin = input[2];
                        string titleCount = input[3];
                        double price = double.Parse(input[4]);
                        const int productNumber = 0;
                        result = controller.CreateProduct(productType, origin, titleCount, price, productNumber);
                    }
                    else if (input[0] == "Delivery")
                    {
                        string storeName = input[1];
                        result = controller.Delivery(storeName);
                    }
                    else if (input[0] == "GetInventory")
                    {
                        string storeName = input[1];
                        result = controller.GetInventory(storeName);
                    }
                    else if (input[0] == "Order") {
                        if (input[2] == "Pen" || input[1] == "Pencil") {
                            result = controller.OrderOfficeSupply(input[1], input[2], input[3]);
                        }
                        else{
                            result = controller.OrderBook(input[1], input[2], input[3]);
                        }
                    }

                    writer.Write(result);
                }
                catch(Exception ex)
                {
                    if (ex.Message.Contains("items for delivety"))
                    {
                        string[] messageTokens = ex.Message.Split(' ');
                        string storeName = messageTokens[2].Trim(',');
                        controller.Delivery(storeName);

                    }
                    else {
                        writer.WriteLine(ex.Message);
                    }
                    
                }
            }           
        }
    }
}
