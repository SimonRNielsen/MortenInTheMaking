﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace MortenInTheMaking
{
    internal class Decoration : GameObject
    {
        #region Fields

        #endregion
        #region Properties



        #endregion
        #region Constructor

        /// <summary>
        /// The decoration will be choosen by the enum value and spawn position
        /// </summary>
        /// <param name="type">Enum value</param>
        /// <param name="spawnPos">Position</param>
        public Decoration(Enum type, Vector2 spawnPos) : base(type, spawnPos)
        {
            Type = type;
            position = spawnPos;

            //Sprites and layer
            Types(type);
        }

        #endregion
        #region Methods
         /// <summary>
         /// To select the correct sprite, layer and spriteeffect
         /// </summary>
         /// <param name="type">Enum value</param>
        public void Types(Enum type)
        {
            this.sprite = GameWorld.sprites[type];
            this.layer = 0.1f;

            if (type is DecorationType.Background)
            {
                this.layer = 0f;
            }
            else if (type is DecorationType.Morten)
            {
                this.scale = 0.25f;
                this.SpriteEffectIndex = 1;
            }
            else if (type is DecorationType.Start)
            {
                this.layer = 1f;
            }
        }


        #endregion
    }
}
