using RobotService.Models.Contracts;
using RobotService.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;


namespace RobotService.Repositories
{
    public class SupplementRepository : IRepository<ISupplement>
    {
        private List<ISupplement> supplements;
        public SupplementRepository() { 
            supplements = new List<ISupplement>();
        }
        ISupplement supplementFound = null;
        public void AddNew(ISupplement model)
        {
            supplementFound = supplements.FirstOrDefault(x => x.InterfaceStandard == model.InterfaceStandard);
            if (supplementFound == null) {
                supplements.Add(model);
            }            
        }
        public ISupplement FindByStandard(int interfaceStandard) => supplements.FirstOrDefault(x => x.InterfaceStandard == interfaceStandard);
        
        public IReadOnlyCollection<ISupplement> Models()
        {
            return supplements.AsReadOnly();
        }
        public bool RemoveByName(string typeName)
        {
            supplementFound = supplements.FirstOrDefault(x => x.GetType().Name.Equals(typeName, StringComparison.OrdinalIgnoreCase));
            if (supplementFound != null){
                supplements.Remove(supplementFound);
                return true;
            }
            return false;
        }
    }
}
