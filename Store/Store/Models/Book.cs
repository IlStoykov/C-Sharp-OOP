using Store.Models.Contracts;
using Store.Utilities.Messages;
using System;

namespace Store.Models
{
    public abstract class Book : IBook
    {
        private double price;
        public Book(string author, string title, double price, string genre) {
            Author = author;
            Title = title;
            Price = price;
            Genre = genre;
        }
        public string Author { get; private set; }

        public string Title { get; private set; }

        public double Price {
            get => price;
            private set {
                if (value <= 0) {
                    throw new ArgumentException(ExceptionMessages.PriceMustBePositive);
                }
                price = value;
            }
        }
        public string Genre { get; private set; }
    }
}
