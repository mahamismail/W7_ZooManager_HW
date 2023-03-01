using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZooManager
{
    public static class Game
    {
        static public int numCellsX = 4;
        static public int numCellsY = 4;

        static private int maxCellsX = 10;
        static private int maxCellsY = 10;

        static public List<List<Zone>> animalZones = new List<List<Zone>>();
        static public Zone holdingPen = new Zone(-1, -1, null);


        /************************* SETUPGAME() ***************************************
         * Makes a list of rows and columns for game.
         * Called in Game object
         ************************************************************************/
        static public void SetUpGame()
        {
            for (var y = 0; y < numCellsY; y++)
            {
                List<Zone> rowList = new List<Zone>();
                // Note one-line variation of for loop below!
                for (var x = 0; x < numCellsX; x++) rowList.Add(new Zone(x, y, null));
                animalZones.Add(rowList);
            }
        }

        /************************* ADDZONES() ************************************
         * This method adds row or column to the existing zones.
         * Takes parameters direction
         * Called on button click
         ************************************************************************/
        static public void AddZones(Direction d)
        {
            if (d == Direction.down || d == Direction.up)
            {
                if (numCellsY >= maxCellsY) return; // hit maximum height!
                List<Zone> rowList = new List<Zone>();
                for (var x = 0; x < numCellsX; x++)
                {
                    rowList.Add(new Zone(x, numCellsY, null));
                }
                numCellsY++;
                if (d == Direction.down) animalZones.Add(rowList);
                // if (d == Direction.up) animalZones.Insert(0, rowList);
            }
            else // must be left or right...
            {
                if (numCellsX >= maxCellsX) return; // hit maximum width!
                for (var y = 0; y < numCellsY; y++)
                {
                    var rowList = animalZones[y];
                    // if (d == Direction.left) rowList.Insert(0, new Zone(null));
                    if (d == Direction.right) rowList.Add(new Zone(numCellsX, y, null));
                }
                numCellsX++;
            }
        }

        /************************* ZONECLICK() ***************************************
         * This method checks if a zone has been clicked or not and places animal onto
         * zone if any animal is in holding
         * Takes parameters clickedZone
         * Calls methods ActivateAnimals() and DeactivateAnimals()
         ************************************************************************/
        static public void ZoneClick(Zone clickedZone)
        {
            Console.Write("Got animal ");
            Console.WriteLine(clickedZone.emoji == "" ? "none" : clickedZone.emoji);
            Console.Write("Held animal is ");
            Console.WriteLine(holdingPen.emoji == "" ? "none" : holdingPen.emoji);
            if (clickedZone.occupant != null) clickedZone.occupant.ReportLocation();
            if (holdingPen.occupant == null && clickedZone.occupant != null)
            {
                // take animal from zone to holding pen
                Console.WriteLine("Taking " + clickedZone.emoji);
                holdingPen.occupant = clickedZone.occupant;
                holdingPen.occupant.location.x = -1;
                holdingPen.occupant.location.y = -1;
                clickedZone.occupant = null;
                //ActivateAnimals();
            }
            else if (holdingPen.occupant != null && clickedZone.occupant == null)
            {
                // put animal in zone from holding pen
                Console.WriteLine("Placing " + holdingPen.emoji);
                clickedZone.occupant = holdingPen.occupant;
                clickedZone.occupant.location = clickedZone.location;
                holdingPen.occupant = null;
                Console.WriteLine("Empty spot now holds: " + clickedZone.emoji);
                ActivateAnimals();
                DectivateAnimals();

            }
            else if (holdingPen.occupant != null && clickedZone.occupant != null)
            {
                Console.WriteLine("Could not place animal.");
                // Don't activate animals since user didn't get to do anything
            }
        }


        /************************* ADDANIMALTOHOLDING() ***************************
         * This method adds the selected animal to the holding pen
         * Takes parameters animalType
         * It instantiates new Animal subclass objects depending on given parameter.
         ************************************************************************/
        static public void AddAnimalToHolding(string animalType)
        {
            if (holdingPen.occupant != null) return;
            if (animalType == "cat") holdingPen.occupant = new Cat("Fluffy");
            if (animalType == "mouse") holdingPen.occupant = new Mouse("Squeaky");
            if (animalType == "raptor") holdingPen.occupant = new Raptor("Dinobirdy");
            if (animalType == "chick") holdingPen.occupant = new Chick("Chicky");
            Console.WriteLine($"Holding pen occupant at {holdingPen.occupant.location.x},{holdingPen.occupant.location.y}");
            //ActivateAnimals();
        }

        /*When isActivated was not included in ActivateAnimals(), this is what happened
         * If an animal moves right or down, it gets activated twice in the same call to ActivateAnimals(), 
         * but if it moves up or left (or remains where it is), it only gets activated once. 
         * This is because the squares are checked left to right, top to bottom. When one animal is 
         * activated it performs its task and moves down, of right it is checked AGAIN because the code 
         * was still in process of checking the rest of the squares.
         */

        /************************* ACTIVATEANIMALS() *****************************
         * This method activates animals in the zones.
         * Called on Activate()
         ************************************************************************/
        static public void ActivateAnimals() 
        {
            for (var r = 1; r < 11; r++) // reaction times from 1 to 10
            {
                for (var y = 0; y < numCellsY; y++)
                {
                    for (var x = 0; x < numCellsX; x++)
                    {
                            var zone = animalZones[y][x];
                            if (zone.occupant != null && zone.occupant.reactionTime == r && zone.occupant.isActivated == false)
                            {
                                zone.occupant.Activate();
                                //Console.WriteLine($"Activating square ({x},{y}) of rt {r} with animal {zone.occupant})");
                            }
                    }
                }
            }
        }

        /************************* DEACTIVATEANIMALS() ***************************
         * This method deactivates animals in the zones.
         * Called on Dectivate()
         ************************************************************************/
        static public void DectivateAnimals()
        {
            for (var r = 1; r < 11; r++) // reaction times from 1 to 10
            {
                for (var y = 0; y < numCellsY; y++)
                {
                    for (var x = 0; x < numCellsX; x++)
                    {
                        var zone = animalZones[y][x];
                        if (zone.occupant != null && zone.occupant.reactionTime == r && zone.occupant.isActivated == true)
                        {
                            zone.occupant.Deactivate();
                        }
                    }
                }
            }
        }

        /**************************** SEEK() **************************************
         * This method checks the animal and it's zone's surroundings to 
         * detect other animals.
         * This method takes 4 parameters.
         * - x and y are the starting coordinates for the search
         * - d is the direction to search in
         * - target is the animal species to look for
         * - dist is the maximum distance to search for the target animal
         ************************************************************************/
        static public int Seek(int x, int y, Direction d, string target, int dist)
        {
            int numTargets = 0; //variable to count the number of targets found

            for (int i = 0; i < dist; i++)  //Loop through the specified distance
            {
                numTargets++;

                switch (d) // Move the current position in the specified direction
                {
                    case Direction.up:
                        y--;
                        break;

                    case Direction.down:
                        y++;
                        break;

                    case Direction.left:
                        x--;
                        break;

                    case Direction.right:
                        x++;
                        break;
                }

                if (y < 0 || x < 0 || y > numCellsY - 1 || x > numCellsX - 1) //Checking if the current position is out of bounds
                    break;

                if (animalZones[y][x].occupant == null) //Checking if the current cell is occupied by an animal
                    continue;

                //Checking if the animal in the current cell is the target species
                if (animalZones[y][x].occupant.species == target)
                {
                    Console.WriteLine($"Found {target} {numTargets} block away");

                    return numTargets; //Return the distance
                }
            }

            return 0;
        }

        /**************************** ATTACK() **************************************
         * This method currently assumes that the attacker has determined there is prey
         * in the target direction. In addition to bug-proofing our program, can you think
         * of creative ways that NOT just assuming the attack is on the correct target (or
         * successful for that matter) could be used?
         * This method takes 2 parameters.
         * - attacker is Animal that is attacking, d is the direction to search in
         ************************************************************************/
        static public void Attack(Animal attacker, Direction d)
        {
            Console.WriteLine($"{attacker.name} is attacking {d.ToString()}");
            int x = attacker.location.x;
            int y = attacker.location.y;

            switch (d)
            {
                case Direction.up:
                    animalZones[y - 1][x].occupant = attacker;
                    break;
                case Direction.down:
                    animalZones[y + 1][x].occupant = attacker;
                    break;
                case Direction.left:
                    animalZones[y][x - 1].occupant = attacker;
                    break;
                case Direction.right:
                    animalZones[y][x + 1].occupant = attacker;
                    break;
            }
            animalZones[y][x].occupant = null;
        }



        /* We can't make the same assumptions with this method that we do with Attack, since
         * the animal here runs AWAY from where they spotted their target (using the Seek method
         * to find a predator in this case). So, we need to figure out if the direction that the
         * retreating animal wants to move is valid. Is movement in that direction still on the board?
         * Is it just going to send them into another animal? With our cat & mouse setup, one is the
         * predator and the other is prey, but what happens when we have an animal who is both? The animal
         * would want to run away from their predators but towards their prey, right? Perhaps we can generalize
         * this code (and the Attack and Seek code) to help our animals strategize more...
         */

        /**************************** RETREAT() **************************************
         * This method moves the fleeing animal in the opposite direction from attacker.
         * This method takes 2 parameters.
         * - runner is Animal that is running, d is the direction to search in.
         ************************************************************************/
        static public bool Retreat(Animal runner, Direction d)
        {
            Console.WriteLine($"{runner.name} is retreating {d.ToString()}");
            int x = runner.location.x;
            int y = runner.location.y;

            switch (d)
            {
                case Direction.up:
                    /* The logic below uses the "short circuit" property of Boolean &&.
                     * If we were to check our list using an out-of-range index, we would
                     * get an error, but since we first check if the direction that we're modifying is
                     * within the ranges of our lists, if that check is false, then the second half of
                     * the && is not evaluated, thus saving us from any exceptions being thrown.
                     */
                    if (y > 0 && animalZones[y - 1][x].occupant == null)
                    {
                        animalZones[y - 1][x].occupant = runner;
                        animalZones[y][x].occupant = null;
                        return true; // retreat was successful
                    }
                    return false; // retreat was not successful

                    /* Note that in these four cases, in our conditional logic we check
                     * for the animal having one square between itself and the edge that it is
                     * trying to run to. For example,in the above case, we check that y is greater
                     * than 0, even though 0 is a valid spot on the list. This is because when moving
                     * up, the animal would need to go from row 1 to row 0. Attempting to go from row 0
                     * to row -1 would cause a runtime error. This is a slightly different way of testing
                     * if 
                     */

                case Direction.down:
                    if (y < numCellsY - 1 && animalZones[y + 1][x].occupant == null)
                    {
                        animalZones[y + 1][x].occupant = runner;
                        animalZones[y][x].occupant = null;
                        return true;
                    }
                    return false;
                case Direction.left:
                    if (x > 0 && animalZones[y][x - 1].occupant == null)
                    {
                        animalZones[y][x - 1].occupant = runner;
                        animalZones[y][x].occupant = null;
                        return true;
                    }
                    return false;
                case Direction.right:
                    if (x < numCellsX - 1 && animalZones[y][x + 1].occupant == null)
                    {
                        animalZones[y][x + 1].occupant = runner;
                        animalZones[y][x].occupant = null;
                        return true;
                    }
                    return false;
            }
            return false; // fallback
        }

    }
}

