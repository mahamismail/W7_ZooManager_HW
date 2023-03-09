using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZooManager
{
    public class LivingOrg
    {
        // only allow subclasses to modify these properties
        public string emoji { get; protected set; } 
        public string species { get; protected set; }
        public string name { get; protected set; }
        public int reactionTime { get; protected set; }  // default reaction time for animals (1 - 10)
        public int turnsTaken { get; protected set; }// number of turns taken on the board
        public int hunger { get; protected set; }

        public bool isActivated { get; protected set; } = false; // bool to check when animal is activated.

        public bool huntSuccess; //{ get; set; }

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
            huntSuccess = false;
            isActivated = false;
            //Console.WriteLine($"Animal {name} at {location.x},{location.y} deactivated");
        }

    }
}
