using RobotService.Models.Contracts;
using RobotService.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotService.Models
{
    public abstract class Robot : IRobot
    {
        private string model;
        private int batteryCapacity;
        private List<int> interfaceStandards;
        public Robot(string model, int batteryCapacity, int conversionCapacityIndex) {
            BatteryCapacity = batteryCapacity;
            BatteryLevel = BatteryCapacity;
            interfaceStandards = new List<int>();
            Model = model; 
            ConvertionCapacityIndex = conversionCapacityIndex;            
        }
        public string Model {
            get => model;
            set {
                if (string.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentException(ExceptionMessages.ModelNullOrWhitespace);
                }
                model = value;
            }
        }
        public int BatteryCapacity { 
            get=> batteryCapacity;
            set{
                if (value < 0) {
                    throw new ArgumentException(ExceptionMessages.BatteryCapacityBelowZero);
                }
                batteryCapacity = value;
            }
        }
        public int BatteryLevel { get; private set; }

        public int ConvertionCapacityIndex { get; private set; }

        public IReadOnlyCollection<int> InterfaceStandards => interfaceStandards.AsReadOnly();

        public void Eating(int minutes)
        {
            BatteryLevel += minutes * ConvertionCapacityIndex;
            if (BatteryLevel > BatteryCapacity) {
                BatteryLevel = BatteryCapacity;
            }
        }
        public bool ExecuteService(int consumedEnergy)
        {
            return BatteryLevel >= consumedEnergy ? (BatteryLevel-=consumedEnergy)>=0 : false;
        }

        public void InstallSupplement(ISupplement supplement)
        {
            int interfaceFound = supplement.InterfaceStandard;
            interfaceStandards.Add(interfaceFound);

            int supplementUsage = supplement.BatteryUsage;
            BatteryCapacity -= supplementUsage;
            BatteryLevel -= supplementUsage;
        }
        public override string ToString()
        {
           StringBuilder result = new StringBuilder();
        result.AppendLine($"{GetType().Name} {Model}:");
        result.AppendLine($"--Maximum battery capacity: {BatteryCapacity}");
        result.AppendLine($"--Current battery level: {BatteryLevel}");
        string supplementsInstalled = interfaceStandards.Any() ? string.Join(", ", interfaceStandards) : "none";
        result.AppendLine($"--Supplements installed: {supplementsInstalled}");

        return result.ToString().Trim();
        }
    }
}
