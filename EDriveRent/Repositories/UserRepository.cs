using EDriveRent.Models.Contracts;
using EDriveRent.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Repositories
{
    public class UserRepository : IRepository<IUser>
    {
        private List<IUser> users;

        public UserRepository() { 
            users = new List<IUser>();
        }
        public void AddModel(IUser model)// check if should duplicate classes
        {
            var userFound = users.Any(x => x.DrivingLicenseNumber == model.DrivingLicenseNumber);
            if (!userFound)
            {
                users.Add(model);
            }            
        }
        public IUser FindById(string identifier)
        {
            return users.FirstOrDefault(x => x.DrivingLicenseNumber == identifier);
        }

        public IReadOnlyCollection<IUser> GetAll()
        {
            return users.AsReadOnly();
        }
        public bool RemoveById(string identifier)
        {
            return users.Remove(users.FirstOrDefault(x => x.DrivingLicenseNumber == identifier));
        }
    }
}
