using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MortenInTheMaking
{
    internal class ProductivityManager : Overlay
    {
        #region fields
        private int productivity; //produktivitet starter på 50%
        private int maxProductivity = 100;

        public int Money { get; private set; } = 0; //Start sum

        private bool running = true;
        private Thread ProductivityThread;

        #endregion
        #region Properties

        #endregion
        #region Constructor

        public ProductivityManager(Enum type, Vector2 spawnPos) : base(type, spawnPos)
        {
            this.scale = 1f;
            this.layer = 0.98f;
            this.sprite = GameWorld.sprites[type];

            ProductivityThread = new Thread(UpdateProductivity);
            ProductivityThread.IsBackground = true;
            ProductivityThread.Start();
        }


        #endregion
        #region methods


        ////public void StartThread()
        ////{
        ////    productivityThread.Start();
        ////}


        public void StopThread()
        {
            running = false;
            ProductivityThread.Join(); //Behøves muligvis ikke
        }

        public void DrinkCoffee()
        {
            productivity = Math.Min(maxProductivity, productivity + 20); //øger produktivitet
        }

        public void WorkAtComputer()
        {
            if (productivity > 0)
            {
                Thread.Sleep(1000);
                Money += 5;
                productivity -= 1;
            }
        }

        public void UpdateProductivity()
        {
            while (running)
            {
                if (productivity > 0)
                {
                    productivity--; //dræner produktivitet hele tiden, langsomt over tid
                }

                productivity = MathHelper.Clamp(productivity, 0, maxProductivity);

                Thread.Sleep(1000);//Opdaterer hvert 1 sek

            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (type == (Enum)ProgressFilling.BarFilling)
            {
                // Udregn skaleret bredde
                int scaleX = productivity / maxProductivity;
                float scaledWidth = sprite.Width * scaleX;
                int scaleWidthInt = Convert.ToInt32(scaledWidth);

                // Tegn kun den del af baren, der er fyldt
                Rectangle sourceRectangle = new Rectangle(0, 0, scaleWidthInt, sprite.Height);
                spriteBatch.Draw(GameWorld.sprites[type], position, sourceRectangle, Color.White);
            }
            else
            {
                // Tegn den normale, ikke-fyldte bar
                spriteBatch.Draw(GameWorld.sprites[type], position, Color.White);
            }
        }

        #endregion
    }
}
