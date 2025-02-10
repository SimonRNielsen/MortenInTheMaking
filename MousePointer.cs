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
using System.Threading;

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
        private float selectionBoxScale;
        private float selectionBoxLayer = 0.99f;
        private Thread inputThread;

        #endregion
        #region Properties

        public Rectangle CollisionBox
        {
            get { return new Rectangle((int)position.X, (int)position.Y, 1, 1); }
        }
        public Vector2 Position { get => position; }
        public bool LeftClick { get => leftClick; }
        public bool RightClick { get => rightClick; }

        #endregion
        #region Constructor

        public MousePointer(Enum type)
        {

            this.type = type;
            try
            {
                sprite = GameWorld.sprites[type];
            }
            catch { }
            inputThread = new Thread(HandleInput);
            inputThread.IsBackground = true;
            inputThread.Start();

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
            if (tempObject != null)
                spriteBatch.Draw(GameWorld.sprites[DecorationType.SelectionBox], tempObject.Position, null, Color.White, 0f, Vector2.Zero, selectionBoxScale, SpriteEffects.None, selectionBoxLayer);
        }


        private void SetSelectionBoxSize()
        {
            selectionBoxScale = MathHelper.Max((tempObject.Sprite.Width / GameWorld.sprites[DecorationType.SelectionBox].Width), (tempObject.Sprite.Height / GameWorld.sprites[DecorationType.SelectionBox].Height));
        }


        private void LeftClickEvent()
        {
            foreach (GameObject gameObject in GameWorld.gameObjects)
            {
                if (gameObject is ISelectable)
                {
                    if (gameObject.CollisionBox.Intersects(CollisionBox))
                    {
                        tempObject = gameObject;
                        SetSelectionBoxSize();
                        break;
                    }
                    else
                        tempObject = null;
                }
            }
        }


        private void RightClickEvent()
        {
            if (tempObject != null)
                if (tempObject is Worker)
                    foreach (GameObject gameObject in GameWorld.gameObjects)
                    {
                        if (gameObject is ISelectable)
                            if (gameObject.CollisionBox.Intersects(CollisionBox))
                            {
                                if (gameObject is Workstation)
                                {
                                    (gameObject as ISelectable).AssignToWorkstation(tempObject as Worker, gameObject as Workstation);
                                }
                            }
                    }
        }

        private void HandleInput()
        {

            while (GameWorld.GameRunning)
            {
                var mouseState = Mouse.GetState();
                position = mouseState.Position.ToVector2();
                leftClick = mouseState.LeftButton == ButtonState.Pressed;
                rightClick = mouseState.RightButton == ButtonState.Pressed;
            }

        }

        #endregion

    }
}
