using Handball.Models.Contracts;
using Handball.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Handball.Repositories
{
    public class PlayerRepository : IRepository<IPlayer>
    {
        private List<IPlayer> players;
        public PlayerRepository() {     
            players = new List<IPlayer>();
        }
        public IReadOnlyCollection<IPlayer> Models => players.AsReadOnly();

        public void AddModel(IPlayer model)
        {
            IPlayer playerFpund = players.FirstOrDefault(x => x.Name == model.Name);
            if (playerFpund == null){
                players.Add(playerFpund);
            }
        }

        public bool ExistsModel(string name)
        {
            return (players.Any(x => x.Name == name));
        }

        public IPlayer GetModel(string name) => players.Find(x => x.Name == name);
        

        public bool RemoveModel(string name)
        {
            IPlayer playerFound = players.FirstOrDefault(x => x.Name == name);
            if (playerFound != null)
            {
                players.Remove(playerFound);
                return true;
            }
            return false;
        }
    }
}

