using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZooManager
{
    public class Alien : LivingOrg, IPredator
    {
        public List<string> preys { get; set; } // list of animals that an animal can attacked
        //get { return preys; } 
        //set { preys = value; }

        public Alien(string name)
        {
            emoji = "👽";
            species = "alien";
            this.name = name;
            reactionTime = 1; // reaction time 1 (fast) to 5 (medium)
            preys = new List<string>() { "cat", "mouse" , "chick" , "raptor"}; // find a way to call all animal subclass species names automatically or change something in hunt function.
            turnsTaken = 0;
        }

        /************************* HUNT() ***************************************
         * This method seeks preys around it upto a given distance in
         * each direction, and attacks if present.
         * Takes parameters List of preys and distance (# of blocks to be checked
         * in each direction)
         * Called in different Animal subclasses in override ACtivate()
         ************************************************************************/
        public void Hunt(List<string> preys, int dist)
        {
            foreach (string prey in preys)
            {
                if (Game.Seek(location.x, location.y, Direction.up, prey, dist) != 0) // if the dist returned from Seek() is not 0 do this
                {
                    Game.Attack(this, Direction.up);
                }
                else if (Game.Seek(location.x, location.y, Direction.down, prey, dist) != 0)
                {
                    Game.Attack(this, Direction.down);
                }
                else if (Game.Seek(location.x, location.y, Direction.left, prey, dist) != 0)
                {
                    Game.Attack(this, Direction.left);
                }
                else if (Game.Seek(location.x, location.y, Direction.right, prey, dist) != 0)
                {
                    Game.Attack(this, Direction.right);
                }
            }
        }

        public override void Activate()
        {
            base.Activate();
            Console.WriteLine("I am an alien. Argh.");
            Hunt(preys, 1);
            turnsTaken++;

            Console.WriteLine($"This {species} took {turnsTaken} turns.");
        }

    }
}
