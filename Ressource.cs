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

        private static string HowToPlay() => "Left mouse click for avatar \n" +
            "Right mouse click for workstation";

        private static string RessourceStatus()
        {
            int water = GameWorld.BrewingStation.Water;
            int milk = GameWorld.BrewingStation.Milk;
            int coffebean = GameWorld.BrewingStation.CoffeeBeans;
            int coffee = GameWorld.BrewingStation.Coffee;
            return 
                $"Coffee bean: {coffebean} \n" +
                $"Milk: {milk} \n" +
                $"Water : {water} \n" +
                $"Coffee: {coffee}";
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(GameWorld.ressourceFont, GameWorld.Money.ToString(), new Vector2(1480 - (GameWorld.Money.ToString().Length * +5), 980), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);

            spriteBatch.DrawString(GameWorld.ressourceFont, RessourceStatus(), new Vector2(10, 20), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            
            spriteBatch.DrawString(GameWorld.ressourceFont, RessourceStatus(), new Vector2(10, 20), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.3f);

            spriteBatch.DrawString(GameWorld.ressourceFont, HowToPlay(), new Vector2(1500, 20), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.3f);
            base.Draw(spriteBatch);
        }

        #endregion
    }
}
