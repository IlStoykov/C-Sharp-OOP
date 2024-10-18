using RobotService.Models.Contracts;
using RobotService.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Repositories
{
    public class SupplementRepository : IRepository<ISupplement>
    {
        private List<ISupplement> supplements;
        SupplementRepository() { 
            supplements = new List<ISupplement>();
        }
        ISupplement supplementFound = null;
        public void AddNew(ISupplement model)
        {
            supplementFound = supplements.FirstOrDefault(x => x.InterfaceStandard == model.InterfaceStandard);
            if (supplementFound == null) {
                return;
            }
            supplements.Add(model);
        }

        public ISupplement FindByStandard(int interfaceStandard) => supplements.FirstOrDefault(x => x.InterfaceStandard == interfaceStandard);
        

        public IReadOnlyCollection<ISupplement> Models()
        {
            return supplements.AsReadOnly();
        }

        public bool RemoveByName(string typeName)
        {
            supplementFound = supplements.FirstOrDefault(x => x.GetType().Name == typeName);
            if (supplementFound != null){
                supplements.Remove(supplementFound);
                return true;
            }
            return false;
        }
    }
}
