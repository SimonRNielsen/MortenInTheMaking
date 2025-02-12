using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortenInTheMaking
{
    internal interface ISelectable
    {
        void AssignToWorkstation(Worker worker, Workstation workstation)
        {
            if ((WorkstationType)workstation.Type == WorkstationType.Computer)
            {
                if (workstation.AssignedWorker != null)
                    workstation.AssignedWorker.Busy = false;
                workstation.AssignedWorker = worker;
                worker.Busy = true;
                worker.Destination = GameWorld.locations[workstation.Type];
            }
            else if (workstation.AssignedWorker == null)
            {
                workstation.AssignedWorker = worker;
                worker.Busy = true;
                worker.Destination = GameWorld.locations[workstation.Type];
            }
            
        }
    }
}
