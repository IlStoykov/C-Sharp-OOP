using Handball.Models.Contracts;
using Handball.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Handball.Models
{
    public class Team : ITeam
    {
        private string name;
        private List<IPlayer> players;
        public Team(string name)
        {
            Name = name;
            players = new List<IPlayer>();
            PointsEarned = 0;
        }

        public string Name {
            get => name;
            private set {
                if (string.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentNullException(ExceptionMessages.TeamNameNull);
                }
                name = value;
            }
        }
        public int PointsEarned { get; private set; }

        public double OverallRating => players.Any() ? Math.Round(players.Average(x => x.Rating), 2): 0; 

        public IReadOnlyCollection<IPlayer> Players => players.AsReadOnly();

        public void Draw()
        {
            PointsEarned += 1;
            IPlayer playerFound = players.OfType<Goalkeeper>().FirstOrDefault();
            playerFound.IncreaseRating();
        }

        public void Lose()
        {
            foreach (var player in players){
                player.DecreaseRating();
            }
        }

        public void SignContract(IPlayer player)
        {
            IPlayer playerFound = players.FirstOrDefault(x => x.Name == player.Name);
            if (playerFound == null){
                players.Add(playerFound);
            }
        }
        public void Win()
        {
            PointsEarned += 3;
            foreach (var player in players) { 
                player.IncreaseRating();
            }
        }
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine($"Team: {Name} Points: {PointsEarned}");
            result.AppendLine($"--Overall rating: {OverallRating:F2}");//check if it ok
            result.AppendLine($"--Players: {(players.Any() ? string.Join(", ", players.Select(p => p.Name)) : "none")}");

            return result.ToString().TrimEnd();
        }
    }
}
