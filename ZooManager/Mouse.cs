using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZooManager
{
    public class Mouse : Animal
    {
        public Mouse(string name)
        {
            emoji = "🐭";
            species = "mouse";
            this.name = name; // "this" to clarify instance vs. method parameter
            reactionTime = new Random().Next(1, 4); // reaction time of 1 (fast) to 3
            /* Note that Mouse reactionTime range is smaller than Cat reactionTime,
             * so mice are more likely to react to their surroundings faster than cats!
             */
            predators = new List<string>() { "cat" };
            turnsTaken = 0;
        }

        /************************* override ACTIVATE() **************************
        * This method takes from Activate() in Animal class and overrides
        * for Mouse.
        * It calls Flee() and writes to console
        ************************************************************************/
        public override void Activate()
        {
            base.Activate();
            Console.WriteLine("I am a mouse. Squeak.");
            Flee(predators, 1);
            turnsTaken++;
            Console.WriteLine($"This {species} took {turnsTaken} turns.");
        }
    }
}

