using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handball.Models
{
    public class CenterBack : Player
    {
        private const double initRating = 4.0;
        private const double ratingDeviation = 1.0;
        public CenterBack(string name) : base(name, initRating)
        {
        }
        public override void DecreaseRating()
        {
            Rating -= ratingDeviation;
        }
        public override void IncreaseRating()
        {
            Rating += ratingDeviation;
        }
    }
}
