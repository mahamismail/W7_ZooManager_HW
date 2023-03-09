using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZooManager
{
    interface IPrey
    {
        List<string> predators { get; } // list of animals that an animal can avoid
        void Flee(List<string> predators, int dist); // the common function that all preys use
    }
}
