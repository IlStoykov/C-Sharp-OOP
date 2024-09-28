using Handball.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handball.Core
{
    public class Controller : IController
    {
        public string LeagueStandings()
        {
            throw new NotImplementedException();
        }

        public string NewContract(string playerName, string teamName)
        {
            throw new NotImplementedException();
        }

        public string NewGame(string firstTeamName, string secondTeamName)
        {
            throw new NotImplementedException();
        }

        public string NewPlayer(string typeName, string name)
        {
            throw new NotImplementedException();
        }

        public string NewTeam(string name)
        {
            throw new NotImplementedException();
        }

        public string PlayerStatistics(string teamName)
        {
            throw new NotImplementedException();
        }
    }
}
