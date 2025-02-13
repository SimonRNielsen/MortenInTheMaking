using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Threading;

namespace MortenInTheMaking
{
    internal class ProductivityManager : GameObject
    {
        #region fields
        private int maxProductivity = 100;


        private Thread ProductivityThread;
        private int productivity;

        #endregion
        #region Properties

        #endregion
        #region Constructor



        /// <summary>
        /// Constructor for "baren" der fylder produktivitet
        /// </summary>
        /// <param name="type"></param>
        /// <param name="spawnPos"></param>
        /// <param name="test"></param>
        public ProductivityManager(Enum type, Vector2 spawnPos) : base(type, spawnPos)
        {
            this.layer = 0.98f;
            this.sprite = GameWorld.sprites[type];

            ProductivityThread = new Thread(UpdateProductivity);
            ProductivityThread.IsBackground = true;
            ProductivityThread.Start();
        }


        #endregion
        #region methods
        //public void DrinkCoffee()
        //{
        //    int coffee = GameWorld.BrewingStation.Coffee;
        //}

       



        public void UpdateProductivity()
        {
            while (GameWorld.GameRunning)
            {
                //if (productivity > 0)
                //{
                    Thread.Sleep(2000);
                    //productivity--; //dræner produktivitet hele tiden, langsomt over tid
                    productivity = GameWorld.Productivity;
                //}


                //productivity = MathHelper.Clamp(productivity, 0, maxProductivity);
                Thread.Sleep(1000);//Opdaterer hvert 1 sek

            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            if (GameWorld.GameRunning)
            {

                // Udregn skaleret bredde korrekt
                float scaleX = (float)GameWorld.Productivity / maxProductivity;
                int scaledWidth = (int)MathF.Round(sprite.Width * scaleX);

                // Sørg for, at vi ikke tegner en negativ bredde
                scaledWidth = Math.Max(0, scaledWidth);

                // Tegn kun den del af baren, der er fyldt
                Rectangle sourceRectangle = new Rectangle(0, 0, scaledWidth, sprite.Height);
                spriteBatch.Draw(GameWorld.sprites[type], position, sourceRectangle, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, layer);
            }
            //else
            //{

            //    // Tegn den normale, ikke-fyldte bar
            //    spriteBatch.Draw(GameWorld.sprites[type], position, Color.White);

            //}
        }

        #endregion
    }
}
