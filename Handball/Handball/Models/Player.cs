using Handball.Models.Contracts;
using Handball.Utilities.Messages;
using System;


namespace Handball.Models
{
    public abstract class Player : IPlayer
    {
        private string name;
        private double rating;
        private string team;
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
    }
}
