using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MortenInTheMaking
{
    internal class Ressource : GameObject
    {
        #region Fields
        #endregion

        #region Properties


        #endregion

        #region Constructor


        public Ressource(Enum type, Vector2 spawnPos) : base(type, spawnPos)
        {
            Type = type;
            position = spawnPos;
        }


        #endregion

        #region Methods

        private static string HowToPlay() => "\nSelect worker: \nLeft mouse click\n" +
            "Assign to workstation: \nRight mouse click";

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

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(GameWorld.ressourceFont, GameWorld.Money.ToString(), new Vector2(1403, 980), Color.White, 0f, Vector2.Zero, 1.4f, SpriteEffects.None, 1f);

            spriteBatch.DrawString(GameWorld.ressourceFont, RessourceStatus(), new Vector2(60, 23), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);

            spriteBatch.DrawString(GameWorld.ressourceFont, HowToPlay(), new Vector2(1660, 23), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            base.Draw(spriteBatch);
        }

        #endregion
    }
}
