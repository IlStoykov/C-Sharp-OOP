using CarDealership.Models.Contracts;

namespace CarDealership.Models
{
    public abstract class Customer : ICustomer
    {
        private string name;
        private List<string> purchases;
        public Customer(string name) { 
            Name = name;
            purchases = new List<string>();
        }
        public string Name {
            get => name;
            set {
                if (string.IsNullOrWhiteSpace(value)) { 
                    throw new ArgumentNullException("Name is required.");
                }
                name = value;
            }
        }
        public IReadOnlyCollection<string> Purchases => purchases.AsReadOnly();

        public void BuyVehicle(string vehicleModel)
        {
            if (!string.IsNullOrEmpty(vehicleModel)) {
                purchases.Add(vehicleModel);
            }
        }
        public override string ToString() => $"{Name} - Purchases: {Purchases.Count}";


    }
}
