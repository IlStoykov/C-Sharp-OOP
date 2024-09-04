using CarDealership.Models.Contracts;


namespace CarDealership.Models
{
    public abstract class Vehicle : IVehicle
    {
        private string model;
        private double price;
        private List<string> buyers;
        public Vehicle(string model, double price) { 
            Model = model;
            Price = price;
            buyers = new List<string>();
        }
        public string Model {
            get => model;
            set {
                if (string.IsNullOrWhiteSpace(value)) { 
                    throw new ArgumentNullException("Model is required.");
                }
                model = value;
            }
        }
        public double Price { 
            get => price;
            set{
                if (value <= 0) {
                    throw new ArgumentOutOfRangeException("Price must be a positive number.");
                }
                price = value;
            }
        }
        public IReadOnlyCollection<string> Buyers => buyers.AsReadOnly();

        public int SalesCount => Buyers.Count;

        public void SellVehicle(string buyerName)
        {
            if (!string.IsNullOrEmpty(buyerName)) {
                buyers.Add(buyerName); 
            }
        }
        public override string ToString()
        {
            return $"{Model} - Price: {Price:F2}, Total Model Sales: {SalesCount}";
        }
    }
}
