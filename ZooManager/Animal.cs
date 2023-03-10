using System;
using System.Collections.Generic;

namespace ZooManager
{
    public class Animal
    {
        //calling all variables here, that may or may not be used in its subclasses, depending on need.
        public string emoji;
        public string species;
        public string name;
        public int reactionTime = 5; // default reaction time for animals (1 - 10)
        public List<string> preys; // list of animals that an animal can attacked
        public List<string> predators; // list of animals that an animal can avoid
        public int turnsTaken; // number of turns taken on the board
        public bool isActivated = false; //{ get; set; }// bool to check when animal is activated.
        public Point location;


        /************************* REPORTLOCATION() ******************************
         * This method reports which square the Animal is on. It writes to console.
         * Called in Game object.
         ************************************************************************/
        public void ReportLocation()
        {
            Console.WriteLine($"I am at {location.x},{location.y}");
        }

        /************************* ACTIVATE() ************************************
         * This method activates the animal and writes in console
         * Called in Game object and in all Animal subclasses with override.
         ************************************************************************/
        virtual public void Activate()
        {
            isActivated = true;
            //Console.WriteLine($"Animal {name} at {location.x},{location.y} activated");
        }

        /************************* DEACTIVATE() ************************************
        * This method deactivates the animal and writes in console
        * Called in Game object.
        ************************************************************************/
        public void Deactivate()
        {
            isActivated = false;
            //Console.WriteLine($"Animal {name} at {location.x},{location.y} deactivated");
        }


        /* Note that our cat is currently not very clever about its hunting.
         * It will always try to attack "up" and will only seek "down" if there
         * is no mouse above it. This does not affect the cat's effectiveness
         * very much, since the overall logic here is "look around for a mouse and
         * attack the first one you see." This logic might be less sound once the
         * cat also has a predator to avoid, since the cat may not want to run in
         * to a square that sets it up to be attacked!
         */

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
                else if (Game.Seek(location.x, location.y, Direction.down, prey, dist) != 0 )
                {
                    Game.Attack(this, Direction.down);
                }
                else if (Game.Seek(location.x, location.y, Direction.left, prey, dist) != 0)
                {
                    Game.Attack(this, Direction.left);
                }
                else if (Game.Seek(location.x, location.y, Direction.right, prey, dist) != 0 )
                {
                    Game.Attack(this, Direction.right);
                }
            }
        }


        /* Note that our mouse is (so far) a teeny bit more strategic than our cat.
        * The mouse looks for cats and tries to run in the opposite direction to
        * an empty spot, but if it finds that it can't go that way, it looks around
        * some more. However, the mouse currently still has a major weakness! He
        * will ONLY run in the OPPOSITE direction from a cat! The mouse won't (yet)
        * consider running to the side to escape! However, we have laid out a better
        * foundation here for intelligence, since we actually check whether our escape
        * was succcesful -- unlike our cats, who just assume they'll get their prey!
        */

        /************************* FLEE() ***************************************
        * This method iterates through all predators of the animal and checks 
        * around it upto a given distance in each direction, and flees if present.
        * Takes parameters List of predators and distance (# of blocks to be checked
        * in each direction)
        * Called in different Animal subclasses in overriden Activate()
        ************************************************************************/
        public void Flee(List<string> predators, int dist)
        {
            foreach (string predator in predators)
            {
                if (Game.Seek(location.x, location.y, Direction.up, predator, dist) != 0) // if the dist returned from Seek() is not 0 do this
                {
                    if (Game.Retreat(this, Direction.down)) return;
                }
                if (Game.Seek(location.x, location.y, Direction.down, predator, dist) != 0)
                {
                    if (Game.Retreat(this, Direction.up)) return;
                }
                if (Game.Seek(location.x, location.y, Direction.left, predator, dist) != 0)
                {
                    if (Game.Retreat(this, Direction.right)) return;
                }
                if (Game.Seek(location.x, location.y, Direction.right, predator, dist) != 0)
                {
                    if (Game.Retreat(this, Direction.left)) return;
                }
            }
        }

        /************************* MATURE() **************************
        * This method evolves the curentAnimal to newAnimal. 
        * (Replaces old animal with new)
        * Requires parameters of the currentAnimal and newAnimal
        * It refreshes turnsTaken of newAnimal
        * Called in Activate() of any Animal object that may need maturing
        ************************************************************************/
        public void Mature(Animal currentAnimal, Animal evolvingAnimal)
        {             
            int x = currentAnimal.location.x;
            int y = currentAnimal.location.y;

            Game.animalZones[y][x].occupant = evolvingAnimal;
            evolvingAnimal.turnsTaken = 0;
            Console.WriteLine($"Chick matured into a {evolvingAnimal.GetType().Name}!");

        }

    }
}
