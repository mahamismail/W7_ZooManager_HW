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
            hunger = 4;
            huntSuccess = false;

        }

        public List<string> Preys { get; set; }

        /************************* override ACTIVATE() **************************
        * This method takes from Activate() in Animal class and overrides
        * for Raptor.
        * It calls Hunt(), Flee() and Death() and writes to console
        ************************************************************************/
        public override void Activate()
        {
            base.Activate();
            Hunt(preys, 1);

            if (huntSuccess == true)
            {
                hunger = 4;
            }
            else
            {
                hunger--;

                if (hunger <= 0)
                {
                    Death(this);
                }
            }

            Console.WriteLine("I am a raptor. Rawr.");

            turnsTaken++;
            Console.WriteLine($"This {species} took {turnsTaken} turns.");
        }
    }
}
