using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Models.Contracts
{
    public interface IStore<T> where T : class
    {
        const int WhereHouseMaxLimit = 10;
        const int WhereHouseMinLimit = 3;       


        string StoreType { get; }
        List<T> WareHouse { get; } 
        double Turnover { get; }
        double Profit { get; }
        Dictionary<string, double> ProfitTable { get; }

        void Order(string kind);
    }
}
