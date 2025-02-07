﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace MortenInTheMaking
{
    internal class MousePointer
    {

        #region Fields

        private Texture2D sprite;
        private GameObject tempObject;

        #endregion
        #region Properties

        public Rectangle CollisionBox
        {
            get { return new Rectangle((int)GameWorld.MousePosition.X, (int)GameWorld.MousePosition.Y, 1, 1); }
        }

        #endregion
        #region Constructor

        public MousePointer()
        {

        }

        #endregion
        #region Methods

        /// <summary>
        /// Draws a custom mousecursor at the location its detected to be in
        /// </summary>
        /// <param name="spriteBatch">Gameworld logic</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (sprite != null)
                spriteBatch.Draw(sprite, GameWorld.MousePosition, null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 1f);
        }

        #endregion

    }
}
