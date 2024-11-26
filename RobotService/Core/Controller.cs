using RobotService.Core.Contracts;
using RobotService.Models;
using RobotService.Models.Contracts;
using RobotService.Repositories;
using RobotService.Utilities.Messages;
using System;
using System.Linq;

namespace RobotService.Core
{
    public class Controller : using RobotService.Core.Contracts;
using RobotService.Models;
using RobotService.Models.Contracts;
using RobotService.Repositories;
using RobotService.Utilities.Messages;
using System;
using System.Linq;
using System.Text;


namespace RobotService.Core
{
    public class Controller : IController
    {
        private SupplementRepository supplements;
        private RobotRepository robots;
        private string[] robotsType = new[] { "DomesticAssistant", "IndustrialAssistant" };
        private string[] supplementsTypes = new[] { "SpecializedArm", "LaserRadar" };
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
            ISupplement newSupplement = null;
            if (!supplementsTypes.Contains(typeName)) {
                return String.Format(OutputMessages.SupplementCannotBeCreated, typeName);
            }
            switch (typeName) 
            {
                case "SpecializedArm":
                    newSupplement = new SpecializedArm(typeName);
                    break;

                case "LaserRadar":
                    newSupplement = new SpecializedArm(typeName);
                    break;
            }
            supplements.AddNew (newSupplement);
            return String.Format(OutputMessages.SupplementCreatedSuccessfully, typeName);
        }

        public string PerformService(string serviceName, int intefaceStandard, int totalPowerNeeded)
        {
            var selectedRobots = this.robots.Models().Where(r => r.InterfaceStandards.Any(i => i == intefaceStandard)).OrderByDescending(y => y.BatteryLevel);

            if (selectedRobots.Count() == 0)
            {
                return string.Format(OutputMessages.UnableToPerform, intefaceStandard);
            }

            int powerSum = selectedRobots.Sum(r => r.BatteryLevel);

            if (powerSum < totalPowerNeeded)
            {
                return string.Format(OutputMessages.MorePowerNeeded, serviceName, totalPowerNeeded - powerSum);
            }

            int usedRobotsCount = 0;

            foreach (var robot in selectedRobots)
            {
                usedRobotsCount++;

                if (totalPowerNeeded <= robot.BatteryLevel)
                {
                    robot.ExecuteService(totalPowerNeeded);
                    break;
                }
                else
                {
                    totalPowerNeeded -= robot.BatteryLevel;
                    robot.ExecuteService(robot.BatteryLevel);
                }

            }

            return string.Format(OutputMessages.PerformedSuccessfully, serviceName, usedRobotsCount);
        }

        public string Report()
        {
            StringBuilder result = new StringBuilder();
            var robotsOrderd = robots.Models().OrderByDescending(y => y.BatteryLevel).ThenBy(x=>x.BatteryCapacity);
            foreach (var robot in robotsOrderd)
            {
                result.AppendLine(robot.ToString());
            }
            return result.ToString().TrimEnd();
        }
        public string RobotRecovery(string model, int minutes)
        {
            int robotsFeeded = 0;

            var selectedRobots = robots.Models().Where(x => x.BatteryLevel < x.BatteryCapacity / 2);
            foreach (var robot in selectedRobots)
            {
                robotsFeeded++;
                robot.Eating(minutes);
            }
            return string.Format(OutputMessages.RobotsFed, robotsFeeded);
        }

        public string UpgradeRobot(string model, string supplementTypeName)
        {
            ISupplement suplementFound = supplements.Models().FirstOrDefault(x => x.GetType().Name == supplementTypeName);
            var selectedModels = robots.Models().Where(x=>x.Model == model);
            var notUpgraded = selectedModels.Where(x => x.InterfaceStandards.All(s => s != suplementFound.InterfaceStandard));
            IRobot robotForUpgrade = notUpgraded.FirstOrDefault();
            if (selectedModels == null) {
                return string.Format(OutputMessages.AllModelsUpgraded, model);
            }
            robotForUpgrade.InstallSupplement(suplementFound);
            supplements.RemoveByName(supplementTypeName);

            return string.Format(OutputMessages.UpgradeSuccessful, model, supplementTypeName);
        }
    }
}

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
