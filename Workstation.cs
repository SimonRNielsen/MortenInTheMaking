using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

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
        private int coffee = 4;
        public static int Productivity;
        private List<Worker> workersAtComputer = new List<Worker>();
        private int moneyMaking = 1250; //How much money the worker is making 

        #endregion
        #region Properties

        public override Rectangle CollisionBox
        {
            get
            {
                if (sprite != null && (WorkstationType)type == WorkstationType.Computer)
                    return new Rectangle((int)(Position.X - (sprite.Width / 2) * scale), (int)(Position.Y - (sprite.Height / 2) * scale), (int)(sprite.Width * scale), (int)(sprite.Height * scale));
                else if (sprite != null)
                    return new Rectangle((int)(Position.X - ((sprite.Width / 2) * scale) * 2), (int)(Position.Y - ((sprite.Height / 2) * scale) * 2), (int)(sprite.Width * scale) * 2, (int)(sprite.Height * scale) * 2);
                else
                    return new Rectangle();
            }
        }
        public int CoffeeBeans
        {
            get => coffeeBeans;
            set
            {
                lock (GameWorld.ResourceLock)
                {
                    coffeeBeans = value;
                    if (this.coffeeBeans > 0 && this.water > 0 && this.milk > 0)
                    { this.coffee++; this.milk--; this.water--; this.coffeeBeans--; }
                }
            }
        }
        public int Water
        {
            get => water;
            set
            {
                lock (GameWorld.ResourceLock)
                {
                    water = value; if (this.coffeeBeans > 0 && this.water > 0 && this.milk > 0)
                    { this.coffee++; this.milk--; this.water--; this.coffeeBeans--; }
                }
            }
        }
        public int Milk
        {
            get => milk;
            set
            {
                lock (GameWorld.ResourceLock)
                {
                    milk = value; if (this.coffeeBeans > 0 && this.water > 0 && this.milk > 0)
                    { this.coffee++; this.milk--; this.water--; this.coffeeBeans--; }

                }
            }
        }
        public int Coffee { get => coffee; set { coffee = value; } }

        public Worker AssignedWorker { get => assignedWorker; set => assignedWorker = value; }
        internal List<Worker> WorkersAtComputer { get => workersAtComputer; set => workersAtComputer = value; }

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
            while (GameWorld.GameRunning)
            {
                if (AssignedWorker != null
                    && (Vector2.Distance(Position, AssignedWorker.Position) < 100)
                    && ((WorkstationType)type == WorkstationType.CoffeeBeanStation
                    || (WorkstationType)type == WorkstationType.MilkStation
                    || (WorkstationType)type == WorkstationType.WaterStation))
                {
                    //AssignedWorker.Busy = false;
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
                    if (Coffee > 0)
                    {
                        GameWorld.brewingSoundEffectInstance.Play();
                        color = Color.Green;
                        Thread.Sleep(2000);
                        Coffee--;
                        GameWorld.Productivity++;
                    }
                    else
                    {
                        color = Color.Red;
                        Thread.Sleep(1000);
                    }
                    AssignedWorker.Busy = false;
                    AssignedWorker = null;
                    color = Color.White;
                }
                else if (workersAtComputer.LongCount() > 0
                    && (WorkstationType)type == WorkstationType.Computer
                    && Vector2.Distance(WorkersAtComputer[0].SpawnPosition, WorkersAtComputer[0].Position) < 10)
                {
                    foreach (Worker w in workersAtComputer)
                    {
                        if (Vector2.Distance(w.SpawnPosition, w.Position) < 100)
                        {
                            w.Busy = false;
                        }
                    }
                    if (GameWorld.Productivity > 0)
                    {
                        GameWorld.typpingSoundEffectInstance.Play();
                        color = Color.Green;
                        Thread.Sleep(2000);
                        foreach (Worker w in WorkersAtComputer)
                        {
                            if (GameWorld.Productivity > 0)
                            {
                                GameWorld.Productivity--;
                                GameWorld.Money += moneyMaking;
                            }
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
