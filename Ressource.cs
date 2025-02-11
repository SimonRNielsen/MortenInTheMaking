using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MortenInTheMaking
{
    internal class Ressource : GameObject
    {
        #region Fields
        Thread statusThread;
        #endregion

        #region Properties



        #endregion

        #region Constructor


        public Ressource(Enum type, Vector2 spawnPos) : base(type, spawnPos)
        {
            this.layer = 0.99f;
            Type = type;
            position = spawnPos;


            statusThread = new Thread(Kaffe);
            statusThread.Start();
        }


        #endregion

        #region Methods

        public void Kaffe()
        {
            
        }


        public string RessourceStatus()
        {
            int water = GameWorld.BrewingStation.Water;
            int milk = GameWorld.BrewingStation.Milk;
            int coffebean = GameWorld.BrewingStation.CoffeeBeans;
            int coffee = GameWorld.BrewingStation.Coffee;
            return $"Coffee bean: {coffebean} \n" +
                $"Milk: {milk} \n" +
                $"Water : {water} \n" +
                $"Coffee: {coffee}";
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            spriteBatch.DrawString(GameWorld.ressourceFont, RessourceStatus(), new Vector2(10, 10), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
        }

        #endregion
    }
}
