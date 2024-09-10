using CarDealership.Models.Contracts;
using CarDealership.Repositories.Contracts;


namespace CarDealership.Repositories
{
    public class CustomerRepository : IRepository<ICustomer>
    {
        private List<ICustomer> customers;
        public CustomerRepository()
        {
            customers = new List<ICustomer>();
        }

        public IReadOnlyCollection<ICustomer> Models => customers.AsReadOnly();

        public void Add(ICustomer model)
        {
            if (!customers.Contains(model))
            {
                customers.Add(model);
            }
            else {
                throw new ArgumentException($"The customer with the name{model.Name} exist for the CarDealership");
            }            
        }

        public bool Exists(string text)
        {
            return customers.Any(x => x.Name == text);
        }

        public ICustomer Get(string text)
        {
            return customers.FirstOrDefault(x => x.Name == text);
            
        }

        public bool Remove(string text)
        {
            ICustomer customer = customers.FirstOrDefault(x => x.Name == text);
            if (customer != null)
            {
                customers.Remove(customer);
                return true;
            }
            return false;
        }
    }
}
