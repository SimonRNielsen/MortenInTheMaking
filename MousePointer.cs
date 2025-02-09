using System;
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

        private Enum type;
        private Texture2D sprite;
        private GameObject tempObject;
        private Vector2 position;
        private bool leftClick;
        private bool rightClick;

        #endregion
        #region Properties

        public Rectangle CollisionBox
        {
            get { return new Rectangle((int)position.X, (int)position.Y, 1, 1); }
        }
        public Vector2 Position { get => position; set => position = value; }
        public bool LeftClick { get => leftClick; set => leftClick = value; }
        public bool RightClick { get => rightClick; set => rightClick = value; }

        #endregion
        #region Constructor

        public MousePointer(Enum type)
        {
            this.type = type;
            try
            {
                sprite = GameWorld.sprites[type];
            }
            catch {}
        }

        #endregion
        #region Methods

        /// <summary>
        /// Draws a custom mousecursor at the location its detected to be in
        /// </summary>
        /// <param name="spriteBatch">GameWorld logic</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (sprite != null)
                spriteBatch.Draw(sprite, position, null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 1f);
        }

        #endregion

    }
}
