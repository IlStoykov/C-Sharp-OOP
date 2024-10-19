using Handball.Core.Contracts;
using Handball.Models;
using Handball.Models.Contracts;
using Handball.Repositories;
using Handball.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Handball.Core
{
    public class Controller : IController
    {
        private PlayerRepository players;
        private TeamRepository teams;
        private string [] playerTypeCheck = new string[] { "Goalkeeper", "CenterBack", "ForwardWing"};
        public Controller(){
            players = new PlayerRepository();
            teams = new TeamRepository();
        }
        ITeam teamFound = null;
        IPlayer playerFound = null;      

        public string NewContract(string playerName, string teamName)
        {
            teamFound = teams.GetModel(teamName);
            playerFound = players.GetModel(playerName);

            if (playerFound == null)
            {
                return string.Format(OutputMessages.PlayerNotExisting, playerName, players.GetType().Name);
            }
            else if (teamFound == null)
            {
                return string.Format(OutputMessages.TeamNotExisting, teamName, teams.GetType().Name);
            }
            else if (playerFound.Team != null)
            {
                return string.Format(OutputMessages.PlayerAlreadySignedContract, playerName, playerFound.Team);
            }            
                
            playerFound.JoinTeam(teamName);
            teamFound.SignContract(playerFound);
            return string.Format(OutputMessages.SignContract, playerName, teamName);            
        }

        public string NewGame(string firstTeamName, string secondTeamName)
        {
            ITeam firstTeam = teams.GetModel(firstTeamName);
            ITeam secondTeam = teams.GetModel(secondTeamName);
            ITeam winner = null;
            ITeam loser = null;
            if (firstTeam.OverallRating == secondTeam.OverallRating) {
                firstTeam.Draw();
                secondTeam.Draw();
                return string.Format(OutputMessages.GameIsDraw, firstTeamName, secondTeamName);
            }
            if (firstTeam.OverallRating > secondTeam.OverallRating) { 
                winner = firstTeam;
                loser = secondTeam;
            }
            else
            {
                winner = secondTeam;
                loser = firstTeam;
            }
            winner.Win();
            loser.Lose();
            return string.Format(OutputMessages.GameHasWinner, winner.Name, loser.Name);
        }

        public string NewPlayer(string typeName, string name)
        {
            if (!playerTypeCheck.Contains(typeName)) {
                return string.Format(OutputMessages.InvalidTypeOfPosition, typeName);
            }
            playerFound = players.GetModel(name);
            if (playerFound != null)
            {
                return string.Format(OutputMessages.PlayerIsAlreadyAdded, name, players.GetType().Name, typeName);
            }
            if (typeName == "Goalkeeper")
            {
               playerFound = new Goalkeeper(name);
            }
            if (typeName == "ForwardWing")
            {
                playerFound = new ForwardWing(name);
            }
            if (typeName == "CenterBack") { 
                playerFound = new CenterBack(name);
            }
            players.AddModel(playerFound);
            return string.Format(OutputMessages.PlayerAddedSuccessfully, name);
        }
        public string NewTeam(string name)
        {
            teamFound = teams.GetModel(name);
            if (teamFound != null)
            {
                return string.Format(OutputMessages.TeamAlreadyExists, name, teams.GetType().Name);
            }
            else {
                teamFound = new Team(name);
                teams.AddModel(teamFound);
                return string.Format(OutputMessages.TeamSuccessfullyAdded, name, teams.GetType().Name);
            }            
        }

        public string PlayerStatistics(string teamName)
        {
            StringBuilder result = new StringBuilder();
            ITeam teamFound = teams.GetModel(teamName);
            List<IPlayer> orderdPlayers = teamFound.Players.OrderByDescending(p => p.Rating).ThenBy(x => x.Name).ToList();

            result.AppendLine($"***{teamName}***");
            foreach (var player in orderdPlayers) {
                result.AppendLine($"{player.ToString()}");
            }
            return result.ToString().TrimEnd();
        }
        public string LeagueStandings()
        {
            List<ITeam> teamSelected = teams.Models.OrderByDescending(x => x.PointsEarned)
                .OrderByDescending(x => x.OverallRating).ThenBy(x => x.Name).ToList();

            StringBuilder result = new StringBuilder();
            result.AppendLine("***League Standings***");
            foreach (var team in teamSelected)
            {
                result.AppendLine($"Team: {team.Name} Points: {team.PointsEarned}");
                result.AppendLine($"--Overall rating: {team.OverallRating}");

                string players = team.Players.Any() ? string.Join(", ", team.Players.Select(x => x.Name)) : "none";

                result.AppendLine($"--Players: {players}");
            }
            return result .ToString().TrimEnd();
        }         

    }
}
