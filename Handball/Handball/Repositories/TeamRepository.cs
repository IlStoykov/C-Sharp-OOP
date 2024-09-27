using Handball.Models.Contracts;
using Handball.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handball.Repositories
{
    public class TeamRepository : IRepository<ITeam>
    {
        private List<ITeam> teams;
        public TeamRepository() {
            teams = new List<ITeam>();
        }
        public IReadOnlyCollection<ITeam> Models => teams.AsReadOnly();
        public void AddModel(ITeam model)
        {
            if (teams.Any(x => x.Name == model.Name)){
                teams.Add(model);
            }
        }
        public bool ExistsModel(string name)
        {
            return teams.Any(x => x.Name == name);
        }

        public ITeam GetModel(string name)
        {
           return teams.FirstOrDefault(x => x.Name == name);
        }

        public bool RemoveModel(string name)
        {
            ITeam modelFound = teams.FirstOrDefault(y => y.Name == name);
            if (modelFound != null){
                teams.Remove(modelFound);
                return true;
            }
            return false;
        }
    }
}
