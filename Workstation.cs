using Microsoft.Xna.Framework;
using SharpDX.Direct2D1.Effects;
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
        /* TODO:
         * Add documentation
         * Use some sort of synchronisation such as lock or semafor, to make sure only one worker can ve at a workstation at a time (other than computer)
         * 
         * */
        #region Fields
        Thread internalWorkstationThread;
        private Worker assignedWorker = null;
        private int coffeeBeans;
        private int water;
        private int milk;
        private int coffee = 1;
        public static int Productivity;


        #endregion
        #region Properties



        #endregion
        #region Constructor
        public int CoffeeBeans
        {
            get => coffeeBeans;
            set
            {
                coffeeBeans = value;
                if (this.CoffeeBeans > 0 && this.Water > 0 && this.Milk > 0)
                { this.Coffee++; }
            }
        }
        public int Water
        {
            get => water;
            set
            {
                water = value; if (this.CoffeeBeans > 0 && this.Water > 0 && this.Milk > 0)
                { this.Coffee++; }
            }
        }
        public int Milk
        {
            get => milk;
            set
            {
                milk = value; if (this.CoffeeBeans > 0 && this.Water > 0 && this.Milk > 0)
                { Coffee++; }
            }
        }
        public int Coffee { get => coffee; set => coffee = value; }



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
        #endregion
        #region Methods

        public void RunWorkstation()
        {
            while (GameWorld.GameRunning)
            {
                if (AssignedWorker != null
                    && (Vector2.Distance(Position, AssignedWorker.Position) < 100)
                    && ((WorkstationType)type == WorkstationType.CoffeeBeanStation
                    || (WorkstationType)type == WorkstationType.MilkStation
                    || (WorkstationType)type == WorkstationType.WaterStation))
                {
                    AssignedWorker.Busy = false;
                    color = Color.Green;
                    Thread.Sleep(2000);
                    if (assignedWorker != null)
                    {
                        assignedWorker.DeliverResource((WorkstationType)type);
                        assignedWorker = null;
                    }
                    color = Color.White;
                }
                else if (assignedWorker != null
                    && (WorkstationType)type == WorkstationType.BrewingStation
                    && Vector2.Distance(Position, AssignedWorker.Position) < 100)
                {
                    AssignedWorker.Busy = false;
                    if (Coffee > 0)
                    {
                        color = Color.Green;
                        Thread.Sleep(2000);
                        Coffee--;
                        Productivity++;
                    }
                    else
                    {
                        color = Color.Red;
                        Thread.Sleep(1000);
                    }
                    AssignedWorker = null;
                    color = Color.White;
                }
                else if (assignedWorker != null
                    && (WorkstationType)type == WorkstationType.Computer
                    && Vector2.Distance(AssignedWorker.SpawnPosition, AssignedWorker.Position) < 100)
                {
                    AssignedWorker.Busy = false;
                    if (Productivity > 0)
                    {
                        color = Color.Green;
                        Thread.Sleep(2000);
                        if (AssignedWorker != null)
                        {
                            Productivity--;
                            //Penge ++
                        }
                    }
                    else
                    {
                        color = Color.Red;
                        Thread.Sleep(500);
                    }
                    color = Color.White;
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
