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
            //Walk ...
            workstation.AssignedWorker = worker;
            worker.Busy = false;
        }
    }
}
