using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZooManager
{
    public class Raptor : Bird
    {
        public Raptor(string name)
        {
            emoji = "🦅";
            species = "raptor";
            this.name = name;
            reactionTime = 1; // reaction time 1 (fast)
            preys = new List<string>() { "cat", "mouse" };
            turnsTaken = 0;

        }

        /************************* override ACTIVATE() **************************
        * This method takes from Activate() in Animal class and overrides
        * for Raptor.
        * It calls Hunt() and writes to console
        ************************************************************************/
        public override void Activate()
        {
            base.Activate();
            Console.WriteLine("I am a raptor. Rawr.");
            Hunt(preys, 1);
            turnsTaken++;
            Console.WriteLine($"This {species} took {turnsTaken} turns.");
        }

    }
}
