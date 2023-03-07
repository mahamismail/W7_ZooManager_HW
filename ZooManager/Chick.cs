using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZooManager
{
    public class Chick : Bird
    {
        public Chick(string name)
        {
            emoji = "🐥";
            species = "chick";
            this.name = name;
            reactionTime = new Random().Next(6, 10);
            predators = new List<string>() { "cat", "alien" };
            turnsTaken = 0;

        }

        /************************* override ACTIVATE() **************************
        * This method takes from Activate() in Animal class and overrides
        * for Chick.
        * It calls Flee(), Mature() and writes to console
        ************************************************************************/
        public override void Activate()
        {
            base.Activate();
            Console.WriteLine("I am a chick. Tweet.");
            Flee(predators, 1);

            if (turnsTaken == 3)
            {
                Mature(this, new Raptor(name));
            }
            else
            {
                turnsTaken++;
                Console.WriteLine($"This {species} took {turnsTaken} turn/s.");
            }
        }
    }
}
