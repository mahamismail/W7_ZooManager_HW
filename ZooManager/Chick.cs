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
            predators = new List<string>(){"cat"};
            turnsTaken = 0;
        }

        public override void Activate()
        {
            base.Activate();
            Console.WriteLine("I am a chick. Tweet.");
            Flee(predators, 1);

            if (turnsTaken == 4)
            {
                turnsTaken = 0;
                Mature();
            }
            else
            {
                turnsTaken++;
                Console.WriteLine($"This {species} took {turnsTaken} turns.");
            }
        }

        public void Mature()
        {
            Raptor raptor = new Raptor(name);
        }
    }
}
