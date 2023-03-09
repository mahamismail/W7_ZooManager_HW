using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZooManager
{
    public class Cat : Animal
    {
        public Cat(string name)
        {
            emoji = "🐱";
            species = "cat";
            this.name = name;
            reactionTime = new Random().Next(1, 6); // reaction time 1 (fast) to 5 (medium)
            predators = new List<string>() { "raptor", "alien" };
            preys = new List<string>() { "mouse", "chick" };
            turnsTaken = 0;
            hunger = 4;
            huntSuccess = false;

        }


        /************************* override ACTIVATE() **************************
        * This method takes from Activate() in Animal class and overrides
        * for Cat.
        * It calls Flee(), Hunt() and writes to console
        ************************************************************************/
        public override void Activate()
        {
            base.Activate();
            Flee(predators, 1);
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

            Console.WriteLine("I am a cat. Meow.");

            turnsTaken++;
            Console.WriteLine($"This {species} took {turnsTaken} turns.");
        }


    }
}

