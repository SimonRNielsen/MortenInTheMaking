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

        private Texture2D sprite;
        private GameObject tempObject;
        private Vector2 position;
        private bool leftClick;
        private bool rightClick;
        private bool ranLeftClick = false;
        private bool ranRightClick = false;
        private float selectionBoxScale;
        private float selectionBoxLayer;
        private Thread inputThread;

        #endregion
        #region Properties

        public Rectangle CollisionBox
        {
            get { return new Rectangle((int)position.X, (int)position.Y, 1, 1); }
        }
        public Vector2 Position { get => position; }
        public bool LeftClick
        {
            get => leftClick;
            private set
            {
                leftClick = value;
                if (value == true)
                    LeftClickEvent();
                else
                    ranLeftClick = false;
            }
        }
        public bool RightClick
        {
            get => rightClick;
            private set
            {
                rightClick = value;
                if (value == true)
                    RightClickEvent();
                else
                    ranRightClick = false;
            }
        }

        #endregion
        #region Constructor

        public MousePointer(Enum type)
        {

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
        /// Draws a custom mousecursor at the location its detected to be in and
        /// </summary>
        /// <param name="spriteBatch">GameWorld logic</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (sprite != null)
                spriteBatch.Draw(sprite, position, null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 1f);
            if (tempObject != null)
                spriteBatch.Draw(GameWorld.sprites[DecorationType.SelectionBox], tempObject.Position, null, Color.White, 0f, new Vector2((tempObject.Sprite.Width / 2 / selectionBoxScale) + 5, (tempObject.Sprite.Height / 2 / selectionBoxScale) + 5), selectionBoxScale, SpriteEffects.None, selectionBoxLayer);
        }

        /// <summary>
        /// Sets the size and layer of the selection box
        /// </summary>
        private void SetSelectionBoxSize()
        {

            selectionBoxScale = Math.Max(tempObject.Sprite.Width / (float)GameWorld.sprites[DecorationType.SelectionBox].Width, tempObject.Sprite.Height / (float)GameWorld.sprites[DecorationType.SelectionBox].Height);
            selectionBoxLayer = tempObject.Layer + 0.001f;

        }

        /// <summary>
        /// Method to run when left mouse is clicked
        /// </summary>
        private void LeftClickEvent()
        {
            if (!ranLeftClick)
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
            ranLeftClick = true;
        }

        /// <summary>
        /// Method to be run when right mouse is clicked
        /// </summary>
        private void RightClickEvent()
        {
            if (tempObject != null && !ranRightClick && tempObject is Worker)
                foreach (GameObject gameObject in GameWorld.gameObjects)
                {
                    if (gameObject is ISelectable && gameObject.CollisionBox.Intersects(CollisionBox) && !(tempObject as Worker).Busy && gameObject is Workstation)
                    {
                        (gameObject as ISelectable).AssignToWorkstation(tempObject as Worker, gameObject as Workstation);
                        break;
                    }
                }
            ranRightClick = true;
        }

        /// <summary>
        /// Thread function to continuously loop HandleInput which translates player input
        /// </summary>
        private void HandleInput()
        {

            while (GameWorld.GameRunning)
            {
                var mouseState = Mouse.GetState();
                position = mouseState.Position.ToVector2();
                LeftClick = mouseState.LeftButton == ButtonState.Pressed;
                RightClick = mouseState.RightButton == ButtonState.Pressed;
            }

        }

        #endregion

    }
}
