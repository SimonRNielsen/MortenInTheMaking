using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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
        public float Productivity { get; private set; } = 50f; //Start produktivitet
        public int Money { get; private set; } = 0; //Start sum

        private bool running = true;
        private Thread productivityThread;

        #endregion
        #region Properties

        #endregion
        #region Constructor

        public ProductivityManager(Enum type, Vector2 spawnPos) : base(type, spawnPos)
        {
            productivityThread = new Thread(UpdateProductivity);
            productivityThread.IsBackground = true;

            this.scale = 1f;
            this.layer = 0.98f;
            this.sprite = GameWorld.sprites[type];
        }
        #endregion
        #region methods


        public void StartThread()
        {
            productivityThread.Start();
        }

        public void StopThread()
        {
            running = false;
            productivityThread.Join();
        }

        public void DrinkCoffee()
        {
            Productivity = Math.Min(100f, Productivity + 20f); //øger produktivitet
        }

        public void WorkAtComputer()
        {
            if (Productivity > 0)
            {
                Money += 5;
                Productivity -= 10;
            }
        }

        public void UpdateProductivity()
        {
            while(running)
            {
                if(Productivity > 0 )
                {
                    Productivity -= 0.1f; //dræner produktivitet hele tiden, langsomt over tid
                }

                Thread.Sleep(100);//Opdaterer hvert 100 ms
            }
        }

        #endregion
    }
}
