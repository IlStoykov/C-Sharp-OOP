using RobotService.Models.Contracts;
using RobotService.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Repositories
{
    public class RobotRepository : IRepository<IRobot>
    {
        private List<IRobot> robots;
        IRobot robotFound = null;
        public RobotRepository() { 
            robots = new List<IRobot>();
        }
        public void AddNew(IRobot model)
        {
            if (robots.FirstOrDefault(x => x.Model == model.Model) == null) { 
                robots.Add(model);
            }
        }
        public IRobot FindByStandard(int interfaceStandard) {
            return robots.FirstOrDefault(x => x.InterfaceStandards.Any(x => x == interfaceStandard));
        }        
        public IReadOnlyCollection<IRobot> Models() => robots.AsReadOnly();        

        public bool RemoveByName(string typeName)
        {
            robotFound = robots.FirstOrDefault(x => x.Model == typeName);
            if (robotFound != null) { 
                robots.Remove(robotFound);
                return true;
            }
            return false;
        }
    }
}
