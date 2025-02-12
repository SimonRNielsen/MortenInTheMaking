using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortenInTheMaking
{
    internal interface ISelectable
    {
        void AssignToWorkstation(Worker worker, Workstation workstation)
        {
            workstation.AssignedWorker = worker;
            if ((WorkstationType)workstation.Type != WorkstationType.Computer)
            {
                worker.Destination = GameWorld.locations[workstation.Type];
            }
            else if ((WorkstationType)workstation.Type == WorkstationType.Computer)
            {
                worker.Destination = worker.SpawnPosition;
            }
        }
    }
}
