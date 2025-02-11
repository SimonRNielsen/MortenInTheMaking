using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D11;
using System;
using System.Threading;

namespace MortenInTheMaking
{
    internal class ProductivityManager : Overlay
    {
        #region fields
        private float productivity = 50f; //produktivitet starter på 50%
        private float maxProductivity = 100f;

        public int Money { get; private set; } = 0; //Start sum

        private bool running = true;
        private Thread productivityThread;

        #endregion
        #region Properties

        #endregion
        #region Constructor

        public ProductivityManager(Enum type, Vector2 spawnPos) : base(type, spawnPos)
        {
            this.scale = 1f;
            this.layer = 0.98f;
            this.sprite = GameWorld.sprites[type];

            productivityThread = new Thread(UpdateProductivity);
            productivityThread.IsBackground = true;
            productivityThread.Start();
        }

        //public ProductivityManager(Enum type, Vector2 spawnPos /*Texture2D bar*/) : base(type, spawnPos)
        //{
        //    this.scale = 1f;
        //    this.layer = 0.98f;
        //    this.sprite = GameWorld.sprites[type];
        //    bar = GameWorld.sprites[ProgressBarGraphics.BarFill];

        //    productivityThread = new Thread(UpdateProductivity);
        //    productivityThread.IsBackground = true;
        //    productivityThread.Start();
        //}
        #endregion
        #region methods


        //public void StartThread()
        //{
        //    productivityThread.Start();
        //}

        public void StopThread()
        {
            running = false;
            productivityThread.Join();
        }

        public void DrinkCoffee()
        {
            productivity = Math.Min(maxProductivity, productivity + 20f); //øger produktivitet
        }

        public void WorkAtComputer()
        {
            if (productivity > 0)
            {
                Money += 5;
                productivity -= 10;
            }
        }

        public void UpdateProductivity()
        {
            while (running)
            {
                if (productivity > 0)
                {
                    productivity -= 0.1f; //dræner produktivitet hele tiden, langsomt over tid
                }

                productivity = MathHelper.Clamp(productivity, 0, maxProductivity);

                Thread.Sleep(100);//Opdaterer hvert 100 ms
            }
        }

        //public override void Draw(SpriteBatch spriteBatch)
        //{
        //    if (texture == null)
        //        return;
        //    //Texture2D texture = GameWorld.sprites[type];

        //        if (type == (Enum)ProgressBarGraphics.BarFill)
        //    {
        //            // Udregn skaleret bredde
        //            float scaleX = productivity / maxProductivity;
        //            int scaledWidth = (int)(texture.Width * scaleX);

        //            // Tegn kun den del af baren, der er fyldt
        //            Rectangle sourceRectangle = new Rectangle(0, 0, scaledWidth, texture.Height);
        //            spriteBatch.Draw(texture, position, sourceRectangle, Color.White);
        //        }
        //        else
        //        {
        //            // Tegn den normale, ikke-fyldte bar
        //            spriteBatch.Draw(texture, position, Color.White);
        //        }
            
        //}

        //public void UpdateProductivity(float value)
        //{
        //    productivity = MathHelper.Clamp(value, 0, maxProductivity);
        //}

        //public override void Draw(SpriteBatch spriteBatch)
        //{
        //    if (GameWorld.sprites.ContainsKey(type))
        //    {
        //        Texture2D texture = GameWorld.sprites[type];

        //        if(type == ProgressBarGraphics.BarFill)
        //        {
        //            float scaleX = productivity / maxProductivity;
        //            Rectangle sourceRectangle = new Rectangle(0, 0, (int)(texture.Width * scaleX), texture.Height);

        //            spriteBatch.Draw(texture, position, sourceRectangle, Color.White);
        //        }
        //        else 
        //        {
        //            spriteBatch.Draw(texture, position, Color.White);
        //        }
        //    }
        //}

        #endregion
    }
}
