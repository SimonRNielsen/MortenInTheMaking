using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MortenInTheMaking
{
    internal class Worker : GameObject, ISelectable
    {
        #region Fields

        private bool busy;
        private Vector2 destination;
        private Vector2 velocity;
        private Vector2 spawnPosition;
        private int hasCoffee = 10;
        private RessourceType carriedRessource;
        private bool hasRessource = false;

        #endregion
        #region Properties

        public bool Busy { get => busy; set => busy = value; }
        public Vector2 Destination { get => destination; set => destination = value; }
        public Vector2 SpawnPosition { get => spawnPosition; set => spawnPosition = value; }
        public int HasCoffee { get => hasCoffee; set => hasCoffee = value; }

        #endregion
        #region Constructor

        public Worker(Enum type, Vector2 spawnPos) : base(type, spawnPos)
        {
            Type = type;
            spawnPosition = spawnPos;
            position = spawnPos;
            this.sprite = GameWorld.sprites[type];
            this.layer = 0.98f;
            this.Destination = position;
            switch ((WorkerID)type)
            {
                case WorkerID.Philip:
                case WorkerID.Simon:
                    spriteEffectIndex = 0;
                    break;
                case WorkerID.Irene:
                case WorkerID.Rikke:
                    spriteEffectIndex = 1;
                    break;
            }
        }

        #endregion
        #region Methods

        public override void Update(GameTime gameTime)
        {
            if ((Vector2.Distance(Position, this.Destination) > 100) || (Vector2.Distance(Position, this.Destination) > 0 && this.Destination == spawnPosition))
            {
                MoveToDestination(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            float displace = 0;
            switch (spriteEffectIndex)
            {
                case 0:
                    displace = -(sprite.Width / 2f);
                    break;
                case 1:
                    displace = sprite.Width / 2f;
                    break;
            }
            if (hasRessource)
                spriteBatch.Draw(GameWorld.sprites[carriedRessource], new Vector2(position.X + displace, position.Y), null, color, 0f, new Vector2(sprite.Width / 2, sprite.Height / 2), 0.6f, spriteEffects[0], layer);
        }

        public void MoveToDestination(GameTime gameTime)
        {
            if (Destination == GameWorld.locations[WorkstationType.BrewingStation] && Vector2.Distance(destination, position) < 105)
            {
                if (GameWorld.BrewingStation.AssignedWorker != this)
                    busy = false;
                hasRessource = false;
            }
            //Calculating the deltatime which is the time that has passed since the last frame
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Position.X + 5 < this.Destination.X)
                velocity += new Vector2(1, 0);
            else if (Position.X - 5 > this.Destination.X)
                velocity -= new Vector2(1, 0);

            if (Position.Y + 5 < this.Destination.Y)
                velocity += new Vector2(0, 1);
            else if (Position.Y - 5 > this.Destination.Y)
                velocity -= new Vector2(0, 1);

            //Speed for worker walkin vertically or horizontally
            int speed = 300;
            //Speed for worker walking diagonally
            if ((velocity.Y != 0) && (velocity.X != 0))
            { speed = 200; }
            position += (speed * velocity * deltaTime);

            switch (velocity.X)
            {
                case < 0:
                    spriteEffectIndex = 0;
                    break;
                case > 0:
                    spriteEffectIndex = 1;
                    break;
            }

            velocity = Vector2.Zero;

            if (Vector2.Distance(position, spawnPosition) < 10)
                switch ((WorkerID)type)
                {
                    case WorkerID.Philip:
                    case WorkerID.Simon:
                        spriteEffectIndex = 0;
                        break;
                    case WorkerID.Irene:
                    case WorkerID.Rikke:
                        spriteEffectIndex = 1;
                        break;
                }
        }

        public void DeliverResource(WorkstationType workstation)
        {
            this.Destination = GameWorld.locations[WorkstationType.BrewingStation];
            //this.Busy = false;
            switch (workstation)
            {
                case WorkstationType.CoffeeBeanStation:
                    GameWorld.BrewingStation.CoffeeBeans++;
                    carriedRessource = RessourceType.CoffeeBeans;
                    hasRessource = true;
                    break;
                case WorkstationType.WaterStation:
                    GameWorld.BrewingStation.Water++;
                    carriedRessource = RessourceType.Water;
                    hasRessource = true;
                    break;
                case WorkstationType.MilkStation:
                    carriedRessource = RessourceType.Milk;
                    hasRessource = true;
                    GameWorld.BrewingStation.Milk++;
                    break;
            }


        }



        #endregion
    }
}
