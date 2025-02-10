using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MortenInTheMaking
{
    internal class Workstation : GameObject, ISelectable
    {
        #region Fields
        private bool KeepWorkstationRunning = true;
        Thread internalWorkstationThread;


        #endregion
        #region Properties



        #endregion
        #region Constructor


        public Workstation(Enum type, Vector2 spawnPos) : base(type, spawnPos)
        {
            Type = type;
            position = spawnPos;
            sprite = GameWorld.sprites[type];
            internalWorkstationThread = new Thread(RunWorkstation);
            internalWorkstationThread.IsBackground = true;
            GameWorld.locations.Add(type, spawnPos);
        }

        #endregion
        #region Methods

        public void RunWorkstation()
        {
            while (KeepWorkstationRunning) //Ændre til GameRunning 
            {
                if (/*Assigned worker at station*/)
                {
                    color = Color.Green;
                    Thread.Sleep(5000);
                    if (/*Asigned worker still at station*/)
                    {
                        Worker.NextMove(Type);
                    }
                    color = Color.White;
                }
                else
                {
                    Thread.Sleep(1000);
                }


                if ((workerAssignedTo == type) && WorkerDistance < X)
                {
                    Sprite.Farver = ændret
                  Sleep(5000)
                  If(workerAssigned && WorkerDistance < X)
                  {
                        Worker.NextMove(type) //Her bliver workerAssigned forhåbentligt ændret til en anden type 
                  }
                    Sprite.Farve = original
                }
                Else
                  {
                    Sleep(1000);
                }

            }
        }

        public void Start()
        {
            internalWorkstationThread.Start();
        }

        #endregion
    }
}
