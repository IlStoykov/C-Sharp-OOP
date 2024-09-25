
namespace Handball.Models
{
    public class Goalkeeper : Player
    {
        private const double intRating = 2.5;
        public Goalkeeper(string name) : base(name, intRating)
        {
        }
        public override void IncreaseRating() {
            Rating += 0.75;
        }
        public override void DecreaseRating(){
            Rating -= 1.25;
        }
    }
}
