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
        private Vector2 velocity;


        #endregion
        #region Properties

        public bool Busy { get => busy; set => busy = value; }
        public Vector2 Destination { get => destination; set => destination = value; }


        #endregion
        #region Constructor

        public Worker(Enum type, Vector2 spawnPos) : base(type, spawnPos)
        {
            Type = type;
            position = spawnPos;
            this.sprite = GameWorld.sprites[type];
            this.layer = 0.98f;
            this.Destination = position;
        }

        #endregion
        #region Methods

        public override void Update(GameTime gameTime)
        {
            if (Vector2.Distance(Position, this.Destination) > 100)
            {
                MoveToDestination(gameTime);
            }
        }

        public void MoveToDestination(GameTime gameTime)
        {
            //Calculating the deltatime which is the time that has passed since the last frame
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Position.X+1 < this.Destination.X)
                velocity += new Vector2(1, 0);
            else if (Position.X-1 > this.Destination.X)
                velocity -= new Vector2(1, 0);

            if (Position.Y+1 < this.Destination.Y)
                velocity += new Vector2(0, 1);
            else if (Position.Y-1 > this.Destination.Y)
                velocity -= new Vector2(0, 1);

            //Speed for worker walkin vertically or horizontally
            int speed = 300;
            //Speed for worker walking diagonally
            if((velocity.Y != 0) && (velocity.X != 0))
                { speed = 200; }
            position += ( speed * velocity * deltaTime);

            velocity = Vector2.Zero;
        }

        public void DeliverResource(WorkstationType workstation)
        {
            this.Destination = GameWorld.locations[WorkstationType.BrewingStation];
            this.Busy = false;
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
