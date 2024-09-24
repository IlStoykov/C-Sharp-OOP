using Handball.Models.Contracts;
using Handball.Utilities.Messages;
using System;
using System.Text;


namespace Handball.Models
{
    public abstract class Player : IPlayer
    {
        private string name;
        private double rating;
        private string team;
        public Player(string name, double rating) { 
            Name = name;
            Rating = rating;
        }
        public string Name { 
            get => name; private set {
                if (string.IsNullOrWhiteSpace(value)){ 
                    throw new ArgumentNullException(ExceptionMessages.PlayerNameNull);
                }
                name = value;
            }
        }

        public double Rating { 
            get => rating; 
            protected set { 
                rating = value > 10 ? 10 : value < 0 ? 0 : rating;
            }
        }
        public string Team { 
            get => team; private set {
                if (string.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentException(ExceptionMessages.TeamNameNull);
                }
                team = value;
            }
        }
        public void DecreaseRating()
        {
            
        }
        public void IncreaseRating()
        {
            
        }
        public void JoinTeam(string name)
        {
            Team = name;
        }
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine($"{GetType().Name}: {Name}");
            result.AppendLine($"--Rating: {Rating}");
            return result.ToString().TrimEnd();
        }
    }
}
