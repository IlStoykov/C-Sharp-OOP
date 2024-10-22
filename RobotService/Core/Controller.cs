using RobotService.Core.Contracts;
using RobotService.Models;
using RobotService.Models.Contracts;
using RobotService.Repositories;
using RobotService.Utilities.Messages;
using System;
using System.Linq;

namespace RobotService.Core
{
    public class Controller : IController
    {
        private SupplementRepository supplements;
        private RobotRepository robots;
        private string[] robotsType = new[] { "DomesticAssistant", "IndustrialAssistant" }; 
        public Controller() { 
            supplements = new SupplementRepository();
            robots = new RobotRepository();
        }
        public string CreateRobot(string model, string typeName)
        {
            IRobot newRobot = null;
            if (!robotsType.Contains(typeName)) { 
                return string.Format(OutputMessages.RobotCannotBeCreated, typeName);
            }
            switch (typeName)
            {
                case "DomesticAssistant":
                    newRobot = new DomesticAssistant(model);
                    break;

                case "IndustrialAssistant":
                    newRobot = new IndustrialAssistant (model);
                    break;
            }
            robots.AddNew(newRobot);
            return string.Format(OutputMessages.RobotCreatedSuccessfully, typeName, model);
        }

        public string CreateSupplement(string typeName)
        {
            throw new NotImplementedException();
        }

        public string PerformService(string serviceName, int intefaceStandard, int totalPowerNeeded)
        {
            throw new NotImplementedException();
        }

        public string Report()
        {
            throw new NotImplementedException();
        }

        public string RobotRecovery(string model, int minutes)
        {
            throw new NotImplementedException();
        }

        public string UpgradeRobot(string model, string supplementTypeName)
        {
            throw new NotImplementedException();
        }
    }
}
