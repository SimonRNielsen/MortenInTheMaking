using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortenInTheMaking
{
    internal class Worker : GameObject, ISelectable
    {
        #region Fields

        private bool busy;
        private Vector2 destination;


        #endregion
        #region Properties

        public bool Busy { get => busy; set => busy = value; }


        #endregion
        #region Constructor

        public Worker(Enum type, Vector2 spawnPos) : base(type, spawnPos)
        {
            Type = type;
            position = spawnPos;
            this.sprite = GameWorld.sprites[type];
            this.layer = 0.98f;
        }

        #endregion
        #region Methods

        public void DeliverResource(WorkstationType workstation)
        {
            destination = GameWorld.locations[WorkstationType.BrewingStation];
            //Walk
            switch (workstation)
            {
                case WorkstationType.CoffeeBeanStation:
                    GameWorld.BrewingStation.CoffeeBeans++;
                    break;
                case WorkstationType.WaterStation:
                    GameWorld.BrewingStation.Water++;
                    break;
                case WorkstationType.MilkStation:
                    GameWorld.BrewingStation.Milk++;
                    break;
           }

            
        }

        #endregion
    }
}
