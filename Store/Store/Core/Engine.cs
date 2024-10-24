﻿using Store.Core.Contracts;
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
                string[] input = reader.ReadLine().Split();
                if (input[0] == "exit") { 
                    Environment.Exit(0);
                }
                try { 
                    string result = string.Empty;
                    if (input[0] == "CreateStore") { 
                        string type = input[1];
                        result = controller.CreateStore(type);
                    }
                }
                catch { 
                
                }
            }
           
        }
    }
}
