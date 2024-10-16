using RobotService.Models.Contracts;
using Store.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Models
{
    public class Store<T>(string storeKind) : IStore<T> where T : class
    {
        private string storeType;
        private string[] storeTypes = new string[] { "BookStore", "OfficeStore" };
        public string StoreType { 
            get => storeType;
            set {
                if (!storeTypes.Contains(value)) {
                    throw new ArgumentException(ExceptionMessages.StoreType);
                }
            }
            
        }

        public List<T> WareHouse => throw new NotImplementedException();

        public double Turnover => throw new NotImplementedException();

        public double Profit => throw new NotImplementedException();

        public Dictionary<string, double> ProfitTable => throw new NotImplementedException();

        public void Order(string kind)
        {
            throw new NotImplementedException();
        }
    }
}
