using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZooManager
{
    public class Skull : Animal  // Extending to Animal class because it is an animal's skull/carcass
    {
        public Skull(string name)
        {
            emoji = "☠️";
            species = "skull";
            reactionTime = 0;  // default reaction time for animals (1 - 10)
        }

        public override void Activate()
        {
            Console.WriteLine($"Animal dead at {location.x},{location.y} activated");
        }
    }
}
