﻿using BankLoan.Models.Contracts;
using BankLoan.Utilities.Messages;
using System;


namespace BankLoan.Models
{
    public abstract class Client : IClient
    {
        protected string name;
        protected string id;
        protected double income;
        public Client(string name, string id, int interest, double income) { 
            Name = name;
            Id = id;
            Interest = interest;
            Income = income;
        }
        public string Name {
            get => name;
            set {
                if (string.IsNullOrWhiteSpace(value)) { 
                    throw new ArgumentNullException(ExceptionMessages.ClientNameNullOrWhitespace);
                }
                name = value;
            }
        }
        public string Id { 
            get => id;
            set{
                if (string.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentException(ExceptionMessages.ClientIdNullOrWhitespace);
                }
                id = value;
            }
        }
        public int Interest { get; protected set; }

        public double Income { 
            get => income;
            set{
                if (value <= 0) { 
                    throw new ArgumentOutOfRangeException(ExceptionMessages.ClientIncomeBelowZero);
                }
                income = value;
            }
        }
        public abstract void IncreaseInterest();
        
    }
}
