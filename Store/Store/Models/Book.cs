using Store.Models.Contracts;
using Store.Utilities.Messages;
using System;

namespace Store.Models
{
    public abstract class Book : IBook
    {
        private double price;
        public Book() { }
        public string Author { get; private set; }

        public string Title { get; private set; }

        public double Price {
            get => price;
            private set {
                if (value <= 0) {
                    throw new ArgumentException(ExceptionMessages.PriceMustBePositive);
                }
            }
        }
        public string Genre { get; private set; }
    }
}
