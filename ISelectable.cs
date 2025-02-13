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
                //workstation.AssignedWorker = worker;
                if (!workstation.WorkersAtComputer.Contains(worker))
                { workstation.WorkersAtComputer.Add(worker); }
                worker.Busy = true;
                worker.Destination = worker.SpawnPosition;
            }
            else if (workstation.AssignedWorker == null)
            {
                if (GameWorld.ComputerStation.WorkersAtComputer.Contains(worker))
                {
                    GameWorld.ComputerStation.WorkersAtComputer.Remove(worker);
                }
                workstation.AssignedWorker = worker;
                worker.Busy = true;
                worker.Destination = GameWorld.locations[workstation.Type];
            }

        }
    }
}
