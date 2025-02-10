using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortenInTheMaking
{
    internal class ProgressBar : Overlay
    {
        private int productivity;
        private Rectangle backgroundRectangle;
        private Rectangle foregroundRectangle;

        public ProgressBar(Enum type, Vector2 spawnPos) : base(type, spawnPos)
        {
            this.scale = 1f;
            this.layer = 0.98f;
            this.sprite = GameWorld.sprites[type];
        }

        public override void LoadContent(ContentManager content)
        {
            backgroundRectangle = new Rectangle((int)position.X, (int)position.Y, sprite.Width, sprite.Height);
            foregroundRectangle = new Rectangle((int)position.X, (int)position.Y, sprite.Width, sprite.Height);
        }

        private void UpdateProductivity()
        {

            //float healthPercentage = 100;
            //if (enemyHealthbar == false)
            //    healthPercentage = (float)(GameWorld.PlayerInstance.Health / (float)(GameWorld.PlayerInstance.MaxHealth + GameWorld.PlayerInstance.HealthBonus));

            if (RessourceType.Coffee > 0)
            {

            }
        }
    }
}
