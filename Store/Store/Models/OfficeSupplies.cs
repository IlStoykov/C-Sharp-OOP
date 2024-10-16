using Store.Models.Contracts;


namespace Store.Models
{
    public abstract class OfficeSupplies : IOfficeSupplies
    {
        private string color;
        private double price;
        
        private string[] penColors = new string[] { "red", "blue", "black" };
        private string[] pencilColors = new string[] { "red", "blue", "black", "green", "yellow", "purple", "brown" };
        

        public OfficeSupplies(string manufacturer, bool isPackage, double price)
        {           
            
            Manufacturer = manufacturer;
            IsPackage = isPackage;
            Price = price;
        }
        
        private static Random randomIndex = new Random();        

        public string Color {
            get => color;
            private set {
                if (GetType().Name == "Pen") { 
                    int index = randomIndex.Next(penColors.Length);
                    color = penColors[index];
                }
                if (GetType().Name == "Pencil") {
                    int index = randomIndex.Next(pencilColors.Length);
                    color = pencilColors[index];
                }
            }
        }
        public string Manufacturer { get; private set; }

        public bool IsPackage { get; private set; }

        public double Price { 
            get => price;
            private set{
                if (IsPackage) {
                    price = value * 10; 
                }
                price = value;
            }            
        }
    }
}
