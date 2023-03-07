using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZooManager
{
    interface IPredator
    {
        List<string> preys { get; } // list of animals that an animal can avoid
        void Hunt(List<string> preys, int dist);

    }
}
