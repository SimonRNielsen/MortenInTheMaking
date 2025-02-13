using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Globalization;

namespace MortenInTheMaking
{
    internal class Ressource : GameObject
    {
        #region Fields
        #endregion

        #region Properties


        #endregion

        #region Constructor

        /// <summary>
        /// Ressource information
        /// </summary>
        /// <param name="type">Enum value</param>
        /// <param name="spawnPos">Start position</param>
        public Ressource(Enum type, Vector2 spawnPos) : base(type, spawnPos)
        {
            Type = type;
            position = spawnPos;
        }


        #endregion

        #region Methods
        /// <summary>
        /// Player guide
        /// </summary>
        /// <returns>String with how to information to play the game</returns>
        private static string HowToPlay() => "\nSelect worker: \nLeft mouse click\n" +
            "Assign to workstation: \nRight mouse click";

        /// <summary>
        /// Ressource Status
        /// </summary>
        /// <returns>String with the status of the different kinds of ressources</returns>
        private static string RessourceStatus()
        {
            int water = GameWorld.BrewingStation.Water;
            int milk = GameWorld.BrewingStation.Milk;
            int coffebean = GameWorld.BrewingStation.CoffeeBeans;
            int coffee = GameWorld.BrewingStation.Coffee;
            return 
                $"\nCoffee bean: {coffebean} \n" +
                $"Milk: {milk} \n" +
                $"Water : {water} \n" +
                $"Coffee: {coffee}";
        }

        /// <summary>
        /// This override Draw method is for drawing the different kinds of string
        /// </summary>
        /// <param name="spriteBatch">Spritebatch</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(GameWorld.ressourceFont, GameWorld.Money.ToString("N0", new CultureInfo("en-US")), new Vector2(1403, 980), Color.White, 0f, Vector2.Zero, 1.4f, SpriteEffects.None, 1f);

            spriteBatch.DrawString(GameWorld.ressourceFont, RessourceStatus(), new Vector2(60, 23), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);

            spriteBatch.DrawString(GameWorld.ressourceFont, HowToPlay(), new Vector2(1660, 23), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            base.Draw(spriteBatch);
        }

        #endregion
    }
}
