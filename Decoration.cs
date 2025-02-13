using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortenInTheMaking
{
    internal class Decoration : GameObject
    {
        #region Fields



        #endregion
        #region Properties



        #endregion
        #region Constructor


        public Decoration(Enum type, Vector2 spawnPos) : base(type, spawnPos)
        {
            Type = type;
            position = spawnPos;

            this.scale = 1f;
            this.layer = 0.1f;

            //Sprites and layer
            Types(type);
        }

        #endregion
        #region Methods
         
        public void Types(Enum type)
        {
            this.sprite = GameWorld.sprites[type];

            if (type is DecorationType.Background)
            {
                this.layer = 0f;
            }
            else if (type is DecorationType.Morten)
            {
                this.scale = 0.25f;
                this.SpriteEffectIndex = 1; 
            }
            else if (type is OverlayGraphics.BarHollow || type is OverlayGraphics.Lightning || type is OverlayGraphics.MoneySquare)
            {
                this.scale = 0.98f;
            }
            

        }

        #endregion
    }
}
