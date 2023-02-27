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
        }

        public override void Activate()
        {
            base.Activate();
            Console.WriteLine("I am a raptor. Rawr.");
            Hunt(preys);
        }

    }
}
