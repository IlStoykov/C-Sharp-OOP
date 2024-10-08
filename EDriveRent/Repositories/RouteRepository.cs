using EDriveRent.Models.Contracts;
using EDriveRent.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;


namespace EDriveRent.Repositories
{
    public class RouteRepository : IRepository<IRoute>
    {
        private List<IRoute> routes;
        public RouteRepository() {
            routes = new List<IRoute>();
        }
        public void AddModel(IRoute model)
        {
            IRoute routeFound = routes.FirstOrDefault(x => x.RouteId == model.RouteId);
            if (routeFound != null){
                routes.Remove(routeFound);
            }
            routes.Add(model);
        }
        public IRoute FindById(string identifier)
        {
            return routes.FirstOrDefault(x => x.RouteId == int.Parse(identifier));
        }
        public IReadOnlyCollection<IRoute> GetAll()
        {
            return routes.AsReadOnly();
        }

        public bool RemoveById(string identifier)
        {
            return routes.Remove(routes.FirstOrDefault(x => x.RouteId == int.Parse(identifier)));
        }
    }
}
