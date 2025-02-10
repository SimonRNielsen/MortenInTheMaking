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
        private Worker assignedWorker = null;
        private int coffeeBeans;
        private int water;
        private int milk;
        private int coffee;


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

        public Worker AssignedWorker { get => assignedWorker; set => assignedWorker = value; }
        public int CoffeeBeans
        {
            get => coffeeBeans;
            set
            {
                coffeeBeans = value;
                if (CoffeeBeans > 0 && Water > 0 && milk > 0)
                { Coffee++; }
            }
        }
        public int Water { get => water; set => water = value; }
        public int Milk { get => milk; set => milk = value; }
        public int Coffee { get => coffee; set => coffee = value; }

        #endregion
        #region Methods

        public void RunWorkstation()
        {
            while (KeepWorkstationRunning) //Ændre til GameRunning 
            {
                if (assignedWorker != null && ((WorkstationType)type == WorkstationType.CoffeeBeanStation
                    || (WorkstationType)type == WorkstationType.MilkStation)
                    || (WorkstationType)type == WorkstationType.WaterStation)
                {
                    color = Color.Green;
                    Thread.Sleep(5000);
                    if (assignedWorker != null)
                    {
                        assignedWorker.DeliverResource((WorkstationType)type);
                        assignedWorker = null;
                    }
                    color = Color.White;
                }
                else if (assignedWorker != null && (WorkstationType)type == WorkstationType.BrewingStation)
                {
                    { Thread.Sleep(40); }
                    if (Coffee > 0)
                    {


                    }


                }
                else
                {
                    Thread.Sleep(1000);
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
