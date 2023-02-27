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
        }

        public override void Activate()
        {
            base.Activate();
            Console.WriteLine("I am a chick. Tweet.");
            Flee(predators);
        }

    }
}
