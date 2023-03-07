using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZooManager
{
    public class LivingOrg
    {
        public string emoji;
        public string species;
        public string name;
        public int reactionTime = 5; // default reaction time for animals (1 - 10)
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

    }
}
