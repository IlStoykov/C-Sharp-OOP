﻿using Store.IO.Contracts;


namespace Store.IO
{
    public class Writer : IWriter
    {
        public void Write(string message) => Console.Write(message);        

        public void WriteLine(string message) => Console.WriteLine(message);
        
    }
}
