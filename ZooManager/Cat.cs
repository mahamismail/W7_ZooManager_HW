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
            predators = new List<string>() { "raptor" };
            preys = new List<string>() { "mouse", "chick" };
        }

        public override void Activate()
        {
            base.Activate();
            Console.WriteLine("I am a cat. Meow.");
            Flee(predators);
            Hunt(preys);
        }


    }
}

