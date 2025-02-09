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
    public abstract class GameObject
    {
        #region Fields

        protected Enum type;
        protected Texture2D sprite;
        protected Texture2D[] sprites;
        protected Vector2 position;
        protected float layer = 0.5f;
        protected float scale = 1;
        protected Color color = Color.White;
        protected SpriteEffects[] spriteEffects = new SpriteEffects[3] { SpriteEffects.None, SpriteEffects.FlipHorizontally, SpriteEffects.FlipVertically };
        protected int spriteEffectIndex;
        private float rotation;
        private bool isAlive = true;

        #endregion
        #region Properties
        public Rectangle CollisionBox
        {
            get
            {
                if (sprite != null)
                    return new Rectangle((int)(Position.X - (sprite.Width / 2) * scale), (int)(Position.Y - (sprite.Height / 2) * scale), (int)(sprite.Width * scale), (int)(sprite.Height * scale));
                else
                    return new Rectangle();
            }
        }
        public Texture2D Sprite { get => sprite; set => sprite = value; }
        public float Rotation { get => rotation; set => rotation = value; }
        public Vector2 Position { get => position; set => position = value; }
        public bool IsAlive { get => isAlive; set => isAlive = value; }
        public int SpriteEffectIndex { get => spriteEffectIndex; set => spriteEffectIndex = value; }
        public Enum Type { get => type; protected set => type = value; }

        #endregion
        #region Constructor


        public GameObject(Enum type, Vector2 spawnPos)
        {
            Type = type;
            position = spawnPos;
        }

        #endregion
        #region Methods


        public virtual void LoadContent(ContentManager content)
        {

        }


        public virtual void Update(GameTime gameTime)
        {

        }


        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (sprite != null)
                spriteBatch.Draw(sprite, position, null, color, rotation, new Vector2(sprite.Width / 2, sprite.Height / 2), scale, spriteEffects[spriteEffectIndex], layer);
        }

        #endregion

    }
}
